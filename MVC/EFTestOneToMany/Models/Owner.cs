using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTestOneToMany.Models
{
    [Table("dbo.Owner")]
    public partial class Owner
    {
        public Owner() // 相當於 People
        {
            this.PMSToolGroups = new HashSet<PMSToolGroup>();
        }

        [Key]
        public string OwnerId { get; set; }
        public string DESC { get; set; }
        public virtual ICollection<PMSToolGroup> PMSToolGroups { get; set; }
    }


    //  下面是 ok 的寫法
    //[Table("dbo.Owner")]
    //public partial class Owner
    //{
    //    public Owner() // 相當於 People
    //    {
    //        this.PMSToolGroups = new HashSet<PMSToolGroup>();
    //    }
    //    public string OwnerId { get; set; }
    //    public string DESC { get; set; }
    //    public virtual ICollection<PMSToolGroup> PMSToolGroups { get; set; }
    //}
}
