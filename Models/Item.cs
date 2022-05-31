using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace aleck3a_webapp.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}