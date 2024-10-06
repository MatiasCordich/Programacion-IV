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
    // ----- Interfaz del Estado Container para la inyección de dependencias ----- //
    public interface IEstadoContainerRepository
    {
        // Métodos abstractos de la interfaz //

        // ---- Obtener un Estado de Container ---- //
        EstadoContainer GetEstadoContainer(int IdEstadoContainer);

        // ---- Obtener todos los Estados posibles de Container ---- //
        IEnumerable<EstadoContainer> GetEstadosContainer(bool? SoloActivos = true);
    }
    public class EstadoContainerRepository : IEstadoContainerRepository
    {
        // ---- Propiedad: String de conexion ---- //
        private string _connectionString;

        // ---- Constructor ---- //
        public EstadoContainerRepository(string connectionString)
        {
            _connectionString = connectionString;

        }

        /// <summary>
        /// Consulta a la DDBB por medio de un ID
        /// </summary>
        /// <param name="IdContainer">ID del estado de Container</param>
        /// <returns>Devuelve un Estado por su ID</returns>
        public EstadoContainer GetEstadoContainer(int IdEstadoContainer)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM EstadosContainer WHERE IdEstadoContainer = " + IdEstadoContainer.ToString();

                EstadoContainer result = conn.QuerySingle<EstadoContainer>(query);

                return result;

            }
        }

        /// <summary>
        /// Consulta a la base de datos por la lista de Estados de Containers
        /// </summary>
        /// <param name="SoloActivos">True: Solo trae los activos, False: Trae todos los registros</param>
        /// <returns>Devuelve solo los estados que sean ACTIVOS</returns>
        public IEnumerable<EstadoContainer> GetEstadosContainer(bool? SoloActivos = true)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {


                string query = "SELECT * FROM EstadosContainer ";

                if (SoloActivos == true) query += " WHERE FechaBaja is null";

                IEnumerable<EstadoContainer> results = conn.Query<EstadoContainer>(query);

                return results;

            }
        }
    }
}
