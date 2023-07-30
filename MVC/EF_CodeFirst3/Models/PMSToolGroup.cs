using EF_CodeFirst2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.DynamicData;
using System.Web.Services.Description;

namespace EF_CodeFirst3.Models
{
    [Table("dbo.PMSToolGroup")]
    public class PMSToolGroup
    {
        [Key]
        public string ToolGroup { get; set; } // ToolGroup 即 Grade

        [ForeignKey("OwnerObj")]
        public string OWNER { get; set; }
        public PMSOwner OwnerObj { get; set; }

        //public PMSOwner OWNER { get; set; }


        // 
        //public ICollection<PMSOwner> PMSOwners { get; } = new List<PMSOwner>();

        // 一個年級有很多學生
        //    Grade has many Student
        //                    PMSOwnser 即 Studend
        // 一個OWNERID 有很多 PMSOwners
        // 一個 ToolGroup 有很多 PMSOwners
        // MKF10 XX
        // MKF10 xxx 重複的資料


    }
}
