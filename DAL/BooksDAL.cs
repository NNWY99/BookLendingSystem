using BookLendingSystem.Model;
using MySqlConnector;
using System.Data;

namespace BookLendingSystem.DAL
{
    public class BooksDAL
    {
        public List<Books> GetAllBooks()
        {
            return ExecuteQueryToList("SELECT * FROM Books", ConvertToModel);
        }

        public Books GetBookById(int id)
        {
            return ExecuteQuerySingle("SELECT * FROM Books WHERE id = @id", ConvertToModel, 
                new MySqlParameter("@id", id));
        }

        public List<Books> SearchBooks(string keyword)
        {
            return ExecuteQueryToList(
                "SELECT * FROM Books WHERE bookName LIKE @keyword OR author LIKE @keyword OR category LIKE @keyword",
                ConvertToModel,
                new MySqlParameter("@keyword", "%" + keyword + "%"));
        }

        public int AddBook(Books book)
        {
            string sql = "INSERT INTO Books(barCode, bookName, category, author, publishingHouse, publicationDate, loansNumber, TotalNumber, remark, description, image_path) VALUES(@barCode, @bookName, @category, @author, @publishingHouse, @publicationDate, @loansNumber, @totalNumber, @remark, @description, @image_path)";
            return DBHelper.ExecuteNonQuery(sql,
                new MySqlParameter("@barCode", book.BarCode),
                new MySqlParameter("@bookName", book.BookName),
                new MySqlParameter("@category", book.Category),
                new MySqlParameter("@author", book.Author),
                new MySqlParameter("@publishingHouse", book.PublishingHouse),
                new MySqlParameter("@publicationDate", book.PublicationDate),
                new MySqlParameter("@loansNumber", book.LoansNumber),
                new MySqlParameter("@totalNumber", book.TotalNumber),
                new MySqlParameter("@remark", book.Remark),
                new MySqlParameter("@description", book.Description),
                new MySqlParameter("@image_path", book.ImagePath));
        }

        public int UpdateBook(Books book)
        {
            string sql = "UPDATE Books SET barCode=@barCode, bookName=@bookName, category=@category, author=@author, publishingHouse=@publishingHouse, publicationDate=@publicationDate, loansNumber=@loansNumber, TotalNumber=@totalNumber, remark=@remark, description=@description, image_path=@image_path WHERE id=@id";
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
                new MySqlParameter("@remark", book.Remark),
                new MySqlParameter("@description", book.Description),
                new MySqlParameter("@image_path", book.ImagePath));
        }

        public int DeleteBook(int id)
        {
            return DBHelper.ExecuteNonQuery("DELETE FROM Books WHERE id = @id", 
                new MySqlParameter("@id", id));
        }

        public List<string> GetAllCategories()
        {
            List<string> categories = new List<string>();
            DataTable dt = DBHelper.ExecuteQuery("SELECT DISTINCT category FROM Books ORDER BY category");
            foreach (DataRow row in dt.Rows)
            {
                categories.Add(row["category"]?.ToString() ?? string.Empty);
            }
            return categories;
        }

        public List<Books> GetBooksByCategory(string category)
        {
            return ExecuteQueryToList("SELECT * FROM Books WHERE category = @category", ConvertToModel,
                new MySqlParameter("@category", category));
        }

        private List<Books> ExecuteQueryToList(string sql, Func<DataRow, Books> convertFunc, params MySqlParameter[] parameters)
        {
            List<Books> list = new List<Books>();
            DataTable dt = DBHelper.ExecuteQuery(sql, parameters);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(convertFunc(row));
            }
            return list;
        }

        private Books ExecuteQuerySingle(string sql, Func<DataRow, Books> convertFunc, params MySqlParameter[] parameters)
        {
            DataTable dt = DBHelper.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count > 0)
            {
                return convertFunc(dt.Rows[0]);
            }
            return null;
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
                Remark = Convert.ToInt32(row["remark"]),
                Description = row["description"]?.ToString() ?? string.Empty,
                ImagePath = row["image_path"]?.ToString() ?? string.Empty
            };
        }
    }
}