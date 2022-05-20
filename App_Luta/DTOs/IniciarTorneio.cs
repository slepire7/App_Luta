using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App_Luta.DTOs
{
    public class IniciarTorneio
    {
        [Required]
        [MaxLength(16, ErrorMessage = "Numero Maximo de competidores são 16")]
        [MinLength(16, ErrorMessage = "Numero Minimo de competidores são 16")]
        public int[] IdsLutadores { get; set; }
    }
}
