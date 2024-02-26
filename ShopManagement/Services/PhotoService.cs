using ShopManagement.DAL;
using ShopManagement.Models;

namespace ShopManagement.Services
{
    public class PhotoService
    {
        private readonly PhotoRepository _photoRepository;
        public PhotoService(PhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public Photo? GetById(int id)
        {
            Photo? photo = _photoRepository.GetById(id);
            return photo;
        }

        public List<Photo> GetAll()
        {
            List<Photo> photos = _photoRepository.GetAll().ToList();
            return photos;
        }

        public Photo Add(Photo photo)
        {
            Photo? newPhoto = _photoRepository.AddAndSaveChanges(photo);

            return newPhoto;
        }

        public void Update(Photo photo)
        {
            _photoRepository.UpdateAndSaveChanges(photo);
        }

        public void Delete(int id)
        {
            Photo? photo = _photoRepository.GetById(id);

            _photoRepository.RemoveByIdAndSaveChanges(id);
        }

        public List<Photo> GetByProduct(int prodId)
        {
            List<Photo> photos = _photoRepository.GetAll()
                .Where(p => p.ProductId == prodId).ToList();
            return photos;
        }
    }
}
