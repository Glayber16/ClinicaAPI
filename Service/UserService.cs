using ClinicaAPI.Model;
using ClinicaAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SenhaHash.Utils;
namespace ClinicaAPI.Services{
    public class UserService{
        private readonly ClinicaDBContext contextDB;

        public UserService(ClinicaDBContext context)
        {
            contextDB = context;
        }
        public async Task<User> Cadastrar(User usuario){
            Console.WriteLine("Registro Endpoint");
            if(await EmailExistsAsync(usuario.Email) == true){
                throw new Exception("E-mail já está em uso.");
            }
            usuario.SenhaHash = SenhaHash.Utils.SenhaHash.HashPassword(usuario.SenhaHash);
            contextDB.Usuarios.Add(usuario);
            
            await contextDB.SaveChangesAsync();
            return usuario;
        }

        public async Task<User> Login(User usuario){
            Console.WriteLine("Login Endpoint");
            var user = await contextDB.Usuarios.FirstOrDefaultAsync(u => u.Email == usuario.Email);

            if (user == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            // Verifica se a senha fornecida corresponde ao hash armazenado
            if (!SenhaHash.Utils.SenhaHash.VerificarSenha(usuario.SenhaHash, user.SenhaHash))
            {
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

        public async Task<bool> EmailExistsAsync(string email){
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
    }
}