using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Memberships.Areas.Admin.Models
{
    public class SmallButtonModel
    {
        public string Action { get; set; }
        public string Text { get; set; }
        public string Glyph { get; set; }
        public string ButtonType { get; set; }
        public int? ItemId { get; set; }
        public int? ProductId { get; set; }
        public int? ContentId { get; set; }
        public int? SubscriptionId { get; set; }
        public bool DisplayText { get; set; }
        public string ActionParameters
        {
            get
            {
                StringBuilder param = new StringBuilder();
                if (ItemId != null) param.Append(String.Format("?id={0}", ItemId));
                if (ProductId != null) param.Append(String.Format("{0}productId={1}", param.Length.Equals(0) ? "?" : "&", ProductId));
                if (ContentId != null) param.Append(String.Format("{0}contentId={1}", param.Length.Equals(0) ? "?" : "&", ContentId));
                if (SubscriptionId != null) param.Append(String.Format("{0}subscriptionId={1}", param.Length.Equals(0) ? "?" : "&", SubscriptionId));
                return param.ToString();
            }
        }

        public string ShowText
        {
            get { return DisplayText ? "" : "sr-only"; }
        }
    }
}