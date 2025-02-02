using System;
using System.ComponentModel.DataAnnotations;

namespace Calendario.Models
{
    public class Evento
    {
        [Key]
        public int EventoID { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; } = null;

        [Required]
        public DateTime Fecha { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; } = null;

        public string Ubicacion { get; set; } = null;

        // public bool TodoElDia { get; set; }
    }
}