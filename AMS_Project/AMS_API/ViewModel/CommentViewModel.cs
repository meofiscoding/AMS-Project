using BusinessObject.DataAccess;

namespace AMS_API.ViewModel
{
    public class CommentViewModel
    {
        public string? Content { get; set; }
        public int PostId { get; set; }
        public User? User { get; set; }
    }
}
