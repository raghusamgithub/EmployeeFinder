using System;
using System.Collections.Generic;
using System.Text;

namespace AWE.Employee.DAL.Entities
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public string MessageName { get; set; }
        public string MessageDesc { get; set; }
        public int? MessageCatId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserId { get; set; }

    }
}
