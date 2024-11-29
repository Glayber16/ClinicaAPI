using ClinicaAPI.Model;
using ClinicaAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaAPI.Services{
    public class UserService{
        private readonly ClinicaDBContext contextDB;

        public UserService(ClinicaDBContext context)  {
            contextDB = context;
        }
        public async Task<User> Cadastrar(User usuario){
            
            if(await EmailExist(usuario.Email) == true){
                throw new Exception("E-mail já está em uso.");
            }
            usuario.SenhaHash = SenhaHash.Utils.SenhaHash.HashPassword(usuario.SenhaHash);
            contextDB.Usuarios.Add(usuario);
            
            await contextDB.SaveChangesAsync();
            return usuario;
        }

        public async Task<User> Login(User usuario){
            
            var user = await contextDB.Usuarios.FirstOrDefaultAsync(u => u.Email == usuario.Email);
            if (user == null){
                throw new Exception("Usuário não encontrado.");
            } 
            if (!SenhaHash.Utils.SenhaHash.VerificarSenha(usuario.SenhaHash, user.SenhaHash)){
                throw new Exception("Senha inválida.");
            }
 
            return user;
        }


        public async Task<List<User>> ListarUsuarios(){
            return await contextDB.Usuarios.ToListAsync();
        }   

        public async Task RemoveUsuario(int id){
            var usuario = await contextDB.Usuarios.FindAsync(id);
            if(usuario != null){
                contextDB.Usuarios.Remove(usuario);
                await contextDB.SaveChangesAsync();
            }
            
        }

        public async Task<bool> EmailExist(string email){
            return await contextDB.Usuarios.AnyAsync(u => u.Email == email);
        }

        public async Task<User> AtualizarUsuario(int id, User usuarioAtt){
            var usuario = await contextDB.Usuarios.FindAsync(id);
            if(usuario == null){
                return null;
            }
            
            usuario.Nome = usuarioAtt.Nome;
            usuario.CPF = usuarioAtt.CPF;
            usuario.Email = usuarioAtt.Email;
            await contextDB.SaveChangesAsync();
            return usuario;
        }

        public async Task<Patient> AtualizarPaciente(int id, [FromBody] Patient patientAtt){
            var patient = await contextDB.Usuarios.OfType<Patient>().FirstOrDefaultAsync(u => u.Id == id);

            if (patient == null){
                return null;
            }
            
            
            patient.CPF = patientAtt.CPF;
            patient.Nome = patientAtt.Nome;
            patient.DataNascimento = patientAtt.DataNascimento;

            await contextDB.SaveChangesAsync();
            return patient;
        }

        public async Task<Doctor> AtualizarMedico(int id, Doctor doctorAtt){
            var doctor = await contextDB.Usuarios.OfType<Doctor>().FirstOrDefaultAsync(u => u.Id == id);
            if (doctor == null){
                return null;
            }
           
            doctor.CPF = doctorAtt.CPF;
            doctor.Nome = doctorAtt.Nome;
            doctor.CRM = doctorAtt.CRM;

            await contextDB.SaveChangesAsync();
            return doctor;
        }

        public async Task<Patient> CadastrarPaciente(Patient paciente){
    
            if (await EmailExist(paciente.Email)){
                throw new Exception("E-mail já está em uso.");
            }
            paciente.SenhaHash = SenhaHash.Utils.SenhaHash.HashPassword(paciente.SenhaHash);
            contextDB.Usuarios.Add(paciente);
            await contextDB.SaveChangesAsync();

            return paciente;
        }

        public async Task<IEnumerable<User>> ListarPacientes(){
            return await contextDB.Usuarios.OfType<Patient>().ToListAsync();
        }

        public async Task<Doctor> CadastrarMedico(Doctor medico){
    
            if (await EmailExist(medico.Email)){
                throw new Exception("E-mail já está em uso.");
            }
            medico.SenhaHash = SenhaHash.Utils.SenhaHash.HashPassword(medico.SenhaHash);
            contextDB.Usuarios.Add(medico);
            await contextDB.SaveChangesAsync();

            return medico;
        }

        public async Task<IEnumerable<User>> ListarMedicos(){
            return await contextDB.Usuarios.OfType<Doctor>().ToListAsync();
        }

        

        
    }
}