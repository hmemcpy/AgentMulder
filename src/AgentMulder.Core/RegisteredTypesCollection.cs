using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AgentMulder.Core
{
    public class RegisteredTypesCollection : ICollection<Registration>
    {
        private readonly List<Registration> registeredTypes = new List<Registration>();
        
        public IEnumerator<Registration> GetEnumerator()
        {
            return registeredTypes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Registration item)
        {
            registeredTypes.Add(item);
        }

        public void Clear()
        {
            registeredTypes.Clear();
        }

        public bool Contains(Registration item)
        {
            return registeredTypes.Contains(item);
        }

        public void CopyTo(Registration[] array, int arrayIndex)
        {
            registeredTypes.CopyTo(array, arrayIndex);
        }

        public bool Remove(Registration item)
        {
            return registeredTypes.Remove(item);
        }

        public int Count
        {
            get { return registeredTypes.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public Registration Match(string typeName)
        {
            Registration result = registeredTypes.FirstOrDefault(registration => registration.TypeName == typeName);

            return result;
        }

        public void AddRange(IEnumerable<Registration> registrations)
        {
            foreach (var registration in registrations)
            {
                registeredTypes.Add(registration);
            }
        }
    }
}