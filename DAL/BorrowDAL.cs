using BookLendingSystem.Model;
using MySqlConnector;
using System.Data;

namespace BookLendingSystem.DAL
{
    public class BorrowDAL
    {
        public int AddBorrow(Borrow borrow)
        {
            string sql = "INSERT INTO Borrow(admin_id, borrowers_id, num) VALUES(@adminId, @borrowersId, @num); SELECT LAST_INSERT_ID();";
            object result = DBHelper.ExecuteScalar(sql,
                new MySqlParameter("@adminId", borrow.AdminId.HasValue ? (object)borrow.AdminId.Value : DBNull.Value),
                new MySqlParameter("@borrowersId", borrow.BorrowersId.HasValue ? (object)borrow.BorrowersId.Value : DBNull.Value),
                new MySqlParameter("@num", borrow.Num));
            return result != DBNull.Value ? Convert.ToInt32(result) : 0;
        }

        public List<Borrow> GetAllBorrows()
        {
            return ExecuteQueryToList("SELECT * FROM Borrow", ConvertToModel);
        }

        private List<Borrow> ExecuteQueryToList(string sql, Func<DataRow, Borrow> convertFunc, params MySqlParameter[] parameters)
        {
            List<Borrow> list = new List<Borrow>();
            DataTable dt = DBHelper.ExecuteQuery(sql, parameters);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(convertFunc(row));
            }
            return list;
        }

        private Borrow ConvertToModel(DataRow row)
        {
            return new Borrow
            {
                Id = Convert.ToInt32(row["id"]),
                AdminId = row["admin_id"] != DBNull.Value ? Convert.ToInt32(row["admin_id"]) : null,
                BorrowersId = row["borrowers_id"] != DBNull.Value ? Convert.ToInt32(row["borrowers_id"]) : null,
                Num = Convert.ToInt32(row["num"])
            };
        }
    }
}