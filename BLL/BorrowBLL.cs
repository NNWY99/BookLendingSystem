using BookLendingSystem.DAL;
using BookLendingSystem.Model;
using System.Data;

namespace BookLendingSystem.BLL
{
    public class BorrowBLL
    {
        private BorrowDAL borrowDAL = new BorrowDAL();
        private BorrowingDetailsDAL borrowingDetailsDAL = new BorrowingDetailsDAL();
        private BooksBLL booksBLL = new BooksBLL();

        public bool CreateBorrow(int adminId, int borrowersId, List<int> bookIds, int borrowDays = 30)
        {
            Borrow borrow = new Borrow
            {
                AdminId = adminId,
                BorrowersId = borrowersId,
                Num = bookIds.Count
            };

            int borrowId = borrowDAL.AddBorrow(borrow);
            if (borrowId > 0)
            {
                DateTime cutOffTime = DateTime.Now.AddDays(borrowDays);

                foreach (int bookId in bookIds)
                {
                    if (booksBLL.DecreaseLoanNumber(bookId))
                    {
                        Borrowing_details detail = new Borrowing_details
                        {
                            BookId = bookId,
                            BorrowId = borrowId,
                            LoanTime = DateTime.Now,
                            CutOffTime = cutOffTime,
                            ReturnTime = null
                        };
                        borrowingDetailsDAL.AddBorrowingDetail(detail);
                    }
                }
                return true;
            }
            return false;
        }

        public bool ReturnBook(int detailId)
        {
            Borrowing_details detail = borrowingDetailsDAL.GetAllBorrowingDetails().FirstOrDefault(d => d.Id == detailId);
            if (detail != null && detail.ReturnTime == null)
            {
                borrowingDetailsDAL.UpdateReturnTime(detailId, DateTime.Now);
                if (detail.BookId != null)
                {
                    booksBLL.IncreaseLoanNumber((int)detail.BookId);
                }
                return true;
            }
            return false;
        }

        public List<Borrowing_details> GetOverdueDetails()
        {
            return borrowingDetailsDAL.GetOverdueDetails();
        }

        public List<Borrowing_details> GetActiveBorrowingDetails()
        {
            return borrowingDetailsDAL.GetActiveBorrowingDetails();
        }

        public List<Borrow> GetAllBorrows()
        {
            return borrowDAL.GetAllBorrows();
        }

        public DataTable GetActiveBorrowingDetailsWithInfo()
        {
            return borrowingDetailsDAL.GetActiveBorrowingDetailsWithInfo();
        }

        public DataTable GetOverdueDetailsWithInfo()
        {
            return borrowingDetailsDAL.GetOverdueDetailsWithInfo();
        }
    }
}