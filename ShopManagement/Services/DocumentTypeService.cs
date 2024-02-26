using ShopManagement.DAL;
using ShopManagement.Models;
using System.Xml.Linq;

namespace ShopManagement.Services
{
    public class DocumentTypeService
    {
        private readonly DocumentTypeRepository _documentTypeRepository;
        public DocumentTypeService(DocumentTypeRepository documentTypeRepository)
        {
            _documentTypeRepository = documentTypeRepository;
        }

        public DocumentType? GetById(int id)
        {
            DocumentType? documentType = _documentTypeRepository.GetById(id);
            return documentType;
        }

        public List<DocumentType> GetAll()
        {
            List<DocumentType> documentTypes = _documentTypeRepository.GetAll().ToList();
            return documentTypes;
        }

        public DocumentType Add(DocumentType documentType)
        {
            DocumentType? newDocumentType = _documentTypeRepository.AddAndSaveChanges(documentType);

            return newDocumentType;
        }

        public void Update(DocumentType documentType)
        {
            _documentTypeRepository.UpdateAndSaveChanges(documentType);
        }

        public void Delete(int id)
        {
            DocumentType? documentType = _documentTypeRepository.GetById(id);
            if(documentType != null)
                _documentTypeRepository.RemoveByIdAndSaveChanges(id);
        }
    }
}
