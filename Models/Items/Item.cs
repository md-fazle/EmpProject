using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models.Items
{
    public class Item
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "ItemId is required")]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Code is required")]
        public int Code { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }

        public string? Description { get; set; }
    }
}
