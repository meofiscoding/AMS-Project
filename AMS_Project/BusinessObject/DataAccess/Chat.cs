using System;
using System.Collections.Generic;

namespace BusinessObject.DataAccess
{
    public partial class Chat
    {
        public int ChatId { get; set; }
        public int? GroupId { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public string Message { get; set; } = null!;
        public string? FilePath { get; set; }
        public DateTime? SentDate { get; set; }

        public virtual Group? Group { get; set; }
        public virtual User? Receiver { get; set; }
        public virtual User? Sender { get; set; }
    }
}
