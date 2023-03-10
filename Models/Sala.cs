using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WSClinica.Models
{
    public class Sala
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Id_centro_atencion { get; set; }
        public DateTime Disponible_Desde { get; set; }
        public DateTime Disponible_Hasta { get; set; }
    }
}
