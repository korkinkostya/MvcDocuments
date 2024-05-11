using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcDocuments.Domain.Entities
{
    public class DocumentRow
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        
        
        [Display(Name = "Цена")]
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayFormat(DataFormatString = "{0:G29}", ApplyFormatInEditMode = true)] 

        public decimal Price { get; set; }

        public int DocumentId { get; set; }
    }
}