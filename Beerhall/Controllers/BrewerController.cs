using Beerhall.Models.Domain;
using Beerhall.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Beerhall.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class BrewerController : Controller
    {

        private readonly IBrewerRepository _brewerRepository;
        private readonly ILocationRepository _locationRepository;

        public BrewerController(IBrewerRepository brewerRepository, ILocationRepository locationRepository)
        {
            _brewerRepository = brewerRepository;
            _locationRepository = locationRepository;

        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            IEnumerable<Brewer> brewers = _brewerRepository.GetAll().ToList();
            ViewData["TotalTurnover"] = brewers.Sum(e => e.Turnover);
            return View(brewers);
        }

        public IActionResult Edit(int id)
        {
            Brewer brewer = _brewerRepository.GetBy(id);
            ViewData["IsEdit"] = true;
            ViewData["Locations"] = GetLocationsAsSelectedList();

            return View(new BrewerEditViewModel(brewer));
        }

        [HttpPost]
        public IActionResult Edit(BrewerEditViewModel brewerEditViewModel, int id)
        {
            

            if (ModelState.IsValid)
            {
                Brewer brewer = null;
                try
                {
                    brewer = _brewerRepository.GetBy(id);
                    MapBrewerEditViewModelToBrewer(brewerEditViewModel, brewer);
                    _brewerRepository.SaveChanges();
                    TempData["message"] = $"You successfully updated brewer {brewer.Name}.";
                }
                catch (Exception)
                {

                    TempData["error"] = $"Sorry, something went wrong, brewer {brewer?.Name} was not updated...";
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["IsEdit"] = true;
            ViewData["Locations"] = GetLocationsAsSelectedList();
            return View(nameof(Edit), brewerEditViewModel);
            
        }

        private SelectList GetLocationsAsSelectedList()
        {
            return new SelectList(
                            _locationRepository.GetAll().OrderBy(l => l.Name),
                            nameof(Location.PostalCode),
                            nameof(Location.Name)
                        );
        }



        private void MapBrewerEditViewModelToBrewer(BrewerEditViewModel brewerEditViewModel, Brewer brewer)
        {
            brewer.Name = brewerEditViewModel.Name;
            brewer.Street = brewerEditViewModel.Street;
            brewer.Location = brewerEditViewModel.PostalCode == null ? null : _locationRepository.GetBy(brewerEditViewModel.PostalCode);
            brewer.Turnover = brewerEditViewModel.Turnover;
        }

       

        public IActionResult Create()
        {

            ViewData["Locations"] = GetLocationsAsSelectedList();
            ViewData["IsEdit"] = false;
            return View(nameof(Edit), new BrewerEditViewModel());
        }

        [HttpPost]
        public IActionResult Create(BrewerEditViewModel brewerEditViewModel)
        {
            
            if (ModelState.IsValid)
            {
                Brewer brewer = null;
                try
                {
                    brewer = new Brewer();
                    MapBrewerEditViewModelToBrewer(brewerEditViewModel, brewer);
                    _brewerRepository.Add(brewer);
                    _brewerRepository.SaveChanges();
                    TempData["message"] = $"You successfully added brewer {brewer.Name}.";
                }
                catch (Exception)
                {
                    TempData["error"] = $"Sorry, er liep iets fout, brouwer {brewer?.Name} kon niet worden gewijzigd";
                }
            }
            ViewData["IsEdit"] = false;
            ViewData["Locations"] = GetLocationsAsSelectedList();
            return View(nameof(Edit), brewerEditViewModel);

        }

        public IActionResult Delete(int id)
        {
            ViewData[nameof(Brewer.Name)] = _brewerRepository.GetBy(id).Name;

            return View();

        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {   
            Brewer brewer = null;
            try
            {
                brewer = _brewerRepository.GetBy(id);
                _brewerRepository.Delete(brewer);
                _brewerRepository.SaveChanges();
                TempData["message"] = $"You successfully deleted brewer { brewer.Name}.";
            }
            catch
            {
                TempData["error"] = $"Sorry, something went wrong, brewer { brewer?.Name} was not deleted…";
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(int id)
        {
            return View(_brewerRepository.GetByWithBeers(id));
        }

        public IActionResult BeerDelete(int id)
        {
            Beer beer = _brewerRepository.GetAll().SelectMany(e => e.Beers).SingleOrDefault(e => e.BeerId == id);
            return View(beer);
        }

        [HttpPost, ActionName("BeerDelete")]
        public IActionResult BeerDeleteConfirmed(int id, int brId)
        { 
            Beer beer = null;
            try
            {
               beer = _brewerRepository.GetAll().SelectMany(e => e.Beers).SingleOrDefault(e => e.BeerId == id);
                _brewerRepository.GetByWithBeers(brId).Beers.Remove(beer);
                _brewerRepository.SaveChanges();
                TempData["message"] = $"You successfully deleted beer { beer.Name}.";
            }
            catch (Exception e)
            {
                TempData["error1"] = $"{e.Message}";
                TempData["error"] = $"Sorry, something went wrong, beer { beer?.Name} was not deleted…";
            }
            return RedirectToAction(nameof(Index));

        }


        public IActionResult BeerAdd(int brId) {
            ViewData["Add"] = "Add a new beer here";
            BeerEditViewModel mevm = new BeerEditViewModel();
            
            mevm.BrewerId = brId;
            return View(mevm);
        }

        [HttpPost]
        public IActionResult BeerAdd(BeerEditViewModel beerEditViewModel, int brId)
        {
            Beer beer = new Beer();            
            MapBeerEditViewModelToBeer(beerEditViewModel, beer);
            _brewerRepository.GetByWithBeers(brId).Beers.Add(beer);
            _brewerRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
            
        }

        private void MapBeerEditViewModelToBeer(BeerEditViewModel beerEditViewModel, Beer beer)
        {
            beer.Name = beerEditViewModel.Name;
            beer.Price = beerEditViewModel.Price;
            beer.Description = beerEditViewModel.Description;
            beer.AlcoholByVolume = beerEditViewModel.Alchocol;
        }
    }
}
