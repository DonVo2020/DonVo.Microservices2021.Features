using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts
{
    public interface IUtilService
    {
        IMapper Mapper { get; }
        Task<string> GetUniqueId(string TableName);
        ILoggerManager GetLogger();
        IMapper GetMapper();
        void Log(string message);

        void LogInfo(string message);

        void LogDebug(string message);
        void LogWarn(string message);
        void LogError(string message);

        List<T> ConcatList<T>(List<T> list1, List<T> list2);
    }
}
