
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaAPI.Model{

    public class MedicalRecord{
        public int Id {get; set;}
        public int PacienteId{get; set;}
        
        [ForeignKey("PacienteId")]
        
        public string Descricao{get; set;}
    }
}