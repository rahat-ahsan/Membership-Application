using Memberships.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memberships.Areas.Admin.Models
{
    public class ProductContentModel
    {
        public int ProductId { get; set; }
        public int ContentId { get; set; }

        public string ProductTitle { get; set; }
        public string ContentTitle { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Content> Contents { get; set; }
    }
}