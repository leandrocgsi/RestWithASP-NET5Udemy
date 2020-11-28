using RestWithASPNETUdemy.Model;

namespace RestWithASPNETUdemy.Business
{
    public interface ILoginBusiness
    {
         object FindByLogin(UserVO user);
    }
}
