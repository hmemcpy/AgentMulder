using System.Collections.Generic;

namespace TestApplication.Types
{
    public class Repository<T, T1>
    {
        public IEnumerable<T1> GetAll()
        {
            yield break;
        }
    }
}