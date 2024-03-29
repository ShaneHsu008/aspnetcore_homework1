﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using aspnetcore_homework.Models;

namespace aspnetcore_homework.Controllers
{
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ContosouniversityContext _context;

        public DepartmentsController(ContosouniversityContext context)
        {
            _context = context;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartment()
        {
            return await _context.Department.ToListAsync();
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _context.Department.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        // GET: api/Departments/DepartmentCourseCount
        [HttpGet("DepartmentCourseCountAll")]
        public async Task<ActionResult<IEnumerable<VwDepartmentCourseCount>>> GetDepartmentCourseCountAll()
        {
            return await _context.VwDepartmentCourseCount.FromSqlRaw("SELECT * FROM vwDepartmentCourseCount").ToListAsync(); ;
        }

        // GET: api/Departments/DepartmentCourseCount/5
        [HttpGet("DepartmentCourseCount/{departmentId}")]
        public async Task<ActionResult<VwDepartmentCourseCount>> GetDepartmentCourseCount(int departmentId)
        {
            var departmentCourseCount = await _context.VwDepartmentCourseCount.FromSqlRaw($"SELECT * FROM vwDepartmentCourseCount WHERE DepartmentID = {departmentId}").FirstOrDefaultAsync();

            if (departmentCourseCount == null)
            {
                return NotFound();
            }

            return departmentCourseCount;
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest();
            }

            await _context.Database.ExecuteSqlInterpolatedAsync($"[dbo].[Department_Update] {id},{department.Name},{department.Budget},{department.StartDate},{department.InstructorId},{department.RowVersion}");

            return NoContent();
        }

        // POST: api/Departments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"[dbo].[Department_Insert] {department.Name},{department.Budget},{department.StartDate},{department.InstructorId}");

            return CreatedAtAction("GetDepartment", new { id = department.DepartmentId }, department);
        }

        // DELETE: api/Departments/DeleteBySP/5
        [HttpDelete("DeleteBySP/{id}")]
        public async Task<ActionResult<Department>> DeleteDepartmentDeleteBySP(int id)
        {
            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            await _context.Database.ExecuteSqlInterpolatedAsync($"[dbo].[Department_Delete] {id},{department.RowVersion}");

            return department;
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> DeleteDepartment(int id)
        {
            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Department.Remove(department);
            await _context.SaveChangesAsync();

            return department;
        }

        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.DepartmentId == id);
        }
    }
}
