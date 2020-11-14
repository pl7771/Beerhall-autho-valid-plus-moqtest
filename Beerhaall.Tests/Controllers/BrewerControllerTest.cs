using Beerhaall.Tests.Data;
using Beerhall.Controllers;
using Beerhall.Models.Domain;
using Beerhall.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Beerhaall.Tests.Controllers
{
    public class BrewerControllerTest
    {

        private BrewerController _controller;
        private Mock<IBrewerRepository> _brewerRepository;
        private Mock<ILocationRepository> _locationRepository;
        private readonly DummyApplicationDbContext _dummyContext;

        public BrewerControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _brewerRepository = new Mock<IBrewerRepository>();
            _locationRepository = new Mock<ILocationRepository>();
            _controller = new BrewerController(_brewerRepository.Object, _locationRepository.Object);
        }

        [Fact]
        public void Index_PassesOrderedListOfBrewersInViewResultModelAndStoresTotalTurnoverInViewData()
        {
            //aarange
            //  _brewerRepository.Setup(m => m.GetAll()).Returns(_dummyContext.Brewers);
            Brewer bavik = new Brewer("Bavik") { BrewerId = 1 };
            Brewer moortgat = new Brewer("Duvel Moortgat") { BrewerId = 2 };
            _brewerRepository.Setup(m => m.GetAll()).Returns(new List<Brewer>() { bavik, moortgat });

            //act
            //IActionResult result = _controller.Index();
            var result = Assert.IsType<ViewResult>(_controller.Index());
            var brewersInModel = Assert.IsType<List<Brewer>>(result.Model);
            //assert

            Assert.Equal(2, brewersInModel.Count);
            Assert.Equal("Bavik", brewersInModel[0].Name);
            Assert.Equal("Duvel Moortgat", brewersInModel[1].Name);
            Assert.Equal(0, result.ViewData["TotalTurnover"]);
        }

        [Fact]
        public void Edit_ValidEdit_UpdatesAndPersistsBrewerAndRedirectsToActionIndex()
        {

            Brewer bavik = new Brewer("Bavik") { BrewerId = 1 };
            Brewer moortgat = new Brewer("Duvel Moortgat") { BrewerId = 2 };
            _brewerRepository.Setup(m => m.GetBy(1)).Returns(bavik);


            var brewerBavik = new BrewerEditViewModel(bavik);
            var result = Assert.IsType<RedirectToActionResult>(_controller.Edit(brewerBavik, 1));


            Assert.Equal("Index", result?.ActionName);
            Assert.Equal("Bavik", bavik.Name);

            _brewerRepository.Verify(m => m.SaveChanges(), Times.Once());
        }


        [Fact]
        public void Index_PassesOrderedListOfBrewersInViewResultModelAndStoresTotalTurnoverInViewData2()
        {
            _brewerRepository.Setup(m => m.GetAll()).Returns(_dummyContext.Brewers);
            var result = Assert.IsType<ViewResult>(_controller.Index());
            var brewersInModel = Assert.IsType<List<Brewer>>(result.Model);
            Assert.Equal(3, brewersInModel.Count);
            Assert.Equal("De Leeuw", brewersInModel[0].Name);
            Assert.Equal("Duvel Moortgat", brewersInModel[1].Name);
            Assert.Equal("Bavik", brewersInModel[2].Name);
            Assert.Equal(20050000, result.ViewData["TotalTurnover"]);
        }

        [Fact]
        public void Create_ModelStateErrors_DoesNotCreateNorPersistsBrewerAndPassesViewModelAndViewDataToEditView()
        {
            _locationRepository.Setup(m => m.GetAll()).Returns(_dummyContext.Locations);
            _brewerRepository.Setup(m => m.GetBy(1)).Returns(_dummyContext.Bavik);
            BrewerEditViewModel brewerEvm = new BrewerEditViewModel(_dummyContext.Bavik);
            _controller.ModelState.AddModelError("111", "Error messageRRR");
            var result = Assert.IsType<ViewResult>(_controller.Create(brewerEvm));
            Assert.Equal("Edit", result.ViewName);
            Assert.Equal(brewerEvm, result.Model);
            var locations = Assert.IsType<SelectList>(result.ViewData["Locations"]);
            Assert.Equal(3, locations.Count());
            var isEdit = Assert.IsType<bool>(result.ViewData["IsEdit"]);
            Assert.False(isEdit);
            _brewerRepository.Verify(m => m.Add(It.IsAny<Brewer>()), Times.Never());
            _brewerRepository.Verify(m => m.SaveChanges(), Times.Never());
        }

        [Fact]
        public void EditTestModelOefening()
        {
            _locationRepository.Setup(m => m.GetAll()).Returns(_dummyContext.Locations);
            _brewerRepository.Setup(m => m.GetBy(1)).Returns(_dummyContext.Bavik);
            BrewerEditViewModel brewerEvm = new BrewerEditViewModel(_dummyContext.Bavik);
            _controller.ModelState.AddModelError("111", "Error messageRRR");
            var result = Assert.IsType<ViewResult>(_controller.Edit(brewerEvm, brewerEvm.BrewerId));
            Assert.Equal("Edit", result.ViewName);
            Assert.Equal(brewerEvm, result.Model);
            Assert.True((bool)result.ViewData["IsEdit"]);
            _brewerRepository.Verify(m => m.Add(It.IsAny<Brewer>()), Times.Never());
            _brewerRepository.Verify(m => m.SaveChanges(), Times.Never());
        }


    }
}
