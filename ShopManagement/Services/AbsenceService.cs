using ShopManagement.DAL;
using ShopManagement.Models;

namespace ShopManagement.Services
{
    public class AbsenceService
    {
        private readonly AbsenceRepository _absenceRepository;
        public AbsenceService(AbsenceRepository absenceRepository)
        {
            _absenceRepository = absenceRepository;
        }

        public Absence? GetById(int id)
        {
            Absence? absence = _absenceRepository.GetById(id);
            return absence;
        }

        public List<Absence> GetAll()
        {
            List<Absence> absence = _absenceRepository.GetAll().ToList();
            return absence;
        }

        public Absence Add(Absence absence)
        {
            Absence? newAbsence = _absenceRepository.AddAndSaveChanges(absence);

            return newAbsence;
        }

        public void Update(Absence absence)
        {
            _absenceRepository.UpdateAndSaveChanges(absence);
        }

        public void Delete(int id)
        {
            _absenceRepository.RemoveByIdAndSaveChanges(id);
        }
    }
}
