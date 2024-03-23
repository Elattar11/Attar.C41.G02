﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attar.C41.G02.DAL.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        [Display(Name = "Date Of Creation")]
        public DateTime DateOfCreation { get; set; }

    }
}
