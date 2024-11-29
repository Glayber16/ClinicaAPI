using ClinicaAPI.Model;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;

using ClinicaAPI.Services;

namespace ClinicaAPI.Controller{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordController : ControllerBase{
       
        private readonly MedicalRecordService medicalRecordService;
        
        public MedicalRecordController(MedicalRecordService medicalRecordServices){
            medicalRecordService = medicalRecordServices;
          
        }

        [HttpPost]

        public async Task<ActionResult> CriarProntuario(MedicalRecord prontuario){
            try{
                Console.WriteLine($"PacienteId: {prontuario.PacienteId}, Descricao: {prontuario.Descricao}");
                var novoProntuario = await medicalRecordService.CriarProntuarios(prontuario.PacienteId, prontuario.Descricao);
                return Ok(novoProntuario);
            }
            catch(Exception ex){
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<MedicalRecord>>> ListarProntuarios(){
            var prontuarios = await medicalRecordService.ListarProntuarios();
            return Ok(prontuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecord>> ProcurarProntuario(int id){
            var prontuario = await medicalRecordService.BuscarProntuario(id);
            return Ok(prontuario);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<MedicalRecord>> ExcluirProntuario(int id){
            try{
                await medicalRecordService.DeletarProntuario(id);
                return Ok("Prontuario removido com sucesso");
            }
             catch (Exception ex){
                return BadRequest(new { message = ex.Message });
            }
            
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<MedicalRecord>> AtualizarProntuarios(int id, string desc){
            try{
                var prontuarioAtt = await medicalRecordService.AtualizarProntuario(id, desc);
                return Ok(prontuarioAtt);
            }
            catch (Exception ex){
                return BadRequest(new{message = ex.Message});
            }
        }
    }
}