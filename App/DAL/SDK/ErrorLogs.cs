
namespace DAL.SDK
{
    using DAL.Entities;
    using DAL.Repositories;

    public class ErrorLogs
    {
        private static IErrorLogRepository _repository;

        static ErrorLogs()
        {
            _repository = new Repository();
        }

        public void AddErrorLog(ErrorLog errorLog)
        {
            _repository.AddErrorLog(errorLog);
        }
    }
}
