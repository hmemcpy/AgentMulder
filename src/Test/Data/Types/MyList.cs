using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TestApplication.Types
{
    public class MyList<T> : IEnumerable<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            return Enumerable.Empty<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}