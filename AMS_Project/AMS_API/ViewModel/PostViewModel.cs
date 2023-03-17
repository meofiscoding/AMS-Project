namespace AMS_API.ViewModel
{
    public class PostViewModel
    {
            public string Content { get; set; }
            public int ClassId { get; set; }
            public List<IFormFile> Files { get; set; }

    }
}
