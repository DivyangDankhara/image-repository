using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace image_repository.Models {
  public class ImageModel {
    [Key]
    public int ImageId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Title { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    [DisplayName("Image Name")]
    public string ImageName { get; set; }

    [Required]
    [NotMapped]
    [DisplayName("Upload Image")]
    public IFormFile ImageFile { get; set; }
  }
}
