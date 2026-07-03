namespace BookLendingSystem.Model
{
    public class Books
    {
        public int Id { get; set; }
        public int BarCode { get; set; }
        public string BookName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string PublishingHouse { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
        public int LoansNumber { get; set; }
        public int TotalNumber { get; set; }
        public int Remark { get; set; } = 1;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
    }
}