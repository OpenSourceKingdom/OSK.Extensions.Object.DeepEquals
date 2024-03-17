using System;
using System.Collections.Generic;
using OSK.Extensions.Object.DeepEquals.Ports;

namespace OSK.Extensions.Object.DeepEquals.Internal.Services
{
    internal class ObjectCache : IObjectCache
    {
        #region Variables

        private readonly Dictionary<object, object> _objects;

        #endregion

        #region Constructors

        public ObjectCache()
        {
            _objects = new Dictionary<object, object>();
        }

        #endregion

        #region IObjectCache

        public void Add(object key, object value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            _objects[key] = value;
        }

        public bool TryGet(object key, out object value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return _objects.TryGetValue(key, out value);
        }

        #endregion
    }
}
