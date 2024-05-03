using System.Runtime.CompilerServices;

namespace Atomic
{
    //TODO: ECS POOL OPTIMIZATION
    //TODO: разворачивать сразу массив с размером N убрать сегменты)
    public sealed class AtomicValuePool<T> : IAtomicValuePool
    {
        public int Count => _count;
        
        private int _startIndex;
        private int _endIndex;
        private int _count;
        
        //TODO: ADRESSES!
        private T[] _array;

        public AtomicValuePool()
        {
            _startIndex = _endIndex = -1;
            _array = null;
        }

        public T this[int index]
        {
            get => this.Get(index);
            set => this.Set(index, value);
        }

        object IAtomicValuePool.this[int index]
        {
            get => this.Get(index);
            set => this.Set(index, (T) value);
        }
        
        public void Get<>(int entity, ref T value)
        {
            return _array != null && entity >= _startIndex && entity <= _endIndex
                ? _array[entity - _startIndex]
                : default;
        }

        public T Get(int entity)
        {
            return _array != null && entity >= _startIndex && entity <= _endIndex
                ? _array[entity - _startIndex]
                : default;
        }

        public void Set(int entity, T value)
        {
            if (_array == null)
            {
                _startIndex = _endIndex = entity;
                _array = new[] {value};
                return;
            }

            if (entity < _startIndex)
            {
                this.AddLeft(entity, value);
                return;
            }

            if (entity > _endIndex)
            {
                this.AddRight(entity, value);
                return;
            }

            _array[entity - _startIndex] = value;
        }

        //NOTIFY ATOMIC ENTITIES! 
        public bool Put(int entity, T value)
        {
            if (_array == null)
            {
                _startIndex = _endIndex = entity;
                _array = new[] {value};
                return true;
            }
            
            if (entity < _startIndex)
            {
                this.AddLeft(entity, value);
                return true;
            }

            if (entity > _endIndex)
            {
                this.AddRight(entity, value);
                return true;
            }

            entity -= _startIndex;

            if (_array[entity] != null)
            {
                return false;
            }

            _array[entity] = value;
            //Notify about added Admin
            return true;
        }

        public bool Del(int entity)
        {
            if (_array == null)
            {
                return false;
            }
            
            if (entity < _startIndex || entity > _endIndex)
            {
                return false;
            }

            entity -= _startIndex;
            if (_array[entity] != null)
            {
                _array[entity] = default;
                return true;
            }

            return false;
        }

        public bool Has(int entity)
        {
            return _array != null && 
                   entity >= _startIndex && entity <= _endIndex &&
                   _array[entity - _startIndex] != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddLeft(int index, T value)
        {
            int diff = _startIndex - index;

            int previousSize = _array.Length;
            var newArray = new T[previousSize + diff];

            for (int i = 0; i < previousSize; i++)
            {
                newArray[i + diff] = _array[i];
            }

            newArray[0] = value;

            _array = newArray;
            _startIndex = index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddRight(int index, T value)
        {
            int diff = index - _endIndex;

            int prevLength = _array.Length;
            int newLength = prevLength + diff;
            T[] newArray = new T[newLength];

            for (int i = 0; i < prevLength; i++)
            {
                newArray[i] = _array[i];
            }

            newArray[newLength - 1] = value;

            _array = newArray;
            _endIndex = index;
        }

        object IAtomicValuePool.Get(int index)
        {
            return this.Get(index);
        }

        void IAtomicValuePool.Set(int entity, object value)
        {
            this.Set(entity, (T) value);
        }

        bool IAtomicValuePool.Put(int entity, object value)
        {
            return this.Put(entity, (T) value);
        }
    }
}