using RestWithASPNETErudio.Model;
using System.Collections.Generic;

namespace RestWithASPNETErudio.Business
{
    public interface IPersonBusiness
    {
        Person Create(Person person);
        Person FindByID(long id);
        List<Person> FindAll();
        Person Update(Person person);
        void Delete(long id);
    }
}
