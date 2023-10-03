using APP_LIBROS_CRUD.Models;
using APP_LIBROS_CRUD.Repositorios.Contrato;
using System.Data;
using System.Data.SqlClient;

namespace APP_LIBROS_CRUD.Repositorios.Implementación
{
    public class LibroRepository: IGenericRepository<Libro>
    {
        private readonly string _cadenaSQL = "";
        public LibroRepository(IConfiguration configuracion)
        {
            _cadenaSQL = configuracion.GetConnectionString("cadenaSQL");
        }

        public async Task<bool> Delete(int id)
        {
            using(var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_eliminarLibro", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IDLibro", id);
                int filas_afectadas = await cmd.ExecuteNonQueryAsync();
                if (filas_afectadas > 0)
                    return true;
                else
                    return false;
            }
        }

        public async Task<List<Libro>> FindAll()
        {
            List<Libro> _lista = new List<Libro>();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_listaLibros", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using(var dr = await cmd.ExecuteReaderAsync())
                {
                    while(await dr.ReadAsync())
                    {
                        _lista.Add(new Libro
                        {
                            IDLibro = Convert.ToInt32(dr["IDLibro"]),
                            Titulo = dr["Titulo"].ToString(),
                            autor = new Autor
                            {
                                IDAutor = Convert.ToInt32(dr["IDAutor"]),
                                Nombre_Autor = dr["Nombre_Autor"].ToString()
                            },
                            NroPaginas = Convert.ToInt32(dr["NroPaginas"]),
                            editorial = new Editorial
                            {
                                IDEditorial = Convert.ToInt32(dr["IDEditorial"]),
                                NombreEditorial = dr["Nombre_Editorial"].ToString()
                            },
                            Estado = dr["Estado"].ToString()
                        });
                    }
                }
            }

            return _lista;
        }

        public async Task<bool> Save(Libro entity)
        {
            using(var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_registrarLibro", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Titulo", entity.Titulo);
                cmd.Parameters.AddWithValue("IDAutor", entity.autor.IDAutor);
                cmd.Parameters.AddWithValue("NroPaginas", entity.NroPaginas);
                cmd.Parameters.AddWithValue("IDEditorial", entity.editorial.IDEditorial);
                int filas_afectadas = await cmd.ExecuteNonQueryAsync();
                if (filas_afectadas > 0)
                    return true;
                else
                    return false;
            }
        }

        public async Task<bool> Update(Libro entity)
        {
            using(var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_actualizarLibro", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IDLibro", entity.IDLibro);
                cmd.Parameters.AddWithValue("Titulo", entity.Titulo);
                cmd.Parameters.AddWithValue("IDAutor", entity.autor.IDAutor);
                cmd.Parameters.AddWithValue("NroPaginas", entity.NroPaginas);
                cmd.Parameters.AddWithValue("IDEditorial", entity.editorial.IDEditorial);
                int filas_afectadas = await cmd.ExecuteNonQueryAsync();
                if (filas_afectadas > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
