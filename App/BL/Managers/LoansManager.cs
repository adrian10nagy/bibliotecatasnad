
namespace BL.Managers
{
    using DAL.SDK;

    public static class LoansManager
    {
        public static int GetLoansNrAll()
        {
            return Kit.Instance.Loans.GetLoanCount();
        }
    }
}
