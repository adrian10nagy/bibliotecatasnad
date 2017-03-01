
namespace BL.Managers
{
    using DAL.Entities;
    using DAL.SDK;
    using System;

    public static class LogsManager
    {
        public static void LogError(ErrorLog errorLog)
        {
            if (errorLog.CreatedDate != DateTime.MinValue && !string.IsNullOrEmpty(errorLog.Message))
            {
                Kit.Instance.ErrorLogs.AddErrorLog(errorLog);
            }
        }
    }
}
