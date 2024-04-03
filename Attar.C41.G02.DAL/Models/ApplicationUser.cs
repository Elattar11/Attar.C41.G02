﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attar.C41.G02.DAL.Models
{
	public class ApplicationUser : IdentityUser
	{
        public string FName { get; set; }
        public string LName { get; set; }
        public bool IsAgre { get; set; }

    }
}
