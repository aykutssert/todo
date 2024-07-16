using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnetCourse.Models
{
    public class Todo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "fill desc")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "fill date")]
        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "select category")]
        public string CategoryId { get; set; } = string.Empty;

        [ValidateNever]
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;

        [Required(ErrorMessage = "select status")]
        public string StatusId { get; set; } = string.Empty;

        [ValidateNever]
        [ForeignKey("StatusId")]
        public Status Status { get; set; } = null!;

        public bool OverDue => StatusId == "open" && DueDate < DateTime.Today;


    }
}
