using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetCourse.Models.ViewModels
{
    public class TodoIndexVM
    {
        public IEnumerable<Todo> Todos { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public IEnumerable<SelectListItem> Statues { get; set; }

        public IEnumerable<SelectListItem> DueFilterValues { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
    }
}
