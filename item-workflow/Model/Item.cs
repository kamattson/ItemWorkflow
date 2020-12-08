using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace item_workflow.Model
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string ArticleSourceFlag { get; set; }
        public string Vendor { get; set; }
        public string ApprovalStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastUpdateDate { get; set; }

    }
}
