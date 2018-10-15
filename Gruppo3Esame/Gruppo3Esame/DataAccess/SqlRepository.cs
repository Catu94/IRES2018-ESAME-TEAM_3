using Gruppo3Esame.Models;
using Gruppo3Esame.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gruppo3Esame.DataAccess
{
    public class SqlRepository : IdentityDbContext, IRepository<Project>, IRepository<Employee>
    {
        public SqlRepository(DbContextOptions options) : base(options)
        { }

        public DbSet<Project> Project { get; set; }
        public DbSet<Employee> Employee { get; set; }

        List<Employee> IRepository<Employee>.GetAll()
        {
            return Employee
                .Include(e => e.PE)
                    .ThenInclude(q => q.Project)
                .ToList();
        }

        Employee IRepository<Employee>.Get(int id)
        {
            return Employee.FirstOrDefault(x => x.Id == id);
        }

        int IRepository<Employee>.Insert(Employee model)
        {
            Employee.Add(model);
            return SaveChanges();
        }

        void IRepository<Employee>.Update(Employee model)
        {
            Employee.Update(model);
            SaveChanges();
        }

        void IRepository<Employee>.Delete(int id)
        {
            var model = Employee.FirstOrDefault(x => x.Id == id);
            Employee.Remove(model);
            SaveChanges();
        }




        List<Project> IRepository<Project>.GetAll()
        {
            var list = Project
                .Include(e => e.PE)
                    .ThenInclude(q => q.Employee)
                .ToList();

            return list.Select(x => new ProjectRowModel
            {
                Key = x.Key,
                Category = x.Category,
                TotalCost = x.PE.Where(pe => pe.ProjectId == x.Id)
            });
        }

        Project IRepository<Project>.Get(int id)
        {
            return Project.FirstOrDefault(x => x.Id == id);
        }

        int IRepository<Project>.Insert(Project model)
        {
            Project.Add(model);
            return SaveChanges();
        }

        void IRepository<Project>.Update(Project model)
        {
            Project.Update(model);
            SaveChanges();
        }

        void IRepository<Project>.Delete(int id)
        {
            var model = Project.FirstOrDefault(x => x.Id == id);
            Project.Remove(model);
            SaveChanges();
        }   
    }
}
