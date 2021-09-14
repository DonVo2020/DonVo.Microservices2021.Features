using AutoMapper;
using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class UtilService : IUtilService
    {
        private readonly FactoryManagementContext _context;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public UtilService(FactoryManagementContext context, ILoggerManager loggerManager, IMapper mapper)
        {
            _context = context;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public async Task<string> GetUniqueId(string TableName)
        {
            long rowCount;
            string output;

            switch (TableName)
            {
                case "Customer":
                    rowCount = await _context.Customer.AsQueryable().LongCountAsync();
                    output = "CUST" + rowCount.ToString();
                    break;
                case "Phone":
                    rowCount = await _context.Phone.AsQueryable().LongCountAsync();
                    output = "PH" + rowCount.ToString();
                    break;
                case "Address":
                    rowCount = await _context.Address.AsQueryable().LongCountAsync();
                    output = "ADD" + rowCount.ToString();
                    break;
                default:
                    output = "NOT_DEFINED";
                    break;
            }

            return output;
        }

        public IMapper GetMapper()
        {
            return _mapper;
        }

        public ILoggerManager GetLogger()
        {
            return _loggerManager;
        }
        public void Log(string message)
        {
            _loggerManager.LogInfo("---------------------------------------------------------------------------------------------------------");
            _loggerManager.LogInfo("--------------------------" + DateTime.Now.ToString() + "------------------------------------------------");
            _loggerManager.LogInfo(message);
        }
        public void LogInfo(string message)
        {
            _loggerManager.LogInfo("---------------------------------------------------------------------------------------------------------");
            _loggerManager.LogInfo("--------------------------" + DateTime.Now.ToString() + "------------------------------------------------");
            _loggerManager.LogInfo(message);
        }
        public void LogDebug(string message)
        {
            _loggerManager.LogDebug("---------------------------------------------------------------------------------------------------------");
            _loggerManager.LogDebug("--------------------------" + DateTime.Now.ToString() + "------------------------------------------------");
            _loggerManager.LogDebug(message);
        }
        public void LogError(string message)
        {
            _loggerManager.LogError("---------------------------------------------------------------------------------------------------------");
            _loggerManager.LogError("--------------------------" + DateTime.Now.ToString() + "------------------------------------------------");
            _loggerManager.LogError(message);
        }
        public void LogWarn(string message)
        {
            _loggerManager.LogWarn("---------------------------------------------------------------------------------------------------------");
            _loggerManager.LogWarn("--------------------------" + DateTime.Now.ToString() + "------------------------------------------------");
            _loggerManager.LogWarn(message);
        }

        public IMapper Mapper {
            get {
                return _mapper;
            }        
        }

        public List<T> ConcatList<T>(List<T> list1,List<T> list2) {

            for (int i = 0; i < list2.Count; i++ ) {
                list1.Add(list2[i]);
            }
            return list1;

        }
    }
}
