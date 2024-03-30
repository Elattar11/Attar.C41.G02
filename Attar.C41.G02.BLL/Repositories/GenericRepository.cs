using Attar.C41.G02.BLL.Interfaces;
using Attar.C41.G02.DAL.Data;
using Attar.C41.G02.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attar.C41.G02.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _context; //NULL

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            //_context.Add(entity); //EF Core 3.1 New Feature

        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            //_context.Update(entity); //EF Core 3.1 New Feature
        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            //_context.Remove(entity); //EF Core 3.1 New Feature
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)_context.Employees.Include(E => E.Department).AsNoTracking().ToList();
            }
            else
            {
                return _context.Set<T>().Include(E => E).AsNoTracking().ToList();
            }
        }
        public T Get(int id)
        {
            //return _context.Employees.Find(id);

            return _context.Find<T>(id);

            /// var department = _context.Employees.Local.Where(D => D.Id == id).FirstOrDefault();
            /// if (department is null)
            /// {
            ///     department = _context.Employees.Where(D => D.Id == id).FirstOrDefault();
            /// }
            /// return department;
        }
    }
}
