using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace image_repository.Models {
  [JsonObject]
  public class ImageModel {

    [JsonProperty]
    public int ImageId { get; set; }

    [Required]
    [JsonProperty]
    [DisplayName("Image Title")]
    public string Title { get; set; }

    [JsonProperty]
    [DisplayName("Image Name")]
    public string ImageName { get; set; }

    [Required]
    [JsonIgnore]
    [DisplayName("Upload Image")]
    public IFormFile ImageFile { get; set; }

    [JsonProperty]
    public string ImageDataURL { get; set; }
  }
}
