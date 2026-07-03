namespace BookLendingSystem.Model
{
    public class Borrowers
    {
        public int Id { get; set; }
        public string BorrowersName { get; set; } = string.Empty;
        public string IDCard { get; set; } = string.Empty;
        public string Sex { get; set; } = "男";
        public string Tel { get; set; } = string.Empty;
        public int? BorrowingCode { get; set; }
        public int Price { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public int Remark { get; set; } = 1;
    }
}