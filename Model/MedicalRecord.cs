using System;

namespace ClinicaAPI.Model{

    public class MedicalRecord{
        public int Id {get; set;}
        public int PacienteId{get; set;}
        public string Descricao{get; set;}
    }
}