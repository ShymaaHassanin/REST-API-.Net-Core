using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Model
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [MaxLength(100)]
        public string ImgURL { get; set; }

        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public Category Category { get; set; }

    }
}
