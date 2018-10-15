using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gruppo3Esame.DataAccess;
using Gruppo3Esame.Models;
using Gruppo3Esame.ViewModel;
using Microsoft.AspNetCore.Mvc;
using static Gruppo3Esame.Infrastructure.Exceptions;

namespace Gruppo3Esame.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IRepository<Project> _repository;
        private readonly IRepository<Employee> _employeeRepository;
        public ProjectController(IRepository<Project> repository, IRepository<Employee> employeeRepository)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult List()
        {
            var models = _repository.GetAll();

            var sumtry = models[0].PE.Where(p => p.ProjectId == models[0].Id);

            var vmp = models
                .Select(x => new ProjectRowModel 
                {
                    Key = x.Key,
                    Category = x.Category,
                    //TotalCost = x.PE.Where(p => p.ProjectId == x.Id)
                    //                .Where(e => e.EmployeeId == e.Employee.Id)
                    //                .Aggregate(0, s => s + ,
                });

            return View(vmp);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            try
            {
                var model = id == null
                    ? new Project()
                    : _repository.Get((int)id);

                return View(model);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Edit(Project model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                if (model.Id == 0)
                    _repository.Insert(model);
                else
                    _repository.Update(model);

                return RedirectToAction(nameof(List));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var model = _repository.Get(id);

                return View(model);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            try
            {
                _repository.Delete(id);

                TempData["Message"] = "Project successfully deleted!";

                return RedirectToAction(nameof(List));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}