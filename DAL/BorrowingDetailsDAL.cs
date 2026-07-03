using BookLendingSystem.Model;
using MySqlConnector;
using System.Data;

namespace BookLendingSystem.DAL
{
    public class BorrowingDetailsDAL
    {
        public int AddBorrowingDetail(Borrowing_details detail)
        {
            string sql = "INSERT INTO Borrowing_details(book_id, borrow_id, loanTime, cut_off_time, return_time) VALUES(@bookId, @borrowId, @loanTime, @cutOffTime, @returnTime)";
            return DBHelper.ExecuteNonQuery(sql,
                new MySqlParameter("@bookId", detail.BookId.HasValue ? (object)detail.BookId.Value : DBNull.Value),
                new MySqlParameter("@borrowId", detail.BorrowId.HasValue ? (object)detail.BorrowId.Value : DBNull.Value),
                new MySqlParameter("@loanTime", detail.LoanTime.HasValue ? (object)detail.LoanTime.Value : DBNull.Value),
                new MySqlParameter("@cutOffTime", detail.CutOffTime),
                new MySqlParameter("@returnTime", detail.ReturnTime.HasValue ? (object)detail.ReturnTime.Value : DBNull.Value));
        }

        public List<Borrowing_details> GetAllBorrowingDetails()
        {
            return ExecuteQueryToList("SELECT * FROM Borrowing_details", ConvertToModel);
        }

        public List<Borrowing_details> GetOverdueDetails()
        {
            return ExecuteQueryToList("SELECT * FROM Borrowing_details WHERE return_time IS NULL AND cut_off_time < NOW()", ConvertToModel);
        }

        public List<Borrowing_details> GetActiveBorrowingDetails()
        {
            return ExecuteQueryToList("SELECT * FROM Borrowing_details WHERE return_time IS NULL", ConvertToModel);
        }

        public int UpdateReturnTime(int id, DateTime returnTime)
        {
            return DBHelper.ExecuteNonQuery("UPDATE Borrowing_details SET return_time = @returnTime WHERE id = @id",
                new MySqlParameter("@returnTime", returnTime),
                new MySqlParameter("@id", id));
        }

        public DataTable GetActiveBorrowingDetailsWithInfo()
        {
            string sql = @"
                SELECT bd.id, bk.bookName, br.borrowers_name, br.tel, 
                       bd.loanTime, bd.cut_off_time, bd.return_time
                FROM Borrowing_details bd
                LEFT JOIN Books bk ON bd.book_id = bk.id
                LEFT JOIN Borrow brw ON bd.borrow_id = brw.id
                LEFT JOIN Borrowers br ON brw.borrowers_id = br.id
                WHERE bd.return_time IS NULL";
            return DBHelper.ExecuteQuery(sql);
        }

        public DataTable GetOverdueDetailsWithInfo()
        {
            string sql = @"
                SELECT bd.id, bk.bookName, br.borrowers_name, br.tel, 
                       bd.loanTime, bd.cut_off_time, bd.return_time
                FROM Borrowing_details bd
                LEFT JOIN Books bk ON bd.book_id = bk.id
                LEFT JOIN Borrow brw ON bd.borrow_id = brw.id
                LEFT JOIN Borrowers br ON brw.borrowers_id = br.id
                WHERE bd.return_time IS NULL AND bd.cut_off_time < NOW()";
            return DBHelper.ExecuteQuery(sql);
        }

        private List<Borrowing_details> ExecuteQueryToList(string sql, Func<DataRow, Borrowing_details> convertFunc, params MySqlParameter[] parameters)
        {
            List<Borrowing_details> list = new List<Borrowing_details>();
            DataTable dt = DBHelper.ExecuteQuery(sql, parameters);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(convertFunc(row));
            }
            return list;
        }

        private Borrowing_details ConvertToModel(DataRow row)
        {
            return new Borrowing_details
            {
                Id = Convert.ToInt32(row["id"]),
                BookId = row["book_id"] != DBNull.Value ? Convert.ToInt32(row["book_id"]) : null,
                BorrowId = row["borrow_id"] != DBNull.Value ? Convert.ToInt32(row["borrow_id"]) : null,
                LoanTime = row["loanTime"] != DBNull.Value ? Convert.ToDateTime(row["loanTime"]) : null,
                CutOffTime = Convert.ToDateTime(row["cut_off_time"]),
                ReturnTime = row["return_time"] != DBNull.Value ? Convert.ToDateTime(row["return_time"]) : null
            };
        }
    }
}