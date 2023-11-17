using RestWithASPNETErudio.Model;
using System.Collections.Generic;

namespace RestWithASPNETErudio.Business
{
    public interface IBookBusiness
    {
        Book Create(Book book);
        Book FindByID(long id);
        List<Book> FindAll();
        Book Update(Book book);
        void Delete(long id);
    }
}
