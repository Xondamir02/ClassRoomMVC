using System.Security.Claims;
using Classroom.Data.Context;
using Classroom.Data.Entities;
using ClassRoomMVC.Models;
using ClassRoomMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassRoomMVC.Controllers;

[Authorize]
public class SchoolsController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserProvider _userProvider;

    public SchoolsController(AppDbContext context, UserProvider userProvider)
    {
        _context=context;
        _userProvider = userProvider;
    }
    public async Task<IActionResult> Index()
    {
        var schools = await _context.Schools
            .Include(school => school.UserSchools)
            .ToListAsync();

        return View(schools);
    }


    [HttpGet]
    public IActionResult CreateSchool()
    {
        return View();
    }

    [HttpPost]

    public async Task<IActionResult> CreateSchool([FromForm] CreateSchoolDto createSchoolDto)
    {
        if (!ModelState.IsValid)
        {
            return View(createSchoolDto);
        }

        var school = new School()
        {
            Name = createSchoolDto.Name,
            Description = createSchoolDto.Description
        };

        if (createSchoolDto.Photo != null)
        {
            school.PhotoUrl = await FileHelper.SaveSchoolFile(createSchoolDto.Photo);
        }

        school.UserSchools = new List<UserSchool>
        {
            new ()
            {
                UserId = _userProvider.UserId,
                Type = EUserSchool.Creater
            }
        };

        _context.Schools.Add(school);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> GetSchoolById(Guid id)
    {
        var school = await _context.Schools
            .Include(school => school.UserSchools)
            .ThenInclude(userSchool => userSchool.User)
            .FirstOrDefaultAsync(x => x.Id == id);

        return View(school);
    }


    public async Task<IActionResult> JoinSchool(Guid id)
    {
        var school = await _context.Schools
            .Include(s => s.UserSchools)
            .ThenInclude(u => u.User)
            .FirstOrDefaultAsync(s => s.Id == id);

        var userId = _userProvider.UserId;
        var isUserExistsInSchool = school.UserSchools.Any(u => u.User.Id == userId);

        if (!isUserExistsInSchool)
        {
            school.UserSchools.Add(new UserSchool()
            {
                UserId = userId,
                Type = EUserSchool.Student
            });
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("GetSchoolById", new { id = school.Id });
    }


    [HttpGet]
    public async Task<IActionResult> UpdateSchool(Guid id)
    {
        var school = await _context.Schools.FirstOrDefaultAsync(s => s.Id == id);
        ViewBag.Id = id;

        return View(new UpdateSchoolDto()
        {
            Name = school.Name,
            Description = school.Description,
        });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateSchool(Guid id, UpdateSchoolDto updateSchoolDto)
    {
        var school = await _context.Schools.FirstOrDefaultAsync(s => s.Id == id);
        school.Name=updateSchoolDto.Name;
        school.Description=updateSchoolDto.Description;
        if (updateSchoolDto.Photo != null)
        {
            school.PhotoUrl = await FileHelper.SaveSchoolFile(updateSchoolDto.Photo);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction("GetSchoolById", new { id = school.Id });
    }

    public async Task<IActionResult> UpdateUserSchoolRole(Guid schoolId, Guid userId, EUserSchool role)
    {
        var userSchool = await _context.UserSchools
            .FirstOrDefaultAsync(u => u.UserId == userId && u.SchoolId == schoolId);

        if (userSchool.Type != EUserSchool.Creater && role != EUserSchool.Creater)
            userSchool.Type = role;

        await _context.SaveChangesAsync();

        return RedirectToAction("GetSchoolById", new { id = schoolId });
    }


}



