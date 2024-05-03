using System.Runtime.CompilerServices;

namespace Atomic
{
    public sealed class AtomicTypePool
    {
        private int _endIndex;
        private int _startIndex;

        private bool[] _indexes;

        public AtomicTypePool()
        {
            _indexes = null;
            _startIndex = _endIndex = -1;
        }

        int[] All()
        {
            
        }

        
        public bool Has(int entity)
        {
            return _indexes != null && entity >= _startIndex && entity <= _endIndex && _indexes[entity];
        }

        public bool Add(int entity)
        {
            if (_indexes == null)
            {
                _startIndex = _endIndex = entity;
                _indexes = new[] {true};
                return true;
            }

            if (entity < _startIndex)
            {
                this.AddLeft(entity);
                return true;
            }

            if (entity > _endIndex)
            {
                this.AddRight(entity);
                return true;
            }

            int i = entity - _startIndex;
            if (_indexes[i])
            {
                return false;
            }

            _indexes[i] = true;
            return true;
        }

        public bool Del(int entity)
        {
            if (_indexes == null)
            {
                return false;
            }

            if (entity < _startIndex || entity > _endIndex)
            {
                return false;
            }

            int i = entity - _startIndex;
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
    }
}