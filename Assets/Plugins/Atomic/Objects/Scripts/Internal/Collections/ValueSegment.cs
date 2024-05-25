using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Atomic.Objects
{
    internal sealed class ValueSegment : IValueCollection
    {
        private int _startIndex;
        private int _endIndex;

        private object[] _array;

        public ValueSegment(int startIndex, int endIndex)
        {
            _startIndex = startIndex;
            _endIndex = endIndex;
            _array = new object[endIndex - startIndex + 1];
        }

        public object this[int index]
        {
            get
            {
                return index >= _startIndex && index <= _endIndex
                    ? _array[index - _startIndex]
                    : null;
            }
            set
            {
                if (index < _startIndex)
                {
                    this.AddLeft(index, value);
                    return;
                }

                if (index > _endIndex)
                {
                    this.AddRight(index, value);
                    return;
                }

                _array[index - _startIndex] = value;
            }
        }

        public bool TryGetValue(int index, out object result)
        {
            if (index < _startIndex || index > _endIndex)
            {
                result = null;
                return false;
            }

            result = _array[index - _startIndex];
            return result != null;
        }

        public bool TryAdd(int index, object value)
        {
            if (index < _startIndex)
            {
                this.AddLeft(index, value);
                return true;
            }

            if (index > _endIndex)
            {
                this.AddRight(index, value);
                return true;
            }

            index -= _startIndex;

            if (_array[index] != null)
            {
                return false;
            }

            _array[index] = value;
            return true;
        }

        public bool Remove(int index)
        {
            if (index < _startIndex || index > _endIndex)
            {
                return false;
            }

            index -= _startIndex;
            if (_array[index] != null)
            {
                _array[index] = null;
                return true;
            }

            return false;
        }

        public bool Remove(int index, out object removed)
        {
            if (index < _startIndex || index > _endIndex)
            {
                removed = null;
                return false;
            }

            index -= _startIndex;
            removed = _array[index];
            _array[index] = null;
            return removed != null;
        }

        public bool ContainsKey(int index)
        {
            if (index < _startIndex || index > _endIndex)
            {
                return false;
            }

            return _array[index - _startIndex] != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddLeft(int index, object value)
        {
            int diff = _startIndex - index;

            int previousSize = _array.Length;
            object[] newArray = new object[previousSize + diff];

            for (int i = 0; i < previousSize; i++)
            {
                newArray[i + diff] = _array[i];
            }

            newArray[0] = value;

            _array = newArray;
            _startIndex = index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddRight(int index, object value)
        {
            int diff = index - _endIndex;

            int prevLength = _array.Length;
            int newLength = prevLength + diff;
            object[] newArray = new object[newLength];

            for (int i = 0; i < prevLength; i++)
            {
                newArray[i] = _array[i];
            }

            newArray[newLength - 1] = value;

            _array = newArray;
            _endIndex = index;
        }

        public IEnumerator<KeyValuePair<int, object>> GetEnumerator()
        {
            for (int i = 0, length = _array.Length; i < length; i++)
            {
                object value = _array[i];
                if (value != null)
                {
                    yield return new KeyValuePair<int, object>(i + _startIndex, value);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}