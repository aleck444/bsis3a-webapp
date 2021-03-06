using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace aleck3a_webapp.Models
{
    public class Type
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Type Name")]
        public string Name { get; set; }

        public Item Item { get; set; }
        public int ItemId { get; set; }
        
    }
}