using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Atomic.Objects
{
    internal sealed class TagSegment : ITagCollection
    {
        private int _startIndex;
        private int _endIndex;

        private bool[] _indexes;

        public TagSegment(int startIndex, int endIndex)
        {
            _startIndex = startIndex;
            _endIndex = endIndex;
            _indexes = new bool[endIndex - startIndex + 1];
        }

        public bool Contains(int tag)
        {
            return tag >= _startIndex && tag <= _endIndex && _indexes[tag];
        }

        public bool Add(int tag)
        {
            if (tag < _startIndex)
            {
                this.AddLeft(tag);
                return true;
            }

            if (tag > _endIndex)
            {
                this.AddRight(tag);
                return true;
            }

            int i = tag - _startIndex;
            if (_indexes[i])
            {
                return false;
            }

            _indexes[i] = true;
            return true;
        }

        public bool Remove(int tag)
        {
            if (tag < _startIndex || tag > _endIndex)
            {
                return false;
            }

            int i = tag - _startIndex;
            if (_indexes[i])
            {
                _indexes[i] = false;
                return true;
            }

            return false;
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

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0, count = _indexes.Length; i < count; i++)
            {
                if (_indexes[i])
                {
                    yield return i + _startIndex;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}