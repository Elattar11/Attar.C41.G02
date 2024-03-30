﻿using Attar.C41.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attar.C41.G02.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //public IEmployeeRepository employeeRepository { get; set; }
        //public IDepartmentRepository departmentRepository { get; set; }

        IGenericRepository<T> Repository<T>() where T : ModelBase ;

        int Complete();
    }
}
