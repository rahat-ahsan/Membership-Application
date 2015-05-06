using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.ComponentModel;


namespace Memberships.Entities
{
   
        [Table("Content")]
        public class Content
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            [MaxLength(255)]
            [Required]
            public string Title { get; set; }
            [MaxLength(2048)]
            public string Description { get; set; }
            [MaxLength(1024)]
            public string Url { get; set; }
            [AllowHtml]
            public string HTML { get; set; }
            [NotMapped]
            public string HTMLShort { get { return (HTML == null || HTML.Length <= 50) ? HTML : HTML.Substring(0, 50); } }
            public int ContentTypeId { get; set; }
            public int ChapterId { get; set; }
            public int PartId { get; set; }
            [DisplayName("Chapter")]
            public virtual ICollection<Chapter> Chapters { get; set; }
            [DisplayName("Part")]
            public virtual ICollection<Part> Parts { get; set; }
            [DisplayName("Content Type")]
            public virtual ICollection<ContentType> ContentTypes { get; set; }
            public virtual ICollection<Product> Products { get; set; }
        }
    }
