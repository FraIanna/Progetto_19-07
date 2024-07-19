using DataLayer.Data;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DataLayer.SqlServer
{
    public class RegistryDao : IRegistryDao
    {

        private readonly string _connectionString;

        public RegistryDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbString");
        }

        private const string INSERT_REGISTRY = @"INSERT INTO ANAGRAFICA (Cognome, Nome, Indirizzo, Città, Cap, CodFS) 
        OUTOPUT INSERTED.Id
        VALUES (@cognome, @nome, @indirizzo, @citta, @cap, @codfs )";

        public RegistryEntity Create(RegistryEntity registry)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                conn.Open();
                using var cmd = new SqlCommand(INSERT_REGISTRY);
                cmd.Parameters.AddWithValue("@cognome", registry.Name);
                cmd.Parameters.AddWithValue("@nome", registry.Surname);
                cmd.Parameters.AddWithValue("@indirizzo", registry.Address);
                cmd.Parameters.AddWithValue("@citta", registry.City);
                cmd.Parameters.AddWithValue("@cap", registry.Cap);
                cmd.Parameters.AddWithValue("@codfs", registry.CodiceFiscale);

                registry.Id = (int)cmd.ExecuteScalar();
                return registry;
            }
            catch (Exception ex) 
            {
            return null;
            }
        }

        public RegistryEntity Delete(int registryId)
        {
            throw new NotImplementedException();
        }

        public RegistryEntity GetRegistry(int registryId)
        {
            throw new NotImplementedException();
        }

        public RegistryEntity Update(int registryId, RegistryEntity registry)
        {
            throw new NotImplementedException();
        }
    }
}
