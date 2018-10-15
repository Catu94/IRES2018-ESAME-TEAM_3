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

            var vmp = models
                .Select(x => new ProjectRowModel 
                {
                    Id = x.Id,
                    Key = x.Key,
                    Category = x.Category,
                    TotalCost = 0,
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