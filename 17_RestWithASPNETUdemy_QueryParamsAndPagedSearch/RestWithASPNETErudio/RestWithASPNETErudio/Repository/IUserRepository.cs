using RestWithASPNETErudio.Data.VO;
using RestWithASPNETErudio.Model;

namespace RestWithASPNETErudio.Repository
{
    public interface IUserRepository
    {
        User? ValidateCredentials(UserVO user);

        User? ValidateCredentials(string username);

        bool RevokeToken(string username);

        User? RefreshUserInfo(User user);
    }
}
