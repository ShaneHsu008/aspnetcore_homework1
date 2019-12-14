using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using aspnetcore_homework.Models;
using aspnetcore_homework.ViewModels;

namespace aspnetcore_homework.Controllers
{
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ContosouniversityContext _context;

        public CoursesController(ContosouniversityContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourse()
        {
            return await _context.Course.ToListAsync();
        }

        // GET: api/Courses/Students
        [HttpGet("StudentsAll")]
        public async Task<ActionResult<IEnumerable<VwCourseStudents>>> GetCourseStudents()
        {
            return await _context.VwCourseStudents.ToListAsync();
        }

        // GET: api/Courses/Students
        [HttpGet("Student/{courseId}")]
        public async Task<ActionResult<VwCourseStudents>> GetCourseStudent(int courseId)
        {
            var courseStudent = await _context.VwCourseStudents.FirstOrDefaultAsync(v => v.CourseId == courseId);

            if (courseStudent == null)
            {
                return NotFound();
            }

            return courseStudent;
        }

        // GET: api/Courses/Students
        [HttpGet("StudentCountAll")]
        public async Task<ActionResult<IEnumerable<VwCourseStudentCount>>> GetCourseStudentCount()
        {
            return await _context.VwCourseStudentCount.ToListAsync();
        }

        // GET: api/Courses/Students
        [HttpGet("StudentCount/{courseId}")]
        public async Task<ActionResult<VwCourseStudentCount>> GetCourseStudentCount(int courseId)
        {
            var courseStudentCount = await _context.VwCourseStudentCount.FirstOrDefaultAsync(v => v.CourseId == courseId);

            if (courseStudentCount == null)
            {
                return NotFound();
            }

            return courseStudentCount;
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id)
        {
            var course = _context.Course.Find(id);

            if (!await TryUpdateModelAsync(course))
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCourse(int id, PatchCourseVM patchCourse)
        {
            var course = _context.Course.Find(id);

            course.Title = patchCourse.Title;
            course.DepartmentId = patchCourse.DepartmentId;

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _context.Course.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
        }

        //// DELETE: api/Courses/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Course>> DeleteCourse(int id)
        //{
        //    var course = await _context.Course.FindAsync(id);
        //    if (course == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Course.Remove(course);
        //    await _context.SaveChangesAsync();

        //    return course;
        //}

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public ActionResult<Course> DeleteCourse(int id)
        {
            var course = _context.Course.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Course.Remove(course);
            _context.SaveChanges();

            return course;
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.CourseId == id);
        }
    }
}
