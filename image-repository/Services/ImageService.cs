using image_repository.DBContext;
using image_repository.Entities;
using image_repository.Interfaces;
using image_repository.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace image_repository.Services {
  public class ImageService : IImageService {

    private readonly ImageDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ImageService(ImageDbContext context, IWebHostEnvironment webHostEnvironment) {
      _context = context;
      _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IList<ImageModel>> Get(string searchString) {
      List<ImageModel> imageModels = new List<ImageModel>();

      var images = from i in _context.Images select i;

      if (!String.IsNullOrEmpty(searchString)) {
        images = images.Where(i => i.Title.ToLower().Contains(searchString.ToLower()));
      }

      foreach(var img in images) {
        imageModels.Add(ConvertImageToImageModel(img));
      }

      return imageModels;
    }

    public async Task<ImageModel> GetById(int id) {
      
      var image = await _context.Images.FirstOrDefaultAsync(m => m.ImageId == id);
      if (image == null) {
        return null;
      }

      return ConvertImageToImageModel(image);
    }

    
    public async Task<int> Create(ImageModel imageModel) {
      Image image = ConvertImageModelToImage(imageModel);

      _context.Add(image);
      int success = await _context.SaveChangesAsync();

      return success;
    }

    public async Task Delete(int id) {
      var image = await _context.Images.FindAsync(id);
      
      _context.Images.Remove(image);
      await _context.SaveChangesAsync();
      
      return;
    }


    #region Private Methods
    private ImageModel ConvertImageToImageModel(Image image) {
      string imageBase64Data = Convert.ToBase64String(image.ImageData);
      string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);

      return new ImageModel {
        ImageId = image.ImageId,
        ImageName = image.ImageName,
        Title = image.Title,
        ImageDataURL = imageDataURL
      };
    }

    private Image ConvertImageModelToImage(ImageModel imageModel) {
      
      string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
      string extension = Path.GetExtension(imageModel.ImageFile.FileName);
      imageModel.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;


      MemoryStream ms = new MemoryStream();
      imageModel.ImageFile.CopyTo(ms);
      byte[] arr = ms.ToArray();

      return new Image {
        ImageId = imageModel.ImageId,
        Title = imageModel.Title,
        ImageName = imageModel.ImageName,
        ImageData = arr
      };
    }

    #endregion
  }
}
