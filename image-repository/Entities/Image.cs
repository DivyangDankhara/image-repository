using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace image_repository.Entities {
  public class Image {
    [Key]
    public int ImageId { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string Title { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string ImageName { get; set; }

    [Column(TypeName = "bytea")]
    public byte[] ImageData { get; set; }
  }
}
