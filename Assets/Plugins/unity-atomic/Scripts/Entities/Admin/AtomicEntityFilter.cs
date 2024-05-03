using System.Collections;
using System.Collections.Generic;

namespace Atomic
{
    public class AtomicEntityFilter
    {
        public Enumerator GetEnumerator()
        {
            
        }
        
        public struct Enumerator : IEnumerator<int>
        {
            public bool MoveNext()
            {
                
            }

            public void Reset()
            {
                thrownew System.NotImplementedException();
            }

            object IEnumerator.Current => Current;

            public int Current { get; }
            public void Dispose()
            {
                
            }
        }

        public void WithType(int gameObject)
        {
            
        }
        
        public void WithValue(int gameObject)
        {
            
        }
    }
}