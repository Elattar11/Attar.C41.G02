using Attar.C41.G02.BLL.Interfaces;
using Attar.C41.G02.DAL.Data;
using Attar.C41.G02.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attar.C41.G02.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        //private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context) :base(context)
        {
            //_context = context;
        }
        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return _context.Employees.Where(E => E.Address.ToLower() == address.ToLower()); 
        }
    }
}
