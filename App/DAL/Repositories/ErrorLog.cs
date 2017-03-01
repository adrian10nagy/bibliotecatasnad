
namespace DAL.Repositories
{
    using DAL.Entities;
    using System.Data.SqlClient;
    
    public interface IErrorLogRepository
    {
        void AddErrorLog(ErrorLog errorLog);
    }

     public partial class Repository : IErrorLogRepository
    {
         public void AddErrorLog(ErrorLog errorLog)
        {
            _dbRead.ExecuteNonQuery(
                "ErrorLogsAdd",
            new[] { 
                new SqlParameter("@message", errorLog.Message),
                new SqlParameter("@created", errorLog.CreatedDate),
            });
        }
    }
}
