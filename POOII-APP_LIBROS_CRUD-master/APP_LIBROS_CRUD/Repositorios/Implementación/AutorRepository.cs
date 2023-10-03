using APP_LIBROS_CRUD.Models;
using APP_LIBROS_CRUD.Repositorios.Contrato;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace APP_LIBROS_CRUD.Repositorios.Implementación
{
    public class AutorRepository: IGenericRepository<Autor>
    {
        private readonly string _cadenaSQL = "";

        public AutorRepository(IConfiguration configuracion)
        {
            _cadenaSQL = configuracion.GetConnectionString("cadenaSQL");
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Autor>> FindAll()
        {
            List<Autor> _lista = new List<Autor>();
            using(var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_listaAutores", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using(var dr = await cmd.ExecuteReaderAsync())
                {
                    while(await dr.ReadAsync())
                    {
                        _lista.Add(new Autor
                        {
                            IDAutor = Convert.ToInt32(dr["IDAutor"]),
                            Nombre_Autor = dr["Nombre_Autor"].ToString()
                        });
                    }
                }
            }
            return _lista;
        }

        public Task<bool> Save(Autor entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Autor entity)
        {
            throw new NotImplementedException();
        }
    }
}
