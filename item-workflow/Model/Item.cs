﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace item_workflow.Model
{
    [Table("Workflow_Header")]
    public class Item
    {
        [Key]
        [Column("NIO_request_ID")]
        public int RequestId { get; set; }
        [Column("workflow_id")]
        public Guid WorkflowId { get; set; }
        public string ProductTitle { get; set; }
        public string ArticleSourceFlag { get; set; }
        public string Vendor { get; set; }
        public int Price { get; set; }
        public string HazardousFlag { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastUpdateDate { get; set; }

        public List<Approval> Approvals { get; set; }


        public override string ToString()
        {
            return "WorkflowId: " + WorkflowId +
                "ProductTitle " + ProductTitle +
                "ArticleSourceFlag " + ArticleSourceFlag +
                "Vendor " + Vendor +
                "Price " + Price +
                "HazardousFlag" + HazardousFlag +
                "LastUpdateDate " + LastUpdateDate;
        }

    }
}
