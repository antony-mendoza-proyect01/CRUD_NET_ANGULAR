using BackEndApi.Models;

namespace BackEndApi.Services.Contrato
{
    public interface IPeliculaService
    {
        Task<List<Pelicula>> GetList();
        Task<Pelicula> Get(int idPelicula);
        Task<Pelicula> Add(Pelicula modelo);
        Task<bool> Update(Pelicula modelo);
        Task<bool> Delete(Pelicula modelo);

    }
}
