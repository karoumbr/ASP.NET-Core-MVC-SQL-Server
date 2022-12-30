using Microsoft.AspNetCore.Mvc;
using ProduitFamilleMVC_SQLServer.Models;
using ProduitFamilleMVC_SQLServer.Models.Repositories;
using ProduitFamilleMVC_SQLServer.ViewModels;

namespace ProduitFamilleMVC_SQLServer.Controllers
{
    public class ProduitController : Controller
    {
        //injection des services
        public IRepository<ProduitFamilleViewModel> ProduitRepository { get; }
        public IRepository<Famille> FamilleRepository { get; }
        public ProduitController(IRepository<ProduitFamilleViewModel> produitRepository, IRepository<Famille> familleRepository)
        {
            ProduitRepository = produitRepository;
            FamilleRepository = familleRepository;
        }

        public IActionResult Index()
        {
            var produits = ProduitRepository.Lister();
            return View(produits);
        }
        public IActionResult Details(int id)
        {
            var produit = ProduitRepository.ListerSelonId(id);
            return View(produit);
        }
        public IActionResult Create()
        {
            ProduitFamilleViewModel viewModel = new ProduitFamilleViewModel
            {
                Familles = FamilleRepository.Lister()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(ProduitFamilleViewModel viewModel)
        {
            try
            {
                var produit = new ProduitFamilleViewModel
                {
                    Reference = viewModel.Reference,
                    Designation = viewModel.Designation,
                    Description = viewModel.Description,
                    disponible = viewModel.disponible,
                    FamilleId = viewModel.FamilleId
                };
                ProduitRepository.Ajouter(produit);
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View();
            }
        }

        public IActionResult Edit(int id)
        {
            var produit = ProduitRepository.ListerSelonId(id);
            ProduitFamilleViewModel viewModel = new ProduitFamilleViewModel
            {
                ProduitId = produit.ProduitId,
                Reference = produit.Reference,
                Designation = produit.Designation,
                Description = produit.Description,
                disponible = produit.disponible,
                FamilleId = produit.FamilleId,
                Familles = FamilleRepository.Lister()
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit(int id, ProduitFamilleViewModel viewModel)
        {
            try
            {
                var editedProduit =  new ProduitFamilleViewModel
                {
                    ProduitId = viewModel.ProduitId,
                    Reference = viewModel.Reference,
                    Designation = viewModel.Designation,
                    Description = viewModel.Description,
                    disponible = viewModel.disponible,
                    FamilleId = viewModel.FamilleId,
                    Familles = FamilleRepository.Lister()
                };
                ProduitRepository.Modifier(id, editedProduit);
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View();
            }
      


        }

        public IActionResult Delete(int id)
        {
            var produit = ProduitRepository.ListerSelonId(id);
            return View(produit);
        }
        [HttpPost]
        public IActionResult Delete(int id, ProduitFamilleViewModel viewModel)
        {
            try
            {
                ProduitRepository.Supprimer(id);
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View();
            }



        }
    }
 
}
