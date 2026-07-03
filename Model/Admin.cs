namespace BookLendingSystem.Model
{
    public class Admin
    {
        public int Id { get; set; }
        public string AdminName { get; set; } = string.Empty;
        public string AdminAccount { get; set; } = string.Empty;
        public string AdminPassword { get; set; } = string.Empty;
        public DateTime? CreateTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public int FailCount { get; set; }
        public DateTime? LastFailTime { get; set; }
        public bool IsLocked { get; set; }
    }
}