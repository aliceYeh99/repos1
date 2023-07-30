using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTestOneToMany.Models
{
    [Table("PeopleAddress")]
    public partial class PeopleAddress
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PeopleAddressId
        {
            get;
            set;
        }
        [Column(Order = 2)]
        [Key, ForeignKey("People")]
        public int PeopleId
        {
            get;
            set;
        }
        [Required]
        [StringLength(100)]
        public string AddressLine1
        {
            get;
            set;
        }
        [Required]
        [StringLength(100)]
        public string AddressLine2
        {
            get;
            set;
        }
        [StringLength(50)]
        public string City
        {
            get;
            set;
        }
        [StringLength(50)]
        public string State
        {
            get;
            set;
        }
        [StringLength(50)]
        public string Country
        {
            get;
            set;
        }
        public virtual People People  
        {  
            get;  
            set;  
        }
    } 
}  
