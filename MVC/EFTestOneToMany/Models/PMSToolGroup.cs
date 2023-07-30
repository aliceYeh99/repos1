using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTestOneToMany.Models
{
    //[Table("dbo.PMSToolGroup")]
    //public partial class PMSToolGroup
    //{
    //    // 相當於 PeopleAddress
    //    [Key, ForeignKey("Owner")]
    //    public string OwnerId { get; set; }
    //    public string DESC { get; set; }

    //    public virtual Owner Owner { get; set; }
    //}
    // 上面是我以為的寫法，但是做不出來


    //[Table("dbo.PMSToolGroup")]
    //public partial class PMSToolGroup
    //{
    //    public PMSToolGroup()
    //    {
    //        this.Owners = new HashSet<Owner>();
    //    }
    //    // PMSToolGroup 相當於 PeopleAddress
    //    // Owner 相當於 People

    //    public string OwnerId { get; set; }

    //    public string PMSToolGroupId { get; set; }

    //    public virtual ICollection<Owner> Owners { get; set; }
    //}

    // 下面是 ok 的寫法
    [Table("dbo.PMSToolGroup")]
    public partial class PMSToolGroup
    {
        public PMSToolGroup()
        {
            //this.Owners = new HashSet<Owner>();
        }
        // PMSToolGroup 相當於 PeopleAddress
        // Owner 相當於 People
        [ForeignKey("Owner")]
        public string OwnerId { get; set; }
        public virtual Owner Owner { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PMSToolGroupId { get; set; }

        //public virtual ICollection<Owner> Owners { get; set; }
    }



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
