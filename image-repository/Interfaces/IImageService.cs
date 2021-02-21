using image_repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace image_repository.Interfaces {
  public interface IImageService {
    public IList<ImageModel> Get(string searchString);

    public Task<ImageModel> GetById(int id);

    public Task<int> Create(ImageModel imageModel);

    public Task Delete(int id);
  }
}
