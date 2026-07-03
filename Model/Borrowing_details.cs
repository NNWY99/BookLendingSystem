namespace BookLendingSystem.Model
{
    public class Borrowing_details
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public int? BorrowId { get; set; }
        public DateTime? LoanTime { get; set; }
        public DateTime CutOffTime { get; set; }
        public DateTime? ReturnTime { get; set; }
    }
}