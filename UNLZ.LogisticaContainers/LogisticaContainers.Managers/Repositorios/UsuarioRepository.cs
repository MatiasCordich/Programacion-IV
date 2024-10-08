using LogisticaContainers.Managers.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace LogisticaContainers.Managers.Repositorios
{
    // ----- Interfaz del Repositorio Usuario para la inyección de dependencias ----- //
    public interface IUsuarioRepository
    {
        int CrearUsuario(Usuario usuario);
        Usuario? GetUsuarioPorGoogleSubject(string googleSubject);
        Usuario? GetUsuarioPorId(int IdUsuario);
        IEnumerable<Usuario> GetUsuarios();

    }

    // ----- La clase UsuarioRepository ----- //
    public class UsuarioRepository : IUsuarioRepository
    {
        // ---- Propiedad: String de conexion ---- //
        private string _connectionString;

        // ---- Constructor ---- //
        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // ---- Implementación de los métodos de la interfaz ---- //

        /// <summary>
        /// Devuelve una lista de Usuarios
        /// </summary>
        /// <param></param>
        /// <returns>Devuelve una lista de Usuarios</returns>
        public IEnumerable<Usuario> GetUsuarios()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                List<Usuario> usuarios = db.Query<Usuario>("SELECT * FROM Usuario").ToList();

                return usuarios;
            }

        }

        /// <summary>
        /// Consulta a la DDBB por un Usuario por su ID
        /// </summary>
        /// <param name="IdUsuario"></param>
        /// <returns>Puede devolver un Usuario como un nulo</returns>
        public Usuario? GetUsuarioPorId(int IdUsuario)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                Usuario usuario = db.Query<Usuario>("SELECT * FROM Usuario WHERE IdUsuario = " + IdUsuario.ToString()).FirstOrDefault();

                return usuario;
            }
        }

        /// <summary>
        /// Consulta a la DDBB por un Usuario por su Id de Google
        /// </summary>
        /// <param name="googleSubject"></param>
        /// <returns>Puede devolver un Usuario o nulo</returns>
        public Usuario? GetUsuarioPorGoogleSubject(string googleSubject)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                Usuario usuarios = db.Query<Usuario>("SELECT * FROM Usuario WHERE GoogleIdentificador = '" + googleSubject.ToString() + "'").FirstOrDefault();

                return usuarios;
            }
        }

        /// <summary>
        /// Crea un nuevo Usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>Devuelve un el ID del usuario creado</returns>
        public int CrearUsuario(Usuario usuario)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO Usuario (GoogleIdentificador, NombreCompleto, Nombre, Apellido, Email, Borrado, IdUsuarioAlta, FechaAlta, IdUsuarioModificacion, FechaModificacion, IdUsuarioBaja, FechaBaja)  
                         VALUES (@GoogleIdentificador, @NombreCompleto, @Nombre, @Apellido, @Email, @Borrado, @IdUsuarioAlta, @FechaAlta, @IdUsuarioModificacion, @FechaModificacion, @IdUsuarioBaja, @FechaBaja);                    
                         SELECT CAST(SCOPE_IDENTITY() AS INT)";

                usuario.IdUsuario = db.QuerySingle<int>(query, usuario);

                return usuario.IdUsuario;
            }
        }


    }
}
