using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic
{
    internal sealed class ObjectSegmentProvider : IMapProvider<int, object>
    {
#if ODIN_INSPECTOR
        [HorizontalGroup]
        [LabelText("Start")]
        [ShowInInspector, ReadOnly]
#endif
        private int _startIndex;

#if ODIN_INSPECTOR
        [HorizontalGroup]
        [LabelText("End")]
        [ShowInInspector, ReadOnly]
#endif
        private int _endIndex;

#if ODIN_INSPECTOR
        [ListDrawerSettings(OnBeginListElementGUI = nameof(DrawLabel))]
        [ShowInInspector, ReadOnly]
#endif
        private object[] _array;

        public ObjectSegmentProvider(int startIndex, int endIndex)
        {
            _startIndex = startIndex;
            _endIndex = endIndex;
            _array = new object[endIndex - startIndex + 1];
        }

        public ObjectSegmentProvider()
        {
            _startIndex = _endIndex = -1;
            _array = null;
        }


        public object this[int index]
        {
            get
            {
                return _array != null && index >= _startIndex && index <= _endIndex
                    ? _array[index - _startIndex]
                    : null;
            }
            set
            {
                if (_array == null)
                {
                    _startIndex = _endIndex = index;
                    _array = new[] {value};
                    return;
                }

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
            if (_array == null)
            {
                result = null;
                return false;
            }
            
            if (index < _startIndex || index > _endIndex)
            {
                result = null;
                return false;
            }

            result = _array[index - _startIndex];
            return result != null;
        }

        public List<KeyValuePair<int, object>> GetPairs()
        {
            var result = new List<KeyValuePair<int, object>>();
            
            if (_array == null)
            {
                return result;
            }
            
            for (int i = 0, length = _array.Length; i < length; i++)
            {
                object value = _array[i];
                if (value != null)
                {
                    result.Add(new KeyValuePair<int, object>(i + _startIndex, value));
                }
            }

            return result;
        }

        public int GetPairsNonAlloc(KeyValuePair<int, object>[] results)
        {
            int count = 0;

            if (_array == null)
            {
                return count;
            }
            
            for (int i = 0, length = _array.Length; i < length; i++)
            {
                object value = _array[i];
                if (value != null)
                {
                    results[count++] = new KeyValuePair<int, object>(i + _startIndex, value);
                }
            }

            return count;
        }

        public bool Add(int index, object value)
        {
            if (_array == null)
            {
                _startIndex = _endIndex = index;
                _array = new[] {value};
                return true;
            }
            
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
            if (_array == null)
            {
                return false;
            }
            
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
            if (_array == null)
            {
                removed = null;
                return false;
            }
            
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

#if ODIN_INSPECTOR
        private void DrawLabel(int index)
        {
            GUILayout.Space(4);
            GUILayout.Label($"Element #{index + _startIndex}");
        }
#endif
    }
}