using RestWithASPNETUdemy.Model;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Business
{
    public interface IUserRepository
    {
        User FindByLogin(string login);
    }
}
