namespace WSClinica.Models
{
    public class CitasPaciente
    {
        
        public string Identificacion { get; set; }
        public string Paciente { get; set; }
        public DateTime Fecha_inicio { get; set; }
        public int Duracion_minutos { get; set; }
        public string Estado { get; set; }
        public string Sala { get; set; }
        public string Medico { get; set; }
        public string CentroAtencion { get; set; }
    }
}
