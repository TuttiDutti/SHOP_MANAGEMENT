using ShopManagement.DAL;
using ShopManagement.Models;

namespace ShopManagement.Services
{
    public class AbsenceTypeService
    {
        private readonly AbsenceTypeRepository _absenceTypeRepository;
        public AbsenceTypeService(AbsenceTypeRepository absenceTypeRepository)
        {
            _absenceTypeRepository = absenceTypeRepository;
        }

        public AbsenceType? GetById(int id)
        {
            AbsenceType? absenceType = _absenceTypeRepository.GetById(id);
            return absenceType;
        }

        public List<AbsenceType> GetAll()
        {
            List<AbsenceType> absenceTypes = _absenceTypeRepository.GetAll().ToList();
            return absenceTypes;
        }

        public AbsenceType Add(AbsenceType absenceType)
        {
            AbsenceType? newAbsenceType = _absenceTypeRepository.AddAndSaveChanges(absenceType);

            return newAbsenceType;
        }

        public void Update(AbsenceType absenceType)
        {
            _absenceTypeRepository.UpdateAndSaveChanges(absenceType);
        }

        public void Delete(int id)
        {
            AbsenceType? absenceType = _absenceTypeRepository.GetById(id);

            _absenceTypeRepository.RemoveByIdAndSaveChanges(id);
        }
    }
}
