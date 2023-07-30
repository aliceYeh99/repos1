using EF_CodeFirst3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CodeFirst2.Models
{
    [Table("dbo.PMS_OWNER")]
    public class PMSOwner
    {
        [Key]
        public string DEPT_CODE { get; set; }
        public string DESC { get; set; }


        public ICollection<PMSToolGroup> PMSToolGroups { get; } = new List<PMSToolGroup>(); // Collection navigation containing dependents
    }
}
