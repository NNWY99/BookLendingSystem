using MySqlConnector;
using System.Data;

namespace BookLendingSystem.DAL
{
    public class DBHelper
    {
        private static readonly string connectionString = "server=localhost;database=mybook;uid=root;pwd=20050714;port=3306;charset=utf8;";

        static DBHelper()
        {
            InitializeDatabase();
        }

        private static void InitializeDatabase()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("SHOW COLUMNS FROM Books LIKE 'description'", conn))
                    {
                        if (cmd.ExecuteScalar() == null)
                        {
                            using (var alterCmd = new MySqlCommand("ALTER TABLE Books ADD COLUMN description TEXT DEFAULT NULL COMMENT '图书介绍'", conn))
                            {
                                alterCmd.ExecuteNonQuery();
                            }
                        }
                    }

                    using (var cmd = new MySqlCommand("SHOW COLUMNS FROM Books LIKE 'image_path'", conn))
                    {
                        if (cmd.ExecuteScalar() == null)
                        {
                            using (var alterCmd = new MySqlCommand("ALTER TABLE Books ADD COLUMN image_path VARCHAR(500) DEFAULT NULL COMMENT '图片路径'", conn))
                            {
                                alterCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public static int ExecuteNonQuery(string sql, params MySqlParameter[] parameters)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static object ExecuteScalar(string sql, params MySqlParameter[] parameters)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteScalar();
                }
            }
        }

        public static DataTable ExecuteQuery(string sql, params MySqlParameter[] parameters)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
}