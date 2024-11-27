using ClinicaAPI.Model;

using Microsoft.AspNetCore.Mvc;
using ClinicaAPI.Services;

namespace ClinicaAPI.Controller{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase{
        private readonly UserService userService;
        
        public PatientController(UserService userServices){
            userService = userServices;
           
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> ListarPacientes(){
            try {
                var pacientes = await userService.ListarPacientes();
                return Ok(pacientes);
            }
            catch (Exception ex){
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Patient>> CadastrarPaciente(Patient paciente){
            try{
                var novoPaciente = await userService.CadastrarPaciente(paciente);
                return Ok(novoPaciente);
            }
            catch (Exception ex) {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}