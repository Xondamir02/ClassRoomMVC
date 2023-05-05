using Classroom.Data.Context;
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
        public async Task<IActionResult> Index(Guid schoolId)
        {

                var school = await _context.Schools
                .Include(s => s.Sciences)
                .ThenInclude(s => s.UserSciences)
                .FirstOrDefaultAsync(s => s.Id == schoolId);

            return View(school);
        }



    }
}
