using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcore_homework.ViewModels
{
    public class PatchCourseVM
    {
        [MaxLength(50)]
        public string Title { get; set; }
        [Range(1, 5)]
        public int DepartmentId { get; set; }
    }
}
