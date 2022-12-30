using Microsoft.AspNetCore.Mvc;
using ProduitFamilleMVC_SQLServer.Models;
using ProduitFamilleMVC_SQLServer.Models.Repositories;

namespace ProduitFamilleMVC_SQLServer.Controllers
{
    public class FamilleController : Controller
    {
        // injection du service FamilleRepository
        public IRepository<Famille> Repository { get; }
        public FamilleController(IRepository<Famille> repository)
        {
            Repository = repository;
        }

        public IActionResult Index()
        {
            var familles = Repository.Lister();
            return View(familles);
        }
        public IActionResult Details(int id)
        {
            var famille = Repository.ListerSelonId(id);
            return View(famille);
        }
        public IActionResult Create()
        {
           return View();
        }
        [HttpPost]
        public IActionResult Create(Famille famille)
        {
            try
            {
                Repository.Ajouter(famille);
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return View();
            }
         
        }
        public IActionResult Edit(int id)
        {
            var famille = Repository.ListerSelonId(id);
            return View(famille);
        }
        [HttpPost]
       public IActionResult Edit(int id, Famille famille)
        {
            try
            {
                Repository.Modifier(id,famille);
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return View();
            }

        }
        public IActionResult Delete(int id)
        {
            var famille = Repository.ListerSelonId(id);
            return View(famille);
        }
        [HttpPost]
        public IActionResult Delete(int id, Famille famille)
        {
            try
            {
                Repository.Supprimer(id);
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return View();
            }

        }


    }
}
