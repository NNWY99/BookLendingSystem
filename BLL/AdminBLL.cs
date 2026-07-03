using BookLendingSystem.DAL;
using BookLendingSystem.Model;

namespace BookLendingSystem.BLL
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public Admin Admin { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsLocked { get; set; }
        public int RemainingAttempts { get; set; }
    }

    public class AdminBLL
    {
        private AdminDAL adminDAL = new AdminDAL();
        private const int MaxFailCount = 5;
        private const int LockDurationMinutes = 15;

        public LoginResult Login(string account, string password)
        {
            Admin admin = adminDAL.GetAdminByAccount(account);
            if (admin == null)
            {
                return new LoginResult { Success = false, Message = "账号不存在" };
            }

            if (admin.IsLocked)
            {
                if (admin.LastFailTime.HasValue)
                {
                    TimeSpan elapsed = DateTime.Now - admin.LastFailTime.Value;
                    if (elapsed.TotalMinutes >= LockDurationMinutes)
                    {
                        adminDAL.UnlockAccount(admin.Id);
                        admin.IsLocked = false;
                        admin.FailCount = 0;
                    }
                    else
                    {
                        double remainingMinutes = LockDurationMinutes - elapsed.TotalMinutes;
                        return new LoginResult
                        {
                            Success = false,
                            IsLocked = true,
                            Message = $"账号已被锁定，请等待 {Math.Ceiling(remainingMinutes)} 分钟后重试"
                        };
                    }
                }
                else
                {
                    adminDAL.UnlockAccount(admin.Id);
                    admin.IsLocked = false;
                    admin.FailCount = 0;
                }
            }

            if (admin.AdminPassword == password)
            {
                adminDAL.UpdateLastLoginTime(admin.Id, DateTime.Now);
                return new LoginResult { Success = true, Admin = admin };
            }

            int newFailCount = admin.FailCount + 1;
            bool shouldLock = newFailCount >= MaxFailCount;
            DateTime failTime = DateTime.Now;

            adminDAL.UpdateLoginFail(admin.Id, newFailCount, failTime, shouldLock);

            if (shouldLock)
            {
                return new LoginResult
                {
                    Success = false,
                    IsLocked = true,
                    Message = $"密码错误 {MaxFailCount} 次，账号已被锁定，请等待 {LockDurationMinutes} 分钟"
                };
            }

            int remainingAttempts = MaxFailCount - newFailCount;
            return new LoginResult
            {
                Success = false,
                Message = $"账号或密码错误，还剩 {remainingAttempts} 次尝试机会",
                RemainingAttempts = remainingAttempts
            };
        }

        public bool Register(string name, string account, string password)
        {
            if (adminDAL.CheckAccountExists(account))
            {
                return false;
            }

            Admin admin = new Admin
            {
                AdminName = name,
                AdminAccount = account,
                AdminPassword = password,
                CreateTime = DateTime.Now,
                FailCount = 0,
                IsLocked = false
            };

            return adminDAL.AddAdmin(admin);
        }

        public List<Admin> GetAllAdmins()
        {
            return adminDAL.GetAllAdmins();
        }

        public bool UnlockAdmin(int id)
        {
            return adminDAL.UnlockAccount(id);
        }
    }
}