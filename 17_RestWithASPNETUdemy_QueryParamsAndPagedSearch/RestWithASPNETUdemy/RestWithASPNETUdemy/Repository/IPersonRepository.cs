using RestWithASPNETUdemy.Model;

namespace RestWithASPNETUdemy.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(long id);
    }
}
