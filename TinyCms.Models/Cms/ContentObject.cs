using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace TinyCms
{
    public class ContentObject : DynamicObject, ICloneable
    {
        #region ctor

        public ContentObject()
        {

        }

        public ContentObject(IEnumerable<string> properties, bool trackChanges)
        {
            _OriginalValues = new ConcurrentDictionary<string, object>(new Func<IEnumerable<KeyValuePair<string, object>>>(() =>
            {
                List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
                foreach (string prop in properties)
                {
                    list.Add(new KeyValuePair<string, object>(prop, null));
                }
                return list;
            }).Invoke());
            _ChangedValues = new ConcurrentDictionary<string, object>();
            _TrackChanges = trackChanges;
        }

        protected ContentObject(ConcurrentDictionary<string, object> originalValues, ConcurrentDictionary<string, object> changedValues, bool trackChanges)
        {
            _OriginalValues = originalValues;
            _ChangedValues = changedValues;
            _TrackChanges = trackChanges;
        }


        #endregion

        #region public static methods

        public static object ToObject(ContentObject content, bool IncludePublicProperties = true, bool IncludePublicFields = true, bool IncludePrivateProperties = false, bool IncludePrivateFields = false)
        {
            Type t = Type.GetType(content.BaseType);
            object o = t.InvokeMember("", BindingFlags.Public | BindingFlags.CreateInstance, null, null, null);
            BindingFlags props = BindingFlags.Default;
            props |= IncludePublicProperties ? BindingFlags.Public : BindingFlags.Default;
            props |= IncludePrivateProperties ? BindingFlags.NonPublic : BindingFlags.Default;
            BindingFlags fields = BindingFlags.Default;
            fields |= IncludePublicFields ? BindingFlags.Public : BindingFlags.Default;
            fields |= IncludePrivateFields ? BindingFlags.NonPublic : BindingFlags.Default;
            foreach (string key in content._OriginalValues.Keys)
            {
                PropertyInfo p = t.GetProperty(key, props);
                if (p != null)
                {
                    p.SetValue(o, content.Field(key));
                }
                else
                {
                    FieldInfo fi = t.GetField(key, fields);
                    if (fi != null)
                    {
                        fi.SetValue(o, content.Field(key));
                    }
                }
            }
            return o;
        }

        public static T ToObject<T>(ContentObject content, bool IncludePublicProperties = true, bool IncludePublicFields = true, bool IncludePrivateProperties = false, bool IncludePrivateFields = false)
        {
            object o = ToObject(content, IncludePublicProperties, IncludePublicFields, IncludePrivateProperties, IncludePrivateFields);
            return (T)o;
        }


        public static ContentObject FromObject(object o, bool IncludePublicProperties = true, bool IncludePublicFields = true, bool IncludePrivateProperties = false, bool IncludePrivateFields = false)
        {
            BindingFlags props = BindingFlags.Default;
            props |= IncludePublicProperties ? BindingFlags.Public : BindingFlags.Default;
            props |= IncludePrivateProperties ? BindingFlags.NonPublic : BindingFlags.Default;

            ConcurrentDictionary<string, object> values = new ConcurrentDictionary<string, object>();
            foreach (PropertyInfo p in o.GetType().GetProperties(props))
            {
                if (!values.TryAdd(p.Name, p.GetValue(o)))
                {
                    throw new InvalidOperationException(string.Format("The property {0} from {1} could not be added", p.Name, o.GetType().Name));
                }
            }

            props = BindingFlags.Default;
            props |= IncludePublicFields ? BindingFlags.Public : BindingFlags.Default;
            props |= IncludePrivateFields ? BindingFlags.NonPublic : BindingFlags.Default;

            foreach (FieldInfo f in o.GetType().GetFields(props))
            {
                if (!values.TryAdd(f.Name, f.GetValue(o)))
                {
                    throw new InvalidOperationException(string.Format("The field {0} from {1} could not be added", f.Name, o.GetType().Name));
                }
            }

            ContentObject co = new ContentObject(values, new ConcurrentDictionary<string, object>(), false);
            co.BaseType = o.GetType().FullName;
            return co;
        }

        #endregion

        #region Public properties

        public string BaseType { get; set; }

        private bool _TrackChanges = false;

        public bool TrackChanges
        {
            get { return _TrackChanges; }
            set { _TrackChanges = value; }
        }

        private ConcurrentDictionary<string, object> _OriginalValues = new ConcurrentDictionary<string, object>();
        public ConcurrentDictionary<string, object> OriginalValues
        {
            get { return _OriginalValues; }
            set { _OriginalValues = value; }
        }
        private ConcurrentDictionary<string, object> _ChangedValues = new ConcurrentDictionary<string, object>();
        public ConcurrentDictionary<string, object> ChangedValues
        {
            get { return _ChangedValues; }
            set { _ChangedValues = value; }
        }

        public bool HasChanges
        {
            get { return _ChangedValues.Count > 0; }
        }

        public object Field(string Name)
        {
            object o;
            if (InternalGet(Name, out o))
            {
                return o;
            }
            else
            {
                throw new ArgumentException("The Field name '" + Name + "' does not exist", "Name");
            }
        }

        public bool Field(string Name, object Value)
        {
            return InternalSet(Name, Value);
        }
        public bool Field<T>(string Name, T Value)
        {
            return InternalSet(Name, Value);
        }

        public T Field<T>(string Name)
        {
            object o = Field(Name);
            return (T)o;
        }

        #endregion

        #region Public Methods

        public void AcceptChanges()
        {
            if (!_TrackChanges)
            {
                return;
            }
            using (TransactionScope trans = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                foreach (string key in _ChangedValues.Keys)
                {
                    object o;
                    if (_ChangedValues.TryRemove(key, out o))
                    {
                        _OriginalValues.AddOrUpdate(key, o, (k, v) => { return o; });
                    }
                    else
                    {
                        throw new InvalidOperationException(string.Format("Unable to get the value from the property '{0}'", key));
                    }
                }
                if (_ChangedValues.Count > 0)
                {
                    throw new InvalidOperationException(string.Format("There are still {0} unaccepted values", _ChangedValues.Count));
                }
                trans.Complete();
            }
        }

        #endregion

        #region DynamicObject implementation

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            foreach (string key in _OriginalValues.Keys)
            {
                yield return key;
            }
            yield return "__TrackChanges";
            yield return "__BaseType";
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (binder.Name.StartsWith("__"))
            {
                switch (binder.Name)
                {
                    case "__TrackChanges":
                        result = TrackChanges;
                        return true;

                    case "__BaseType":
                        result = BaseType;
                        return true;


                }
            }
            return InternalGet(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (binder.Name.StartsWith("__"))
            {
                switch (binder.Name)
                {
                    case "__TrackChanges":
                        TrackChanges = (bool)value;
                        return true;

                    case "__BaseType":
                        BaseType = (string)value;
                        return true;
                }
            }
            return InternalSet(binder.Name, value);
        }

        #endregion

        #region private support functions

        private bool InternalGet(string Column, out object value)
        {
            if (_TrackChanges && _ChangedValues.TryGetValue(Column, out value))
            {
                return true;
            }
            return _OriginalValues.TryGetValue(Column, out value);
        }

        private bool InternalSet(string Column, object value)
        {
            object o;
            if (!OriginalValues.ContainsKey(Column))
            {
                return _OriginalValues.TryAdd(Column, value);
            }
            if (!_OriginalValues.TryGetValue(Column, out o))
            {
                return false;

            }
            if ((o == null && value != null) || !o.Equals(value))
            {
                if (_TrackChanges)
                {
                    _ChangedValues.AddOrUpdate(Column, value, (key, existing) =>
                    {
                        return value;
                    });
                }
                else
                {
                    _OriginalValues.AddOrUpdate(Column, value, (key, existing) =>
                    {
                        return value;
                    });
                }
            }
            return true;
        }



        #endregion

        #region ICloneable implementation

        public object Clone()
        {
            return new ContentObject(_OriginalValues, _ChangedValues, _TrackChanges);
        }

        #endregion
    }
}
