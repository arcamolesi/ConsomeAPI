using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsomeAPI.Models { 
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [StringLength(35)]
        [Required]
        public string descricao { get; set; }

        public float quantidade { get; set; }
        public float valor { get; set; }

    }
}
