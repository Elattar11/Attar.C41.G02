using Attar.C41.G02.BLL.Interfaces;
using Attar.C41.G02.BLL.Repositories;
using Attar.C41.G02.DAL.Data;
using Attar.C41.G02.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attar.C41.G02.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private Hashtable _repositories;
        public IEmployeeRepository employeeRepository { get; set; }
        public IDepartmentRepository departmentRepository { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            employeeRepository = new EmployeeRepository(_context);
            departmentRepository = new DepartmentRepository(_context);
            
        }
        public IGenericRepository<T> Repository<T>() where T : ModelBase
        {
            var key = typeof(T).Name;

            if (!_repositories.ContainsKey(key))
            {
                if (key == nameof(Employee))
                {
                    var repository = new GenericRepository<T>(_context);
                    _repositories.Add(key, repository);

                }
                else
                {
                    var repository = new GenericRepository<T>(_context);
                    _repositories.Add(key, repository);
                }
            }

            return _repositories[key] as IGenericRepository<T>;
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
