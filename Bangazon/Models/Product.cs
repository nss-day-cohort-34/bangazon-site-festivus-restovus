using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Required]
        [StringLength(255)]
        [RegularExpression(@"^[a-zA-Z  0-9]*$", ErrorMessage = "No special characters.")]
        public string Description { get; set; }

        [Required]
        [StringLength(55, ErrorMessage = "Please shorten the product title to 55 characters")]
        [RegularExpression(@"^[a-zA-Z  0-9]*$", ErrorMessage = "No special characters.")]
        public string Title { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Range(0, 9999.99, ErrorMessage = "Price must be below $10,000")]
        public double Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string UserId { get; set; }

        public string City { get; set; }
        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        public bool Active { get; set; }

        [Required]
        [Display(Name = "Seller")]
        public ApplicationUser User { get; set; }

        [Required]
        [Display(Name = "Product Category")]
        public int ProductTypeId { get; set; }
        [Display(Name = "Category")]
        public ProductType ProductType { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

        public Product()
        {
            Active = true;
        }

    }
}
