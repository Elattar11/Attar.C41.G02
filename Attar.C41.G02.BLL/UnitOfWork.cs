using Attar.C41.G02.BLL.Interfaces;
using Attar.C41.G02.BLL.Repositories;
using Attar.C41.G02.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attar.C41.G02.BLL
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly ApplicationDbContext _context;

        public IEmployeeRepository employeeRepository { get; set; }
        public IDepartmentRepository departmentRepository { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            employeeRepository = new EmployeeRepository(_context);
            departmentRepository = new DepartmentRepository(_context);
            
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
