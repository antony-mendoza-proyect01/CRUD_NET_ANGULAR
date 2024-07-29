using Microsoft.EntityFrameworkCore;
using BackEndApi.Models;
using BackEndApi.Services.Contrato;

namespace BackEndApi.Services.implementacion
{
    public class GeneroService : IGeneroService
    {
        private CrudPeliculaContext _dbcontext;

        public GeneroService(CrudPeliculaContext dbContext)
        {
            _dbcontext = dbContext;
        }

       //Metodo para retornar la lista  de Genero
        public async Task<List<Genero>> GetList()
        {
            try { 
                List<Genero> list = new List<Genero>();

                list = await _dbcontext.Generos.ToListAsync();
                return list;


            }catch (Exception ex) {
                throw ex;
            }
        }
    }
}
