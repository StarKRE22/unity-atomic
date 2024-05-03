using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic
{
    internal sealed class BoolSegmentProvider : ISetProvider<int>
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
        
        private bool[] _indexes;

        public BoolSegmentProvider(int startIndex, int endIndex)
        {
            _startIndex = startIndex;
            _endIndex = endIndex;
            _indexes = new bool[endIndex - startIndex + 1];
        }

        public BoolSegmentProvider()
        {
            _indexes = null;
            _startIndex = _endIndex = -1;
        }
        
        public bool Contains(int value)
        {
            return _indexes != null && value >= _startIndex && value <= _endIndex && _indexes[value]; 
        }

        public List<int> GetValues()
        {
            List<int> result = new List<int>();

            if (_indexes == null)
            {
                return result;
            }
            
            for (int i = 0, count = _indexes.Length; i < count; i++)
            {
                if (_indexes[i])
                {
                    result.Add(i);
                }
            }

            return result;
        }

        public int GetValuesNonAlloc(int[] results)
        {
            int count = 0;
            
            if (_indexes == null)
            {
                return count;
            }
            
            for (int i = 0, length = _indexes.Length; i < length; i++)
            {
                if (_indexes[i])
                {
                    results[count++] = i;
                }
            }

            return count;
        }

        public bool Add(int value)
        {
            if (_indexes == null)
            {
                _startIndex = _endIndex = value;
                _indexes = new[] {true};
                return true;
            }
            
            if (value < _startIndex)
            {
                this.AddLeft(value);
                return true;
            }

            if (value > _endIndex)
            {
                this.AddRight(value);
                return true;
            }

            int i = value - _startIndex;
            if (_indexes[i])
            {
                return false;
            }

            _indexes[i] = true;
            return true;
        }

        public bool Remove(int value)
        {
            if (_indexes == null)
            {
                return false;
            }
            
            if (value < _startIndex || value > _endIndex)
            {
                return false;
            }

            int i = value - _startIndex;
            if (_indexes[i])
            {
                _indexes[i] = false;
                return true;
            }

            return false;
        }

        public void UnionWith(IEnumerable<int> values)
        {
            foreach (int value in values)
            {
                this.Add(value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddLeft(int value)
        {
            int diff = _startIndex - value;

            int previousSize = _indexes.Length;
            bool[] newIndexes = new bool[previousSize + diff];

            for (int i = 0; i < previousSize; i++)
            {
                newIndexes[i + diff] = _indexes[i];
            }

            newIndexes[0] = true;

            _indexes = newIndexes;
            _startIndex = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddRight(int index)
        {
            int diff = index - _endIndex;

            int previousSize = _indexes.Length;
            int newSize = previousSize + diff;
            bool[] newIndexes = new bool[newSize];

            for (int i = 0; i < previousSize; i++)
            {
                newIndexes[i] = _indexes[i];
            }

            newIndexes[newSize - 1] = true;

            _indexes = newIndexes;
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