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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context; //NULL

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Add(Employee entity)
        {
            _context.Employees.Add(entity);
            return _context.SaveChanges();

        }
        public int Update(Employee entity)
        {
            _context.Employees.Update(entity);
            return _context.SaveChanges();
        }
        public int Delete(Employee entity)
        {
            _context.Employees.Remove(entity);
            return _context.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.AsNoTracking().ToList();
        }
        public Employee Get(int id)
        {
            //return _context.Employees.Find(id);

            return _context.Find<Employee>(id);

            /// var department = _context.Employees.Local.Where(D => D.Id == id).FirstOrDefault();
            /// if (department is null)
            /// {
            ///     department = _context.Employees.Where(D => D.Id == id).FirstOrDefault();
            /// }
            /// return department;
        }
    }
}
