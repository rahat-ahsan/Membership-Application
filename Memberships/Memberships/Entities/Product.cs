using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Memberships.Entities
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string Title { get; set; }
        [MaxLength(2048)]
        public string Description { get; set; }
        public int SubscriptionProductId { get; set; }
        public int ProductContentId { get; set; }
        public int SubscriptionId { get; set; }
        public int ContentId { get; set; }
        public ICollection<Content> Contents { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}