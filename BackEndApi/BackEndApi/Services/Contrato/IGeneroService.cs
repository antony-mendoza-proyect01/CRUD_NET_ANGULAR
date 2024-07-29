using BackEndApi.Models;

namespace BackEndApi.Services.Contrato
{
    public interface IGeneroService
    {

        Task<List<Genero>> GetList();
    }
}
