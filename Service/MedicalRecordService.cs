using ClinicaAPI.Model;
using ClinicaAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicaAPI.Services{
    public class MedicalRecordService{
        private readonly ClinicaDBContext contextDB;

        public MedicalRecordService(ClinicaDBContext context){
            contextDB = context;
        }

        public async Task<MedicalRecord> CriarProntuarios(int PacienteId, string desc){
            var prontuario = new MedicalRecord{
                PacienteId = PacienteId,
                Descricao = desc
            };
            contextDB.Prontuarios.Add(prontuario);
            await contextDB.SaveChangesAsync();
            return prontuario;
        }       

        public async Task<List<MedicalRecord>> ListarProntuarios(){
            return await contextDB.Prontuarios.ToListAsync();
        }

        public async Task<List<MedicalRecord>> BuscarProntuario(int pacienteId){
            return await contextDB.Prontuarios
                .Where(p => p.PacienteId == pacienteId)
                .ToListAsync();
        }
    

        public async Task<MedicalRecord> AtualizarProntuario(int id, string desc){
            var prontuario = await contextDB.Prontuarios.FindAsync(id);
            if (prontuario == null){
                throw new Exception("Prontuário não encontrado");
            }
            prontuario.Descricao = desc;
            contextDB.Prontuarios.Update(prontuario);
            await contextDB.SaveChangesAsync();
            return prontuario;
        }
        public async Task DeletarProntuario(int id){
            var prontuario = await contextDB.Prontuarios.FindAsync(id);
            if (prontuario == null)
            {
                throw new Exception("Prontuário não encontrado");
            }

            contextDB.Prontuarios.Remove(prontuario);
            await contextDB.SaveChangesAsync();
        }
    }
}