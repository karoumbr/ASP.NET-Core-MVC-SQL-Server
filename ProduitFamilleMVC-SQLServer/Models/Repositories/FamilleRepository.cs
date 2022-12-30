using System.Data.SqlClient;
namespace ProduitFamilleMVC_SQLServer.Models.Repositories
{
    public class FamilleRepository : IRepository<Famille>
    {
        public IList<Famille>? Familles { get; set; }
        public void Ajouter(Famille element)
        {
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //récupérer le max de id dans la table Famille
                SqlCommand cmd = new SqlCommand("SELECT MAX(id) as Maxx FROM FAMILLE",conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    element.id = int.Parse(reader["Maxx"].ToString())+1;
                }
                reader.Close();
                //insertion d'une nouvelle famille
                string commandText = "insert into Famille(id,nom) values (" +
                     element.id + ",'" + element.nom + "')";
                SqlCommand cmdi = new SqlCommand(commandText,conn);
                cmdi.ExecuteNonQuery();
               
            }
        }

        public IList<Famille> Lister()
        {
          Familles = new List<Famille>();
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //récupérer le max de id dans la table Famille
                SqlCommand cmd = new SqlCommand("SELECT id,nom FROM FAMILLE", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var famille = new Famille()
                    {
                        id = Convert.ToInt32(reader["id"]),
                        nom = reader["nom"].ToString()
                    };
                    Familles.Add(famille);
                }
             

            }
            return Familles; 

        }

        public Famille ListerSelonId(int id)
        {
            return Familles.Single(a => a.id==id);
        }

        public void Modifier(int id, Famille element)
        {
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
               //mettre à jour une famille
                string commandText = "update Famille set nom='" + element.nom + "' where id=" + 
                    element.id;
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
                string commandText = "delete from Famille where id=" + id;
                SqlCommand cmdi = new SqlCommand(commandText, conn);
                cmdi.ExecuteNonQuery();

            }
        }
    }
}
