using System;
using System.Collections.Generic;
using Beerhall.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Beerhall.Models.ViewModels.BrewerViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Beerhall.Controllers {
    [Authorize(Policy = "AdminOnly")]
    public class BrewerController : Controller {
        private readonly IBrewerRepository _brewerRepository;
        private readonly ILocationRepository _locationRepository;

        public BrewerController(IBrewerRepository brewerRepository, ILocationRepository locationRepository) {
            _brewerRepository = brewerRepository;
            _locationRepository = locationRepository;
        }

        [AllowAnonymous]
        public IActionResult Index() {
            IEnumerable<Brewer> brewers = _brewerRepository.GetAll().OrderBy(b => b.Name).ToList();
            ViewData["TotalTurnover"] = brewers.Sum(b => b.Turnover);
            return View(brewers);
        }

        public IActionResult Edit(int id) {
            Brewer brewer = _brewerRepository.GetBy(id);
            if (brewer == null)
                return NotFound();
            ViewData["Locations"] = GetLocationsAsSelectList(brewer.Location?.PostalCode);
            return View(new EditViewModel(brewer));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditViewModel brewerEditViewModel) {
            if (ModelState.IsValid) {
                try {
                    Brewer brewer = _brewerRepository.GetBy(brewerEditViewModel.BrewerId);
                    MapBrewerEditViewModelToBrewer(brewerEditViewModel, brewer);
                    _brewerRepository.SaveChanges();
                    TempData["message"] = $"You successfully updated brewer {brewer.Name}.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e) {
                    ModelState.AddModelError("", e.Message);
                }
            }
            ViewData["Locations"] = GetLocationsAsSelectList(brewerEditViewModel?.PostalCode);
            return View(brewerEditViewModel);
        }

        public IActionResult Create() {
            ViewData["Locations"] = GetLocationsAsSelectList(null);
            return View(nameof(Edit), new EditViewModel(new Brewer()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EditViewModel brewerEditViewModel) {
            if (ModelState.IsValid) {
                try {
                    Brewer brewer = new Brewer();
                    MapBrewerEditViewModelToBrewer(brewerEditViewModel, brewer);
                    _brewerRepository.Add(brewer);
                    _brewerRepository.SaveChanges();
                    TempData["message"] = $"You successfully added brewer {brewer.Name}.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e) {
                    ModelState.AddModelError("", e.Message);
                }
            }
            ViewData["Locations"] = GetLocationsAsSelectList(brewerEditViewModel?.PostalCode);
            return View(nameof(Edit), brewerEditViewModel);
        }

        public IActionResult Delete(int id) {
            Brewer brewer = _brewerRepository.GetBy(id);
            if (brewer == null)
                return NotFound();
            ViewData[nameof(Brewer.Name)] = brewer.Name;
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id) {
            Brewer brewer = null;
            try {
                brewer = _brewerRepository.GetBy(id);
                _brewerRepository.Delete(brewer);
                _brewerRepository.SaveChanges();
                TempData["message"] = $"You successfully deleted brewer {brewer.Name}.";
            }
            catch {
                TempData["error"] = $"Sorry, something went wrong, brewer {brewer?.Name} was not deleted...";
            }
            return RedirectToAction(nameof(Index));
        }

        private SelectList GetLocationsAsSelectList(string postalCode) {
            return new SelectList(
                _locationRepository.GetAll().OrderBy(l => l.Name),
                nameof(Location.PostalCode),
                nameof(Location.Name),
                postalCode);
        }

        private void MapBrewerEditViewModelToBrewer(EditViewModel brewerEditViewModel, Brewer brewer) {
            brewer.Name = brewerEditViewModel.Name;
            brewer.Street = brewerEditViewModel.Street;
            brewer.Location = brewerEditViewModel.PostalCode == null
                ? null
                : _locationRepository.GetBy(brewerEditViewModel.PostalCode);
            brewer.ContactEmail = brewerEditViewModel.ContactEmail;
            brewer.DateEstablished = brewerEditViewModel.DateEstablished;
            brewer.Description = brewerEditViewModel.Description;
            brewer.Turnover = brewerEditViewModel.Turnover;
        }
    }
}
