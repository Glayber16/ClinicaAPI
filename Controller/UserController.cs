using ClinicaAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClinicaAPI.Services;
namespace ClinicaAPI.Controller{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase{
        private readonly UserService userService;
        public UserController(UserService userServices){
            userService = userServices;
        }
        [HttpGet]
        public async Task<ActionResult<List<User>>> ListarUsuarios(){
            var usuarios = await userService.ListarUsuarios();
            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CadastrarUsuario(User usuario){
            User novoUsuario = await userService.Cadastrar(usuario);
            return Ok(novoUsuario);
        }
        [HttpDelete ("{id}")]
        public async Task<ActionResult<User>> RemoverUsuario(int id){
            await userService.RemoveUsuario(id);
            return Ok("Usuario removido com sucesso");
        }
        [HttpPut ("{id}")]
        public async Task<ActionResult<User>> AtualizarUsuario(int id, [FromBody] User usuarioAtt){
            var usuario = await userService.AtualizarUsuario(id, usuarioAtt);
            if(usuario == null) {
                return NotFound("Usuario não encontrado");
            }
            return Ok(usuario);
        }
    }
}