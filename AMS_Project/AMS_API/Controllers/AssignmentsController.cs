using AMS_API.ViewModel;
using BusinessObject.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        IAssignmentRepository _assignmentRepository;
        IClassRepository _classRepository;

        public AssignmentsController(IAssignmentRepository assignmentRepository, IClassRepository classRepository)
        {
            _assignmentRepository = assignmentRepository;
            _classRepository = classRepository;
        }

        // POST api/<AssignmentsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AssignmentViewModel assignmentViewModel)
        {
            //get class object by classId
            var classObj = _classRepository.GetClassById(assignmentViewModel.ClassId);
            //get teacherID from class
            int teacherId = classObj.Result.TeacherId.Value;
            List<Resource> resources = new List<Resource>();
            foreach (var file in assignmentViewModel.Files)
            {
                if (file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var parentPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
                    var directoryPath = Path.Combine(parentPath, "AMSClient", "wwwroot\\uploads");

                    // Create directory if it doesn't exist
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var filePath = Path.Combine(directoryPath, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    resources.Add(new Resource
                    {
                        ResourceName = fileName,
                        FileUrl = $"/uploads/{fileName}",
                        Type = file.ContentType,
                    });
                }
            }
            Assignment assignment = new Assignment
            {
                AssignmentName = assignmentViewModel.Title,
                AssignmentDescription = assignmentViewModel.Description,
                AssignmentDeadline = assignmentViewModel.Deadline,
                TeacherId = teacherId,
                ClassId = assignmentViewModel.ClassId,
                Resources = resources
            };
            await _assignmentRepository.AddAssignment(assignment);
            return Ok(assignment);
        }

    }
}
