using System;
using System.Collections.Generic;

namespace FolderDiffLib.DelimonHelpers.Util
{
    public class KeyEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, object> _keySelector;

        public KeyEqualityComparer(Func<T, object> keySelector)
        {
            _keySelector = keySelector;
        }
        
        public bool Equals(T x, T y)
        {
            if (ReferenceEquals(null, x) && ReferenceEquals(null, y))
                return true;

            if (ReferenceEquals(null, x) || ReferenceEquals(null, y))
                return false;

            if (_keySelector(x) == null || _keySelector(y) == null)
                return false;

            return object.Equals(_keySelector(x), _keySelector(y));
        }

        public int GetHashCode(T obj)
        {
            return !Equals(_keySelector(obj), default(T))
                ? _keySelector(obj).GetHashCode()
                : 0;
        }
    }
}