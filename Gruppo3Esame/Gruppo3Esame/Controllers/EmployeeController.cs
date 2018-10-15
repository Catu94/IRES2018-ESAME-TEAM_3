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
    public class EmployeeController : Controller
    {
        private readonly IRepository<Employee> _repository;
        private readonly IRepository<Project> _projectRepository;
        public EmployeeController(IRepository<Employee> repository, IRepository<Project> projectRepository)
        {
            _repository = repository;
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public IActionResult List()
        {
            var models = _repository.GetAll();

            var vme = models
                .Select(x => new EmployeeRowModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    LastName = x.LastName,
                    OpenProject = x.PE.Where(p => p.ProjectId == x.Id).Count(),
                });

            return View(vme);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            try
            {
                var model = id == null
                    ? new Employee()
                    : _repository.Get((int)id);

                return View(model);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Edit(Employee model)
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

                TempData["Message"] = "Employee successfully deleted!";

                return RedirectToAction(nameof(List));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}