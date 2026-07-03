using BookLendingSystem.Model;
using MySqlConnector;
using System.Data;

namespace BookLendingSystem.DAL
{
    public class BorrowersDAL
    {
        public List<Borrowers> GetAllBorrowers()
        {
            return ExecuteQueryToList("SELECT * FROM Borrowers", ConvertToModel);
        }

        public Borrowers GetBorrowerById(int id)
        {
            return ExecuteQuerySingle("SELECT * FROM Borrowers WHERE id = @id", ConvertToModel,
                new MySqlParameter("@id", id));
        }

        public Borrowers GetBorrowerByIDCard(string idCard)
        {
            return ExecuteQuerySingle("SELECT * FROM Borrowers WHERE IDCard = @idCard", ConvertToModel,
                new MySqlParameter("@idCard", idCard));
        }

        public Borrowers GetBorrowerByCode(int code)
        {
            return ExecuteQuerySingle("SELECT * FROM Borrowers WHERE borrowing_code = @code", ConvertToModel,
                new MySqlParameter("@code", code));
        }

        public List<Borrowers> SearchBorrowers(string keyword)
        {
            return ExecuteQueryToList(
                "SELECT * FROM Borrowers WHERE borrowers_name LIKE @keyword OR IDCard LIKE @keyword OR tel LIKE @keyword",
                ConvertToModel,
                new MySqlParameter("@keyword", "%" + keyword + "%"));
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
            return DBHelper.ExecuteNonQuery("DELETE FROM Borrowers WHERE id = @id",
                new MySqlParameter("@id", id));
        }

        private List<Borrowers> ExecuteQueryToList(string sql, Func<DataRow, Borrowers> convertFunc, params MySqlParameter[] parameters)
        {
            List<Borrowers> list = new List<Borrowers>();
            DataTable dt = DBHelper.ExecuteQuery(sql, parameters);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(convertFunc(row));
            }
            return list;
        }

        private Borrowers ExecuteQuerySingle(string sql, Func<DataRow, Borrowers> convertFunc, params MySqlParameter[] parameters)
        {
            DataTable dt = DBHelper.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count > 0)
            {
                return convertFunc(dt.Rows[0]);
            }
            return null;
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