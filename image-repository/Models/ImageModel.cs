using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace image_repository.Models {
  public class ImageModel {
    [Key]
    public int ImageId { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Title { get; set; }

    [Column(TypeName = "varchar(100)")]
    [DisplayName("Image Name")]
    public string ImageName { get; set; }

    [Column(TypeName = "bytea")]
    public byte[] ImageData { get; set; }

    [Required]
    [NotMapped]
    [DisplayName("Upload Image")]
    public IFormFile ImageFile { get; set; }

    [NotMapped]
    public string ImageDataURL { get; set; }
  }
}
