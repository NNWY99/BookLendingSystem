using BookLendingSystem.Model;
using MySqlConnector;
using System.Data;

namespace BookLendingSystem.DAL
{
    public class BooksDAL
    {
        public List<Books> GetAllBooks()
        {
            List<Books> list = new List<Books>();
            string sql = "SELECT * FROM Books";
            DataTable dt = DBHelper.ExecuteQuery(sql);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(ConvertToModel(row));
            }
            return list;
        }

        public Books GetBookById(int id)
        {
            string sql = "SELECT * FROM Books WHERE id = @id";
            DataTable dt = DBHelper.ExecuteQuery(sql, new MySqlParameter("@id", id));
            if (dt.Rows.Count > 0)
            {
                return ConvertToModel(dt.Rows[0]);
            }
            return null;
        }

        public List<Books> SearchBooks(string keyword)
        {
            List<Books> list = new List<Books>();
            string sql = "SELECT * FROM Books WHERE bookName LIKE @keyword OR author LIKE @keyword OR category LIKE @keyword";
            DataTable dt = DBHelper.ExecuteQuery(sql, new MySqlParameter("@keyword", "%" + keyword + "%"));
            foreach (DataRow row in dt.Rows)
            {
                list.Add(ConvertToModel(row));
            }
            return list;
        }

        public int AddBook(Books book)
        {
            string sql = "INSERT INTO Books(barCode, bookName, category, author, publishingHouse, publicationDate, loansNumber, TotalNumber, remark) VALUES(@barCode, @bookName, @category, @author, @publishingHouse, @publicationDate, @loansNumber, @totalNumber, @remark)";
            return DBHelper.ExecuteNonQuery(sql,
                new MySqlParameter("@barCode", book.BarCode),
                new MySqlParameter("@bookName", book.BookName),
                new MySqlParameter("@category", book.Category),
                new MySqlParameter("@author", book.Author),
                new MySqlParameter("@publishingHouse", book.PublishingHouse),
                new MySqlParameter("@publicationDate", book.PublicationDate),
                new MySqlParameter("@loansNumber", book.LoansNumber),
                new MySqlParameter("@totalNumber", book.TotalNumber),
                new MySqlParameter("@remark", book.Remark));
        }

        public int UpdateBook(Books book)
        {
            string sql = "UPDATE Books SET barCode=@barCode, bookName=@bookName, category=@category, author=@author, publishingHouse=@publishingHouse, publicationDate=@publicationDate, loansNumber=@loansNumber, TotalNumber=@totalNumber, remark=@remark WHERE id=@id";
            return DBHelper.ExecuteNonQuery(sql,
                new MySqlParameter("@id", book.Id),
                new MySqlParameter("@barCode", book.BarCode),
                new MySqlParameter("@bookName", book.BookName),
                new MySqlParameter("@category", book.Category),
                new MySqlParameter("@author", book.Author),
                new MySqlParameter("@publishingHouse", book.PublishingHouse),
                new MySqlParameter("@publicationDate", book.PublicationDate),
                new MySqlParameter("@loansNumber", book.LoansNumber),
                new MySqlParameter("@totalNumber", book.TotalNumber),
                new MySqlParameter("@remark", book.Remark));
        }

        public int DeleteBook(int id)
        {
            string sql = "DELETE FROM Books WHERE id = @id";
            return DBHelper.ExecuteNonQuery(sql, new MySqlParameter("@id", id));
        }

        private Books ConvertToModel(DataRow row)
        {
            return new Books
            {
                Id = Convert.ToInt32(row["id"]),
                BarCode = Convert.ToInt32(row["barCode"]),
                BookName = row["bookName"]?.ToString() ?? string.Empty,
                Category = row["category"]?.ToString() ?? string.Empty,
                Author = row["author"]?.ToString() ?? string.Empty,
                PublishingHouse = row["publishingHouse"]?.ToString() ?? string.Empty,
                PublicationDate = Convert.ToDateTime(row["publicationDate"]),
                LoansNumber = Convert.ToInt32(row["loansNumber"]),
                TotalNumber = Convert.ToInt32(row["TotalNumber"]),
                Remark = Convert.ToInt32(row["remark"])
            };
        }
    }
}