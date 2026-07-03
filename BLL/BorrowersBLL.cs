using BookLendingSystem.DAL;
using BookLendingSystem.Model;

namespace BookLendingSystem.BLL
{
    public class BorrowersBLL
    {
        private BorrowersDAL borrowersDAL = new BorrowersDAL();

        public List<Borrowers> GetAllBorrowers()
        {
            return borrowersDAL.GetAllBorrowers();
        }

        public Borrowers GetBorrowerById(int id)
        {
            return borrowersDAL.GetBorrowerById(id);
        }

        public Borrowers GetBorrowerByIDCard(string idCard)
        {
            return borrowersDAL.GetBorrowerByIDCard(idCard);
        }

        public Borrowers GetBorrowerByCode(int code)
        {
            return borrowersDAL.GetBorrowerByCode(code);
        }

        public List<Borrowers> SearchBorrowers(string keyword)
        {
            return borrowersDAL.SearchBorrowers(keyword);
        }

        public bool AddBorrower(Borrowers borrower)
        {
            return borrowersDAL.AddBorrower(borrower) > 0;
        }

        public bool UpdateBorrower(Borrowers borrower)
        {
            return borrowersDAL.UpdateBorrower(borrower) > 0;
        }

        public bool DeleteBorrower(int id)
        {
            return borrowersDAL.DeleteBorrower(id) > 0;
        }
    }
}