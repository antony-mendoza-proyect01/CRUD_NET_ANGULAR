using Microsoft.EntityFrameworkCore;
using BackEndApi.Models;
using BackEndApi.Services.Contrato;

namespace BackEndApi.Services.implementacion
{
    //usamos referencia del interfaz
    public class PeliculaService : IPeliculaService
    {
        private CrudPeliculaContext _dbcontext;
        
        public PeliculaService(CrudPeliculaContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<List<Pelicula>> GetList()
        {
            try {

                List<Pelicula> list = new List<Pelicula>();
                //incluimos la referencia de Genero atraves de la llave foranea
                list = await _dbcontext.Peliculas.Include(plc => plc.IdGeneroNavigation).ToListAsync();
                return list;

            }catch(Exception ex) {

                throw ex;
            }
        }

        public async Task<Pelicula> Get(int idPelicula)
        {
            try
            {
                Pelicula? encontrado = new Pelicula();

                encontrado = await _dbcontext.Peliculas.Include(plc => plc.IdGeneroNavigation).
                    Where(e=>e.IdPelicula == idPelicula).FirstOrDefaultAsync();

                return encontrado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Pelicula> Add(Pelicula modelo)
        {
            try
            {
                _dbcontext.Peliculas.Add(modelo);
                await _dbcontext.SaveChangesAsync();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(Pelicula modelo)
        {
            try
            {
                _dbcontext.Peliculas.Update(modelo);
                await _dbcontext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(Pelicula modelo)
        {
            try
            {
                _dbcontext.Peliculas.Remove(modelo);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
