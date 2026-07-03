using BookLendingSystem.Model;
using MySqlConnector;
using System.Data;

namespace BookLendingSystem.DAL
{
    public class BorrowersDAL
    {
        public List<Borrowers> GetAllBorrowers()
        {
            List<Borrowers> list = new List<Borrowers>();
            string sql = "SELECT * FROM Borrowers";
            DataTable dt = DBHelper.ExecuteQuery(sql);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(ConvertToModel(row));
            }
            return list;
        }

        public Borrowers GetBorrowerById(int id)
        {
            string sql = "SELECT * FROM Borrowers WHERE id = @id";
            DataTable dt = DBHelper.ExecuteQuery(sql, new MySqlParameter("@id", id));
            if (dt.Rows.Count > 0)
            {
                return ConvertToModel(dt.Rows[0]);
            }
            return null;
        }

        public Borrowers GetBorrowerByIDCard(string idCard)
        {
            string sql = "SELECT * FROM Borrowers WHERE IDCard = @idCard";
            DataTable dt = DBHelper.ExecuteQuery(sql, new MySqlParameter("@idCard", idCard));
            if (dt.Rows.Count > 0)
            {
                return ConvertToModel(dt.Rows[0]);
            }
            return null;
        }

        public Borrowers GetBorrowerByCode(int code)
        {
            string sql = "SELECT * FROM Borrowers WHERE borrowing_code = @code";
            DataTable dt = DBHelper.ExecuteQuery(sql, new MySqlParameter("@code", code));
            if (dt.Rows.Count > 0)
            {
                return ConvertToModel(dt.Rows[0]);
            }
            return null;
        }

        public List<Borrowers> SearchBorrowers(string keyword)
        {
            List<Borrowers> list = new List<Borrowers>();
            string sql = "SELECT * FROM Borrowers WHERE borrowers_name LIKE @keyword OR IDCard LIKE @keyword OR tel LIKE @keyword";
            DataTable dt = DBHelper.ExecuteQuery(sql, new MySqlParameter("@keyword", "%" + keyword + "%"));
            foreach (DataRow row in dt.Rows)
            {
                list.Add(ConvertToModel(row));
            }
            return list;
        }

        public int AddBorrower(Borrowers borrower)
        {
            string sql = "INSERT INTO Borrowers(borrowers_name, IDCard, sex, tel, borrowing_code, price, Order_number, remark) VALUES(@name, @idCard, @sex, @tel, @code, @price, @orderNumber, @remark)";
            return DBHelper.ExecuteNonQuery(sql,
                new MySqlParameter("@name", borrower.BorrowersName),
                new MySqlParameter("@idCard", borrower.IDCard),
                new MySqlParameter("@sex", borrower.Sex),
                new MySqlParameter("@tel", borrower.Tel),
                new MySqlParameter("@code", borrower.BorrowingCode.HasValue ? (object)borrower.BorrowingCode.Value : DBNull.Value),
                new MySqlParameter("@price", borrower.Price),
                new MySqlParameter("@orderNumber", borrower.OrderNumber),
                new MySqlParameter("@remark", borrower.Remark));
        }

        public int UpdateBorrower(Borrowers borrower)
        {
            string sql = "UPDATE Borrowers SET borrowers_name=@name, IDCard=@idCard, sex=@sex, tel=@tel, borrowing_code=@code, price=@price, Order_number=@orderNumber, remark=@remark WHERE id=@id";
            return DBHelper.ExecuteNonQuery(sql,
                new MySqlParameter("@id", borrower.Id),
                new MySqlParameter("@name", borrower.BorrowersName),
                new MySqlParameter("@idCard", borrower.IDCard),
                new MySqlParameter("@sex", borrower.Sex),
                new MySqlParameter("@tel", borrower.Tel),
                new MySqlParameter("@code", borrower.BorrowingCode.HasValue ? (object)borrower.BorrowingCode.Value : DBNull.Value),
                new MySqlParameter("@price", borrower.Price),
                new MySqlParameter("@orderNumber", borrower.OrderNumber),
                new MySqlParameter("@remark", borrower.Remark));
        }

        public int DeleteBorrower(int id)
        {
            string sql = "DELETE FROM Borrowers WHERE id = @id";
            return DBHelper.ExecuteNonQuery(sql, new MySqlParameter("@id", id));
        }

        private Borrowers ConvertToModel(DataRow row)
        {
            return new Borrowers
            {
                Id = Convert.ToInt32(row["id"]),
                BorrowersName = row["borrowers_name"]?.ToString() ?? string.Empty,
                IDCard = row["IDCard"]?.ToString() ?? string.Empty,
                Sex = row["sex"]?.ToString() ?? "男",
                Tel = row["tel"]?.ToString() ?? string.Empty,
                BorrowingCode = row["borrowing_code"] != DBNull.Value ? Convert.ToInt32(row["borrowing_code"]) : null,
                Price = Convert.ToInt32(row["price"]),
                OrderNumber = row["Order_number"]?.ToString() ?? string.Empty,
                Remark = Convert.ToInt32(row["remark"])
            };
        }
    }
}