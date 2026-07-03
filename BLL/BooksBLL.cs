using BookLendingSystem.DAL;
using BookLendingSystem.Model;

namespace BookLendingSystem.BLL
{
    public class BooksBLL
    {
        private BooksDAL booksDAL = new BooksDAL();

        public List<Books> GetAllBooks()
        {
            return booksDAL.GetAllBooks();
        }

        public Books GetBookById(int id)
        {
            return booksDAL.GetBookById(id);
        }

        public List<Books> SearchBooks(string keyword)
        {
            return booksDAL.SearchBooks(keyword);
        }

        public bool AddBook(Books book)
        {
            return booksDAL.AddBook(book) > 0;
        }

        public bool UpdateBook(Books book)
        {
            return booksDAL.UpdateBook(book) > 0;
        }

        public bool DeleteBook(int id)
        {
            return booksDAL.DeleteBook(id) > 0;
        }

        public List<string> GetAllCategories()
        {
            return booksDAL.GetAllCategories();
        }

        public List<Books> GetBooksByCategory(string category)
        {
            return booksDAL.GetBooksByCategory(category);
        }

        public bool DecreaseLoanNumber(int bookId)
        {
            Books book = booksDAL.GetBookById(bookId);
            if (book != null && book.LoansNumber > 0)
            {
                book.LoansNumber--;
                return booksDAL.UpdateBook(book) > 0;
            }
            return false;
        }

        public bool IncreaseLoanNumber(int bookId)
        {
            Books book = booksDAL.GetBookById(bookId);
            if (book != null)
            {
                book.LoansNumber++;
                return booksDAL.UpdateBook(book) > 0;
            }
            return false;
        }
    }
}