using LanchesMac.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LanchesMac.ViewModels;

namespace LanchesMac.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lanchRepository;

        public LancheController(ILancheRepository lanchRepository)
        {
            _lanchRepository = lanchRepository;
        }

        public IActionResult List()
        {

            // var lanches = _lanchRepository.Lanches;
            //return View(lanches);

            var lancheListViewModel = new LancheListViewModel();
            lancheListViewModel.Lanches = _lanchRepository.Lanches;
            lancheListViewModel.CategoriaAtual = "Categoria Atual"; 

            return View(lancheListViewModel);   



        }
    }
}
