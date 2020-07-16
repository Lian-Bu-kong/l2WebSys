using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataModel.DB
{
    public class CoilSchedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("鋼捲編號")]
        public string Id { get; set; }
        [DisplayName("排序號")]
        public short SeqNo { get; set; }
        [DisplayName("系統排序號")]
        public float SortNo { get; set; }
        [DisplayName("更新來源")]
        public string UpdateSource { get; set; }
        [DisplayName("建立時間")]
        public DateTime CreateTime { get; set; } 
    }
}
