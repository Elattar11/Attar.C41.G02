using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attar.C41.G02.DAL.Models
{
    public class Department : ModelBase
    {
        

        public string Code { get; set; }

        public string Name { get; set; }

        [Display(Name = "Date Of Creation")]
        public DateTime DateOfCreation { get; set; }

        
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

    }
}
