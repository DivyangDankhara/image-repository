using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace image_repository.Models {
  public class ImageModel {
    
    public int ImageId { get; set; }

    [Required]
    [DisplayName("Image Title")]
    public string Title { get; set; }


    [DisplayName("Image Name")]
    public string ImageName { get; set; }

    [Required]
    [DisplayName("Upload Image")]
    public IFormFile ImageFile { get; set; }

    public string ImageDataURL { get; set; }
  }
}
