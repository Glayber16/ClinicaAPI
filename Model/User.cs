using System;

namespace ClinicaAPI.Model{

    public class User{
        public int Id {get; set;}
        public string Nome{get; set;}
        public string CPF{get; set;}
        public string Email{get; set;}
        public string SenhaHash { get; set; }
        public string TipoUsuario { get; set; }

        
    }

}
  