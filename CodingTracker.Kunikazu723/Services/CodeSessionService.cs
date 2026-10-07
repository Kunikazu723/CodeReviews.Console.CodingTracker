using CodingTracker.Kunikazu723.Dao;
using CodingTracker.Kunikazu723.Models;
using CodingTracker.Kunikazu723.UserLand;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Kunikazu723.Services
{
    internal class CodeSessionService : IService
    {
        private readonly IDao<CodingSession> _sessionDao;
        private readonly UserInput _userInput;
        public CodeSessionService(IDao<CodingSession> sessionDao, UserInput userInput)
        {
            _sessionDao = sessionDao;
            _userInput = userInput;
        }
        public void AddItem()
        {
            (string startTime, string endTime) = _userInput.GetStartEndDate();

            var codingSession = new CodingSession()
            {
                StartTime = startTime,
                EndTime = endTime
            };

            _sessionDao.InsertItem(codingSession);
        }

        public void DeleteItem()
        {
            throw new NotImplementedException();
        }

        public void UpdateItem()
        {
            throw new NotImplementedException();
        }

        public void ViewAllItems()
        {
            throw new NotImplementedException();
        }
    }
}
