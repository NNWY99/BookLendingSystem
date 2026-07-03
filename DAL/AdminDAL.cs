using BookLendingSystem.Model;
using MySqlConnector;
using System.Data;

namespace BookLendingSystem.DAL
{
    public class AdminDAL
    {
        public Admin GetAdminByAccount(string account)
        {
            return ExecuteQuerySingle("SELECT * FROM Admin WHERE admin_account = @account", ConvertToModel,
                new MySqlParameter("@account", account));
        }

        public bool UpdateLastLoginTime(int id, DateTime loginTime)
        {
            string sql = "UPDATE Admin SET last_login_time = @loginTime, fail_count = 0, last_fail_time = NULL WHERE id = @id";
            return DBHelper.ExecuteNonQuery(sql,
                new MySqlParameter("@loginTime", loginTime),
                new MySqlParameter("@id", id)) > 0;
        }

        public bool UpdateLoginFail(int id, int failCount, DateTime failTime, bool isLocked)
        {
            string sql = "UPDATE Admin SET fail_count = @failCount, last_fail_time = @failTime, is_locked = @isLocked WHERE id = @id";
            return DBHelper.ExecuteNonQuery(sql,
                new MySqlParameter("@failCount", failCount),
                new MySqlParameter("@failTime", failTime),
                new MySqlParameter("@isLocked", isLocked),
                new MySqlParameter("@id", id)) > 0;
        }

        public bool UnlockAccount(int id)
        {
            return DBHelper.ExecuteNonQuery("UPDATE Admin SET is_locked = 0, fail_count = 0, last_fail_time = NULL WHERE id = @id",
                new MySqlParameter("@id", id)) > 0;
        }

        public bool CheckAccountExists(string account)
        {
            object result = DBHelper.ExecuteScalar("SELECT COUNT(*) FROM Admin WHERE admin_account = @account",
                new MySqlParameter("@account", account));
            return result != null && Convert.ToInt32(result) > 0;
        }

        public bool AddAdmin(Admin admin)
        {
            string sql = "INSERT INTO Admin (admin_name, admin_account, admin_password, create_time) VALUES (@name, @account, @password, @createTime)";
            return DBHelper.ExecuteNonQuery(sql,
                new MySqlParameter("@name", admin.AdminName),
                new MySqlParameter("@account", admin.AdminAccount),
                new MySqlParameter("@password", admin.AdminPassword),
                new MySqlParameter("@createTime", admin.CreateTime ?? DateTime.Now)) > 0;
        }

        public List<Admin> GetAllAdmins()
        {
            return ExecuteQueryToList("SELECT * FROM Admin", ConvertToModel);
        }

        private List<Admin> ExecuteQueryToList(string sql, Func<DataRow, Admin> convertFunc, params MySqlParameter[] parameters)
        {
            List<Admin> list = new List<Admin>();
            DataTable dt = DBHelper.ExecuteQuery(sql, parameters);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(convertFunc(row));
            }
            return list;
        }

        private Admin ExecuteQuerySingle(string sql, Func<DataRow, Admin> convertFunc, params MySqlParameter[] parameters)
        {
            DataTable dt = DBHelper.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count > 0)
            {
                return convertFunc(dt.Rows[0]);
            }
            return null;
        }

        private Admin ConvertToModel(DataRow row)
        {
            return new Admin
            {
                Id = Convert.ToInt32(row["id"]),
                AdminName = row["admin_name"]?.ToString() ?? string.Empty,
                AdminAccount = row["admin_account"]?.ToString() ?? string.Empty,
                AdminPassword = row["admin_password"]?.ToString() ?? string.Empty,
                CreateTime = row["create_time"] != DBNull.Value ? Convert.ToDateTime(row["create_time"]) : null,
                LastLoginTime = row["last_login_time"] != DBNull.Value ? Convert.ToDateTime(row["last_login_time"]) : null,
                FailCount = row["fail_count"] != DBNull.Value ? Convert.ToInt32(row["fail_count"]) : 0,
                LastFailTime = row["last_fail_time"] != DBNull.Value ? Convert.ToDateTime(row["last_fail_time"]) : null,
                IsLocked = row["is_locked"] != DBNull.Value && Convert.ToBoolean(row["is_locked"])
            };
        }
    }
}