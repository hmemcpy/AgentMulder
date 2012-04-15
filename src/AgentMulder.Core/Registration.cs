namespace AgentMulder.Core
{
    public class Registration
    {
        public string TypeName { get; private set; }

        public Registration(string typeName)
        {
            TypeName = typeName;
        }
    }
}