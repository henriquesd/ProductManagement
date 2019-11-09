using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.App.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(200, ErrorMessage = "Field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(800, ErrorMessage = "Field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime RegisterDate { get; set; }

        [DisplayName("Active?")]
        public bool Active { get; set; }
    }
}
