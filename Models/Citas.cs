using System.ComponentModel.DataAnnotations;

namespace WSClinica.Models
{
    public class Citas
    {
        [Key]
        public int Id { get; set; }
        public DateTime Fecha_inicio { get; set; }
        public int Duracion_minutos { get; set; }
	    public int Id_sala { get; set; }
        public int Id_paciente { get; set; }
        public int Id_medico { get; set; }
        public string Estado { get; set; }
    }
}
