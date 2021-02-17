using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataModel.DB
{
    public class CoilPDI
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("入口鋼捲編號")]
        public string CoilScheduleID { get; set; }

        [DisplayName("入口鋼捲寬度")]
        public float EntryCoilWidth { get; set; }

        [DisplayName("入口鋼捲長度")]
        public float EntryCoilLength { get; set; }

        [DisplayName("入口鋼捲重量")]
        public float EntryCoilWeight { get; set; }

        [DisplayName("建立時間")]
        public DateTime CreateTime { get; set; }
    }
}
