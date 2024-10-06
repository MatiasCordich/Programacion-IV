using Dapper;
using LogisticaContainers.Managers.Entidades;
using LogisticaContainers.Managers.ModelFactories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaContainers.Managers.Repositorios
{
    // ----- Interfaz del Repositorio Container para la inyección de dependencias ----- //
    public interface IContainerRepository
    {
        // Métodos CRUD abstractos de la interfaz //

        // ---- Obtener Container ---- //
        Container GetContainer(int IdContainer);

        // ---- Obtener los Containers que estén activos ---- //
        IEnumerable<Container> GetContainers(bool? SoloActivos = true);

        // ---- Obtener todos los Containers ---- //
        IEnumerable<ContainerCompleto> GetContainersCompleto();

        // ---- Crear un Container ---- //
        int CrearContainer(Container container);

        // ---- Modificar un Container ---- //
        bool ModificarContainer(int IdContainer, Container container);

        // ---- Eliminar un Container ---- //
        bool EliminarContainer(int IdContainer, int IdUsuarioBaja);
    }

    // ----- La clase ContainerRepository ----- //
    public class ContainerRepository : IContainerRepository
    {
        // ---- Propiedad: String de conexion ---- //
        private string _connectionString;

        // ---- Constructor ---- //
        public ContainerRepository(string conn)
        {
            // Una vez instanciado le pasamos el string de conexion
            _connectionString = conn;
        }

        // ---- Implementación de los métodos de la interfaz ---- //

        /// <summary>
        /// Consulta a la DDBB por el Id
        /// </summary>
        /// <param name="IdContainer"></param>
        /// <returns>Devuelve un Container</returns>
        public Container GetContainer(int IdContainer)
        {
            // ---- Using: Para abrir y cerrar la conexion una vez hecho la consulta ---- //
            using (IDbConnection conn = new SqlConnection(_connectionString)) {

                string query = "SELECT * FROM Container WHERE IdContainer = " + IdContainer.ToString();

                Container result = conn.QuerySingle<Container>(query) ;

                return result ;
            }
            
        }

        /// <summary>
        /// Consulta a la DDBB por la lista de Containers
        /// </summary>
        /// <param name="SoloActivos">True: Solo trae los activos, False: Trae todos los registros</param>
        /// <returns>Devuelve una lista de containers activos</returns>
        public IEnumerable<Container> GetContainers(bool? SoloActivos = true)
        {
            // ---- Using: Para abrir y cerrar la conexion una vez hecho la consulta ---- //
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {

                string query = "SELECT * FROM Container ";

                if (SoloActivos == true) query += " WHERE FechaBaja is null";

                IEnumerable<Container> results = conn.Query<Container>(query);

                return results;
            }
        }

        /// <summary>
        /// Obtiene una lista completa de los containers
        /// </summary>
        /// <returns>Lista completa de containers</returns>
        public IEnumerable<ContainerCompleto> GetContainersCompleto()
        {
            // ---- Using: Para abrir y cerrar la conexion una vez hecho la consulta ---- //
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"SELECT Container.*, EstadosContainer.Descripcion Estado
                                 FROM container    
                                 LEFT JOIN EstadosContainer ON Container.IdEstadoContainer = EstadosContainer.IdEstadoContainer
                                 WHERE container.fechabaja is null";

                IEnumerable<ContainerCompleto> results = conn.Query<ContainerCompleto>(query);

                return results;
            }
        }

        /// <summary>
        /// Crear un nuevo Container en la base de datos
        /// </summary>
        /// <param name="container"></param>
        /// <returns>Devuelve el ID del container creado</returns>
        public int CrearContainer(Container container)
        {
            // ---- Using: Para abrir y cerrar la conexion una vez hecho la consulta ---- //
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {

                string query = @"INSERT INTO Container (DescripcionContainer, IdEstadoContainer, IdUsuarioAlta, FechaAlta, IdUsuarioModificacion, FechaModificacion, IdUsuarioBaja, FechaBaja)  
                                VALUES ( @DescripcionContainer, @IdEstadoContainer, @IdUsuarioAlta, @FechaAlta, @IdUsuarioModificacion, @FechaModificacion, @IdUsuarioBaja, @FechaBaja);                    
                                SELECT CAST(SCOPE_IDENTITY() AS INT) ";


                container.IdContainer = conn.QuerySingle<int>(query, container);


                return container.IdContainer;
            }
        }

        /// <summary>
        /// Modificar Container en la base de Datos
        /// </summary>
        /// <param name="IdContainer">Id del Container a modificar</param>
        /// <param name="container"></param>
        /// <param name="IdUsuarioModificacion"></param>
        /// <returns></returns>
        public bool ModificarContainer(int IdContainer, Container container)
        {
            // ---- Using: Para abrir y cerrar la conexion una vez hecho la consulta ---- //
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {

                string query = @"UPDATE 
                                    Container 
                                SET 
                                    DescripcionContainer = @DescripcionContainer, 
                                    IdEstadoContainer = @IdEstadoContainer, 
                                    FechaModificacion = @FechaModificacion, 
                                    IdUsuarioModificacion  = @IdUsuarioModificacion    
                                    WHERE IdContainer = " + IdContainer.ToString();

                // conn.execute devuelve un entero que representa la cantidad de filas afectadas. 
                //Se espera que se haya modificado solo un registro, por eso se lo compara con un 1.
                return conn.Execute(query, container) == 1;
            }
        }

        /// <summary>
        /// Eliminar de manera lógica un container de la base de datos
        /// </summary>
        /// <param name="IdContainer">Id del container que sera dado de baja</param>
        /// <param name="IdUsuarioBaja">Id del usuario que dio de baja el container</param>
        /// <returns></returns>
        public bool EliminarContainer(int IdContainer, int IdUsuarioBaja)
        {
            // ---- Using: Para abrir y cerrar la conexion una vez hecho la consulta ---- //
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {

                string query = @"UPDATE 
                                    Container 
                                SET 
                                    
                                    FechaBaja = '" + DateTime.Now.ToString("yyyyMMdd") + "'," +
                                    " IdUsuarioBaja  = " + IdUsuarioBaja +
                                "WHERE IdContainer = " + IdContainer.ToString();


                //conn.execute devuelve un entero que representa la cantidad de filas afectadas. 
                //Se espera que se haya modificado solo un registro, por eso se lo compara con un 1.
                return conn.Execute(query) == 1;
            }
        }

       
    }
}
