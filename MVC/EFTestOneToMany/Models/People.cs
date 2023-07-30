using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTestOneToMany.Models
{
    [Table("People")]
    public partial class People
    {
        public People()
        {
            this.PeopleAddress = new HashSet<PeopleAddress>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int PeopleId
        {
            get;
            set;
        }
        [Required]
        [StringLength(50)]
        public string FirstName
        {
            get;
            set;
        }
        [Required]
        [StringLength(50)]
        public string LastName
        {
            get;
            set;
        }
        public virtual ICollection<PeopleAddress> PeopleAddress
        {
            get;
            set;
        }
    }
}
