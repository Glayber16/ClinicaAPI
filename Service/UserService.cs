using ClinicaAPI.Model;
using ClinicaAPI.Data;
using Microsoft.EntityFrameworkCore;
namespace ClinicaAPI.Services{
    public class UserService{
        private readonly ClinicaDBContext contextDB;

        public UserService(ClinicaDBContext context)
        {
            contextDB = context;
        }
        public async Task<User> Cadastrar(User usuario){
            contextDB.Usuarios.Add(usuario);
            await contextDB.SaveChangesAsync();
            return usuario;
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