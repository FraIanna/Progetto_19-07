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
            _connectionString = configuration.GetConnectionString("DbString")!;
        }

        private const string INSERT_REGISTRY = @"INSERT INTO ANAGRAFICA (Cognome, Nome, Indirizzo, Città, Cap, CodFS) 
        OUTPUT INSERTED.Id
        VALUES (@cognome, @nome, @indirizzo, @citta, @cap, @codfs )";

        public RegistryEntity Create(RegistryEntity registry)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                conn.Open();
                using var cmd = new SqlCommand(INSERT_REGISTRY, conn);
                cmd.Parameters.AddWithValue("@cognome", registry.Name);
                cmd.Parameters.AddWithValue("@nome", registry.Surname);
                cmd.Parameters.AddWithValue("@indirizzo", registry.Address);
                cmd.Parameters.AddWithValue("@citta", registry.City);
                cmd.Parameters.AddWithValue("@cap", registry.Cap);
                cmd.Parameters.AddWithValue("@codfs", registry.CodiceFiscale);

                int id = (int)cmd.ExecuteScalar();
                return new RegistryEntity { Id = id };
            }
            catch (Exception ex) 
            {
            throw new Exception("Errore nella registrazione", ex);
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
