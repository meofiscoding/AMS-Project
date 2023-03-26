namespace AMS_API.ViewModel
{
    public class AssignmentViewModel
    {
        public int ClassId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime Deadline { get; set; }
        public List<IFormFile>? Files { get; set; } 
    }
}
