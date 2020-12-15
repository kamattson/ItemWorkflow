using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace item_workflow.Model
{
    [Table("Workflow_Approval")]
    public class Approval
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApprovalId { get; set; }
        Item item { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApproverUser { get; set; }
        public string ApprovalTypeCode { get; set; }
        public string ApproverText { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ApprovalDate { get; set; }

   


    }
}
