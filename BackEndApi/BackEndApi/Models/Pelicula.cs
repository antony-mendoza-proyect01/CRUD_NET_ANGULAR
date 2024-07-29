using System;
using System.Collections.Generic;

namespace BackEndApi.Models;

public partial class Pelicula
{
    public int IdPelicula { get; set; }

    public string? NombrePelicula { get; set; }

    public int? IdGenero { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual Genero? IdGeneroNavigation { get; set; }
}
