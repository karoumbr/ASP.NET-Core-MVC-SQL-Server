using ProduitFamilleMVC_SQLServer.Models;

namespace ProduitFamilleMVC_SQLServer.ViewModels
{
    public class ProduitFamilleViewModel
    {
        public int ProduitId { get; set; }
        public string? Reference { get; set; }
        public string? Designation { get; set; }
        public string? Description { get; set; }
        public bool disponible { get; set; }
        public int FamilleId { get; set; }
        public string? FamilleNom { get; set; }
        public IList<Famille>? Familles { get; set; }

    }
}
