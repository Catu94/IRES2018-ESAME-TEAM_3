using Gruppo3Esame.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Gruppo3Esame.Infrastructure.Exceptions;
using Gruppo3Esame.ViewModel;

namespace Gruppo3Esame.DataAccess
{
    public class SqlRepository : IdentityDbContext, IRepository<Employee>, IRepository<Project> { 

        public SqlRepository(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }

        List<Employee> IRepository<Employee>.GetAll()
        {
            return Employees
                .Include(x => x.PE)
                .ThenInclude(x=> x.Project)
                .ToList();
        }

        Employee IRepository<Employee>.Get(int id)
        {
            return Employees
                .Include(x => x.PE)
                .ThenInclude(x => x.Project)
                .FirstOrDefault(x => x.Id == id);

            // this transforms the IQueryable in an IEnumerable
            // this means that no SQL query is generated,
            // but the items are entirely loaded in memory as C# objects
            // return SuperHeroes
            //     .AsEnumerable()
            //     .Where(x => { return x.Id == id; })
            //     .FirstOrDefault();
        }

        int IRepository<Employee>.Insert(Employee model)
        {
            Employees.Add(model);
            return SaveChanges();
        }

        void IRepository<Employee>.Update(Employee model)
        {
            Employees.Update(model);
            SaveChanges();
        }

        void IRepository<Employee>.Delete(int id)
        {
            var model = Employees.FirstOrDefault(x => x.Id == id);
            Employees.Remove(model);
            SaveChanges();
        }

        List<Project> IRepository<Project>.GetAll()
        {
            return Projects.
                Include(x => x.PE)
                .ThenInclude(x => x.Employee)
                .ToList();
        }

        Project IRepository<Project>.Get(int id)
        {
            var model = Projects
               .Include(x => x.PE)
                .ThenInclude(x => x.Employee)
                .FirstOrDefault(x => x.Id == id);

            if (model == null)
                throw new NotFoundException("Model not found");

            return model;
        }

        int IRepository<Project>.Insert(Project model)
        {
            Projects.Add(model);

            var result = SaveChanges();

            return result;
        }

        void IRepository<Project>.Update(Project model)
        {
            Projects.Update(model);
            SaveChanges();
        }

        void IRepository<Project>.Delete(int id)
        {
            try
            {
                var model = Projects.First(x => x.Id == id);

                Projects.Remove(model);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Model not found", ex);
            }
        }

        public DbSet<Gruppo3Esame.ViewModel.ProjectRowModel> ProjectRowModel { get; set; }
    }
}
