namespace BookLendingSystem.Model
{
    public class Borrow
    {
        public int Id { get; set; }
        public int? AdminId { get; set; }
        public int? BorrowersId { get; set; }
        public int Num { get; set; } = 0;
    }
}