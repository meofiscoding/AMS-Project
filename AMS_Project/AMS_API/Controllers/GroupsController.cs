using AMS_API.ViewModel;
using BusinessObject.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AMS_API.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupStudentRepository _groupStudentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;

        public GroupsController(IGroupRepository groupRepository, IGroupStudentRepository groupStudentRepository, IUserRepository userRepository, IChatRepository chatRepository)
        {
            _groupRepository = groupRepository;
            _groupStudentRepository = groupStudentRepository;
            _userRepository = userRepository;
            _chatRepository = chatRepository;
        }

        //GET /groups?classId=1
        [HttpGet]
        // [CustomAuthorize("Teacher", typeof(IUserRepository))]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Get(int classId)
        {
            try
            {
                //get group and list student of each group
                List<Group> groups = _groupRepository.GetGroupsByClassId(classId);
                foreach (var group in groups)
                {
                    var groupStudents = _groupStudentRepository.GetGroupStudentsByGroupId(group.Id);
                    group.GroupStudents = groupStudents;
                }
                return Ok(groups);
            }
            catch (System.Exception ex)
            {
                return Conflict(ex.Message);
            }
        }


        // POST api/<GroupsController>
        [HttpPost("addGroup")]
        // [CustomAuthorize("Teacher", typeof(IUserRepository))]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post(AddGroupViewModel addGroupViewModel)
        {
            try
            {
                //check if group already exists
                bool groupExists = _groupRepository.CheckGroupExists(addGroupViewModel.Groups.Keys, addGroupViewModel.ClassId);
                if (groupExists)
                {
                    return BadRequest("Group already exists in this class");
                }
                //list groupId
                var groupId = new List<int>();
                //add each group in Group
                foreach (var group in addGroupViewModel.Groups.Keys)
                {
                    int groupID = _groupRepository.CreateGroup(group, addGroupViewModel.ClassId);
                    groupId.Add(groupID);
                }

                //add each student and groupId to groupStudent
                for (int i = 0; i < addGroupViewModel.Groups.Keys.Count; i++)
                {
                    foreach (var student in addGroupViewModel.Groups[addGroupViewModel.Groups.Keys.ElementAt(i)])
                    {
                        //get student id
                        int studentId = _userRepository.GetUserByEmail(student).Id;
                        await _groupStudentRepository.CreateGroupStudent(studentId, groupId[i]);
                    }
                }
                return Ok("Group added successfully");
            }
            catch (System.Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost("uploadfile")]
        public async Task<ActionResult> Post([FromForm]IFormFile file, [FromForm]int groupId)
        {
            try
            {
                var fileName = Path.GetFileName(file.FileName);
                var parentPath  = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
                var directoryPath = Path.Combine(parentPath,"AMSClient", "wwwroot", "uploads", "groups", groupId.ToString());

                // Create directory if it doesn't exist
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var filePath = Path.Combine(directoryPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                //get group by groupId
                var group = _groupRepository.GetGroupById(groupId);
                var resoure = new Resource
                {
                    ResourceName = fileName,
                    FileUrl = $"/uploads/groups/{groupId}/{fileName}",
                    Type = file.ContentType
                };
                group.Resources.Add(resoure);
                await _groupRepository.UpdateGroup(group);
                return Ok($"{resoure.FileUrl}");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        //POST api/Groups/{groupId}/messages
        [HttpPost("{groupId}/messages")]
        public async Task<IActionResult> Post(GroupChatViewModel groupChat)
        {
            try
            {
                //get userId from groupChat.Useremail
                int senderId = _userRepository.GetUserByEmail(groupChat.UserEmail).Id;
                //get all members from groupChat
                var members = _groupStudentRepository.GetGroupStudentsByGroupId(groupChat.GroupId);
                //remove member which has same id with senderId
                members.RemoveAll(x => x.UserId == senderId);
                foreach (var member in members)
                {
                    //create chat
                    var chat = new Chat
                    {
                        SenderId = senderId,
                        ReceiverId = member.UserId,
                        Message = groupChat.Message,
                        GroupId = groupChat.GroupId,
                    };
                    await _chatRepository.CreateChat(chat);
                }
                return Ok("Message sent successfully");
            }
            catch (System.Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
