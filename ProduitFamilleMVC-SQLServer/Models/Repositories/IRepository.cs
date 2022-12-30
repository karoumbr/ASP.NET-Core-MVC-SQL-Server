namespace ProduitFamilleMVC_SQLServer.Models.Repositories
{
    public interface IRepository<T>
    {
        IList<T> Lister();
        T ListerSelonId(int id);
        void Ajouter(T element);
        void Modifier(int id, T element);
        void Supprimer(int id);
    }
}
