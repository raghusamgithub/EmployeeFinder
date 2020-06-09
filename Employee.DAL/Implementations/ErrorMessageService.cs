
using AWE.Employee.DAL.Abstractions;
using AWE.Employee.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Employee.Common.Enum;
using AWE.Employee.Common;

namespace AWE.Employee.DAL.DBContext.Implementations
{
    public class ErrorMessageService : IErrorMessageService
    {
        private readonly IRepository<Message> _messageRepository;
        private List<Message> messages;

        public ErrorMessageService(IRepository<Message> messageRepository)
        {
            _messageRepository = messageRepository;
            messages = _messageRepository.TableNoTracking.ToList();
        }       

        public string GetErrorMessages(ErrorMessage errorMessage, params string[] otherCaption)
        {
            try
            {
                return string.Format((messages.Where(x => x.MessageName == errorMessage.ToString()).FirstOrDefault() ?? new Message()).MessageDesc, otherCaption);
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
