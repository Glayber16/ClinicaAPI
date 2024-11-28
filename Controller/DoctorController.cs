using ClinicaAPI.Model;

using Microsoft.AspNetCore.Mvc;
using ClinicaAPI.Services;

namespace ClinicaAPI.Controller{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase{
       
        private readonly UserService userService;
        
        public DoctorController(UserService userServices){
            userService = userServices;
          
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> ListarMedicos(){
            try {
                var medicos = await userService.ListarMedicos();
                return Ok(medicos);
            }
            catch (Exception ex){
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Patient>> CadastrarMedico(Doctor medico){
            try{
                var novoMedico = await userService.CadastrarMedico(medico);
                
                return Ok(novoMedico);
            }
            catch (Exception ex) {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}