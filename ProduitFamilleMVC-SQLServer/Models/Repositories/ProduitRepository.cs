using ProduitFamilleMVC_SQLServer.ViewModels;
using System.Data.SqlClient;

namespace ProduitFamilleMVC_SQLServer.Models.Repositories
{
    public class ProduitRepository : IRepository<ProduitFamilleViewModel>
    {
        private IList<ProduitFamilleViewModel> produits;
        public void Ajouter(ProduitFamilleViewModel element)
        {
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //récupérer le max de id dans la table Produit
                SqlCommand cmd = new SqlCommand("SELECT MAX(id) as Maxx FROM Produit", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    element.ProduitId = int.Parse(reader["Maxx"].ToString()) + 1;
                }
                reader.Close();
                //insertion d'une nouvelle famille
                string commandText = "insert into Produit(id,reference,designation,description," +
                    "disponible,famille_id) values (" +
                     element.ProduitId + ",'" + element.Reference + "','" + element.Designation +
                     "','" + element.Description + "'," + (element.disponible?1:0) + "," + element.FamilleId +")";
                SqlCommand cmdi = new SqlCommand(commandText, conn);
                cmdi.ExecuteNonQuery();

            }

        }

        public IList<ProduitFamilleViewModel> Lister()
        {
            produits = new List<ProduitFamilleViewModel>();
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //récupérer le max de id dans la table Famille
                SqlCommand cmd = new SqlCommand("SELECT Produit.id," +
                    "Produit.reference,Produit.designation,Produit.description," +
                    "Produit.disponible,Produit.famille_id,Famille.nom from " +
                    "Produit left outer join famille " +
                    "on produit.famille_id=famille.id", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var produit = new ProduitFamilleViewModel()
                    {
                        ProduitId = Convert.ToInt32(reader["id"]),
                        Reference = reader["reference"].ToString(),
                        Designation = reader["designation"].ToString(),
                        Description = reader["description"].ToString(),
                        disponible = Convert.ToBoolean(reader["disponible"]),
                        FamilleId = Convert.ToInt32(reader["famille_id"]),
                        FamilleNom = reader["nom"].ToString(),
                    };
                    produits.Add(produit);
                }
            }
            return produits; ;
        }

        public ProduitFamilleViewModel ListerSelonId(int id)
        {
            return produits.Single(a => a.ProduitId == id);
        }

        public void Modifier(int id, ProduitFamilleViewModel element)
        {
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //mettre à jour une famille
                string commandText = "update produit set reference='" + element.Reference + "'," +
                    "designation='" + element.Designation + "'," +
                    "description='" + element.Description + "'," +
                    "disponible=" + (element.disponible?1:0) + "," +
                    "famille_id=" + element.FamilleId + " where id=" + element.ProduitId;
                SqlCommand cmdi = new SqlCommand(commandText, conn);
                cmdi.ExecuteNonQuery();

            }
        }

        public void Supprimer(int id)
        {
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //mettre à jour une famille
                string commandText = "delete from Produit where id=" + id;
                SqlCommand cmdi = new SqlCommand(commandText, conn);
                cmdi.ExecuteNonQuery();

            }
        }
    }
}
