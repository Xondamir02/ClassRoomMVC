using Classroom.Data.Context;
using Classroom.Data.Entities;
using ClassRoomMVC.Models;
using ClassRoomMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassRoomMVC.Controllers
{
    public class ScienceController : Controller
    {

        private readonly AppDbContext _context;
        private readonly UserProvider _userProvider;

        public ScienceController(AppDbContext context, UserProvider userProvider)
        {
            _context = context;
            _userProvider = userProvider;
        }
        public async Task<IActionResult> Index(Guid schoolId, string? name = null, bool orderByUsers = false)
        {

                var school = await _context.Schools
                .Include(s => s.Sciences)
                .ThenInclude(s => s.UserSciences)
                .FirstOrDefaultAsync(s => s.Id == schoolId);

                ViewBag.SchoolId = schoolId;
                ViewBag.OrderByUsers = orderByUsers;
                var query = _context.Sciences
                    .Include(s => s.UserSciences);


                // var query = select * from sciences;

            if (name != null)
                {
                    // query += where name.contains(str);
                    query = query.Where(s => s.Name.Contains(name))
                        .Include(s => s.UserSciences);
                }

                if (orderByUsers)
                {

                    // query += order by users.count;
                    var sciences = await query
                        .Where(s => s.SchoolId == schoolId)
                        .OrderByDescending(s => s.UserSciences.Count).ToListAsync();

                    return View(sciences);
                }


                return View(await query.Where(s => s.SchoolId == schoolId).ToListAsync());
        }

        public async Task<IActionResult> GetById(Guid scienceId)
        {
            var science = await _context.Sciences
                .Include(s => s.School)
                .Include(s => s.UserSciences)
                .ThenInclude(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == scienceId);

            return View(science);
        }

        //[HttpGet]
        //public IActionResult Create(Guid schoolId)
        //{
        //    ViewBag.SchoolId = schoolId;
        //    return View();
        //}

        [HttpGet]
        public IActionResult Create(Guid schoolId)
        {
            ViewBag.SchoolId = schoolId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid schoolId, [FromForm] CreateScienceDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createDto);
            }

            var school = new Science()
            {
                Name = createDto.Name,
                Description = createDto.Description,
                SchoolId = schoolId
            };

            school.UserSciences = new List<UserScience>
            {
                new UserScience()
                {
                    UserId = _userProvider.UserId,
                    Type = EUserScience.Teacher
                }
            };

            _context.Sciences.Add(school);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { schoolId = schoolId });
        }
        public IActionResult SendJoinScienceRequest(Guid scienceId)
        {
            ViewBag.ScienceId = scienceId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendJoinScienceRequest(Guid scienceId, [FromForm] CreateJoinScienceRequestDto requestDto)
        {
            var toUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == requestDto.Username);

            var isExistsPreviewJoinRequest =
                await _context.JoinScienceRequests
                    .AnyAsync(r => r.ToUserId == toUser.Id && r.ScienceId == scienceId);
            
            if (isExistsPreviewJoinRequest)
            {
                return RedirectToAction("GetById", new { scienceId });
            }

            var isExistsInScience =
                await _context.UserSciences
                    .AnyAsync(u => u.UserId == toUser.Id && u.ScienceId == scienceId);
          

            if (isExistsInScience)
            {
                return RedirectToAction("GetById", new { scienceId });
            }

            var userId = _userProvider.UserId;

            var joinRequest = new JoinScienceRequest()
            {
                FromUserId = userId,
                ScienceId = scienceId,
                ToUserId = toUser.Id,
                IsJoinded = false
            };

            _context.JoinScienceRequests.Add(joinRequest);
            await _context.SaveChangesAsync();

            return RedirectToAction("GetById", new { scienceId });
        }

        public async Task<IActionResult> JoinScience(Guid joinRequestId, bool isJoin)
        {
            var joinRequst = await _context.JoinScienceRequests
                .FirstOrDefaultAsync(r => r.Id == joinRequestId && r.ToUserId == _userProvider.UserId);

            if (isJoin)
            {
                var userScience = new UserScience()
                {
                    ScienceId = joinRequst.ScienceId,
                    UserId = joinRequst.ToUserId,
                    Type = EUserScience.Student
                };

                joinRequst.IsJoinded = true;
                _context.UserSciences.Add(userScience);
            }
            else
            {
                _context.JoinScienceRequests.Remove(joinRequst);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Profile", "Users");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid scienceId)
        {
            var science = await _context.Sciences.FirstOrDefaultAsync(sc => sc.Id == scienceId);
            ViewBag.Id=scienceId;
            return View(new UpdateScienceDto()
            {
                Name = science.Name,
                Description = science.Description,
            });
        }

        //public async Task<IActionResult> Update(Guid scienceId, UpdateScienceDto updateScienceDto)
        //{
        //    var science = await _context.Sciences.FirstOrDefaultAsync(sc => sc.Id == scienceId);

        //    science.Name=updateScienceDto.Name;
        //    science.Description=updateScienceDto.Description;

        //    await _context.SaveChangesAsync();

        //    return RedirectToAction("GetById", new { scienceId });
        //}


    }



}

