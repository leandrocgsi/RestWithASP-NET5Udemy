using RestWithASPNETErudio.Data.VO;

namespace RestWithASPNETErudio.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredentials(UserVO user);

        TokenVO ValidateCredentials(TokenVO token);

        bool RevokeToken(string userName);
    }
}
