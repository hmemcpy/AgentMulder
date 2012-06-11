using System.Linq;

namespace TestApplication.Types
{
    public class PageRepository : Repository<Context, Page>, IPageRepository   // Class PageRepository is never instantiated
    {
        public Page GetSingle(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
    }
}