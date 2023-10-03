using APP_LIBROS_CRUD.Models;
using APP_LIBROS_CRUD.Repositorios.Contrato;
using System.Data;
using System.Data.SqlClient;

namespace APP_LIBROS_CRUD.Repositorios.Implementación
{
    public class EditorialRepository: IGenericRepository<Editorial>
    {
        private readonly string _cadenaSQL = "";

        public EditorialRepository(IConfiguration configuracion)
        {
            _cadenaSQL = configuracion.GetConnectionString("cadenaSQL");
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Editorial>> FindAll()
        {
            List<Editorial> _lista = new List<Editorial>();
            using(var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_listaEditoriales", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using(var dr = await cmd.ExecuteReaderAsync())
                {
                    while(await dr.ReadAsync())
                    {
                        _lista.Add(new Editorial
                        {
                            IDEditorial = Convert.ToInt32(dr["IDEditorial"]),
                            NombreEditorial = dr["Nombre_Editorial"].ToString()
                        });
                    }
                }
            }
            return _lista;
        }

        public Task<bool> Save(Editorial entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Editorial entity)
        {
            throw new NotImplementedException();
        }
    }
}
