using RestWithASPNETErudio.Data.Converter.Implementations;
using RestWithASPNETErudio.Data.VO;
using RestWithASPNETErudio.Model;
using RestWithASPNETErudio.Repository;

namespace RestWithASPNETErudio.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {

        private readonly IRepository<Book> _repository;

        private readonly BookConverter _converter;

        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }
        
        // Method responsible for returning all people,
        public List<BookVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        // Method responsible for returning one book by ID
        public BookVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }

        // Method responsible to crete one new book
        public BookVO Create(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Create(bookEntity);
            return _converter.Parse(bookEntity);
        }

        // Method responsible for updating one book
        public BookVO Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Update(bookEntity);
            return _converter.Parse(bookEntity);
        }

        // Method responsible for deleting a book from an ID
        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
