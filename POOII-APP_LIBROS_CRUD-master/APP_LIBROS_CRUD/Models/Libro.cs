namespace APP_LIBROS_CRUD.Models
{
    public class Libro
    {
        public int IDLibro { get; set; }
        public string Titulo { get; set; }
        public Autor autor { get; set; }
        public int NroPaginas { get; set; }
        public Editorial editorial { get; set; }
        public string Estado { get; set; }
    }
}
