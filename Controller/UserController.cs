using ClinicaAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClinicaAPI.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace ClinicaAPI.Controller{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase{
        private readonly IConfiguration _configuration;
        private readonly UserService userService;
        
        public UserController(UserService userServices, IConfiguration configuration){
            userService = userServices;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<ActionResult<List<User>>> ListarUsuarios(){
            var usuarios = await userService.ListarUsuarios();
            return Ok(usuarios);
        }

        [HttpPost("register")]
        
        public async Task<ActionResult<User>> CadastrarUsuario(User usuario){
            
            try{
                User novoUsuario = await userService.Cadastrar(usuario);
                return Ok(novoUsuario);
            }
            catch(Exception ex){
                return BadRequest(new {error = ex.Message});
            }
        }

        [HttpPost("login")]

        public async Task<ActionResult<User>> Login(User usuario){
    
            try{
   
                User user = await userService.Login(usuario);
                var tokenGenerator = new JwtTokenGenerator(_configuration);
                var token = tokenGenerator.GenerateToken(user.Id.ToString(), user.Email);
   
                return Ok(new { token, user });
            }
            catch(Exception ex){
                return BadRequest(new {error = ex.Message});
            }
            
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