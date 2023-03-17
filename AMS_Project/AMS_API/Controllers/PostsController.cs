using System.Security.Claims;
using AMS_API.ViewModel;
using BusinessObject.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AMS_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IClassRepository _classRepository;
        private readonly IPostRepository _postRepository;
        private readonly IResourceRepository _resourceRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;

        public PostsController(IClassRepository classRepository, IPostRepository postRepository, IResourceRepository resourceRepository, ICommentRepository commentRepository, IUserRepository userRepository, IUserRepository userRepository1)
        {
            _classRepository = classRepository;
            _postRepository = postRepository;
            _resourceRepository = resourceRepository;
            _commentRepository = commentRepository;
            _userRepository = userRepository1;
        }

        // GET: api/<PostsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PostsController>/5
        [HttpGet("getbyclass/{classId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<PostViewModel>>> GetPostsAndComments(int classId)
        {
            var posts = await _postRepository.GetPostsByClassId(classId);
             // Retrieve comments for this post
            foreach (var post in posts)
            {
                post.Comments = _commentRepository.GetCommentsByPostId(post.Id);
                post.User = _userRepository.GetUserById(post.UserId);
                //get list resource of this post
                post.Resources = _resourceRepository.GetResourcesByPostId(post.Id);
            }
            return Ok(posts);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromForm] PostViewModel model)
        {
            var @class = _classRepository.GetClassById(model.ClassId);
            if (@class == null)
            {
                return BadRequest("Class not found");
            }

            var userId = GetUserId();

            if (userId == 0)
            {
                return BadRequest("Invalid user email");
            }

            List<Resource> resources = new List<Resource>();
            //looping through the files and save them to a local directory
            foreach (var item in model.Files)
            {
                if (item.Length > 0)
                {
                    var fileName = Path.GetFileName(item.FileName);
                    var directoryPath  = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads");

                    // Create directory if it doesn't exist
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var filePath = Path.Combine(directoryPath, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await item.CopyToAsync(fileStream);
                    }

                    resources.Add(new Resource
                    {
                        ResourceName = fileName,
                        FileUrl = $"~/uploads/{fileName}",
                        Type = item.ContentType,
                    });
                }
            }

            var post = new Post
            {
                ClassId = model.ClassId,
                UserId = userId,
                PostContent = model.Content,
                Resources = resources
            };
            await _postRepository.CreatePost(post);
            return Ok("Post created successfully");
        }

        //get user id from token
        private int GetUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userIdClaim = identity?.Claims.FirstOrDefault(c => c.Type == "ID");
            if (userIdClaim == null)
            {
                return 0;
            }
            else
            {
                return int.Parse(userIdClaim.Value);
            }
        }
    }
}
