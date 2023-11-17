using RestWithASPNETErudio.Model;

namespace RestWithASPNETErudio.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(long id);
        List<Person> FindByName(string firstName, string secondName);
    }
}
