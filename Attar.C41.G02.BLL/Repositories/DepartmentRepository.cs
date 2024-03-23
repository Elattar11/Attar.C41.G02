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
    internal class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context; //NULL

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Add(Department entity)
        {
            _context.Departments.Add(entity);
            return _context.SaveChanges();

        }
        public int Update(Department entity)
        {
            _context.Departments.Update(entity);
            return _context.SaveChanges();
        }
        public int Delete(Department entity)
        {
            _context.Departments.Remove(entity);
            return _context.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.AsNoTracking().ToList();
        }
        public Department Get(int id)
        {
            //return _context.Departments.Find(id);

            return _context.Find<Department>(id);

            /// var department = _context.Departments.Local.Where(D => D.Id == id).FirstOrDefault();
            /// if (department is null)
            /// {
            ///     department = _context.Departments.Where(D => D.Id == id).FirstOrDefault();
            /// }
            /// return department;
        }

        

    }
}
