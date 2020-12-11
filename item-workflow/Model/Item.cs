using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace item_workflow.Model
{
    public class Item
    {
        public int ItemId { get; set; }
        [Column("workflow_id")]
        public Guid WorkflowId { get; set; }
        public string Name { get; set; }
        public string ArticleSourceFlag { get; set; }
        public string Vendor { get; set; }
        public int Price { get; set; }
        public string ApprovalStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastUpdateDate { get; set; }


        public override string ToString()
        {
            return "ItemId: " + ItemId +
                "Name " + Name +
                "ArticleSourceFlag " + ArticleSourceFlag +
                "Vendor " + Vendor +
                "Price " + Price +
                "ApprovalStatus " + ApprovalStatus + 
                "LastUpdateDate " + LastUpdateDate;
        }

    }
}
