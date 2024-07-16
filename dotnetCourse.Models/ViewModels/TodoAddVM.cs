using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dotnetCourse.Models.ViewModels
{
    public class TodoAddVM
    {
        public Todo Todo { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> Categories { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> Statuses { get; set; }
    }
}
