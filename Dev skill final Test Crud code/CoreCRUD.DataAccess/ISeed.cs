using System.Threading.Tasks;

namespace CoreCRUD.DataAccess
{
    public interface ISeed
    {
        Task MigrateAsync();
        Task SeedAsync();
    }
}
