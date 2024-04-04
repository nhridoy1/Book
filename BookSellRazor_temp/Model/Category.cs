using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookSellRazor_temp.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Category Name must be maximum 20 character"), MinLength(3, ErrorMessage = "Category Name must be minimum 3 character")]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display order must be between 1 to 100")]
        public string DisplayOrder { get; set; }
    }
}
