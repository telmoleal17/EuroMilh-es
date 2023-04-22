using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Servidor.Models
{
    public partial class ApostasArquivada
    {
        [Key]
        [Column("nif")]
        public int Nif { get; set; }
        [Required]
        [Column("numeros")]
        [StringLength(20)]
        public string Numeros { get; set; }
        [Required]
        [Column("estrelas")]
        [StringLength(10)]
        public string Estrelas { get; set; }
        [Key]
        [Column(TypeName = "datetime")]
        public DateTime DataAposta { get; set; }
    }
}
