using DataLayer.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.SqlServer
{
    public class ViolationDao : IViolationDao
    {

        private readonly string _connectionString;

        public ViolationDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbString");
        }

        private const string SELECT_ALL_NOT_CONTESTED_VIOLATIONS = 
            @"SELECT v.IdVerbale, v.IdAnagrafica, v.IdViolazione, v.DataViolazione, v.IndirizzoViolazione, 
                   v.NominativoAgente, v.DataTrascrizioneVerbale, v.DecurtamentoPunti, v.MultaContestata,
                   a.Cognome, a.Nome, a.Indirizzo, a.Città, a.CAP, a.CodFS,
                   tv.Descrizione, tv.Importo
            FROM VERBALE v
            JOIN ANAGRAFICA a ON v.IdAnagrafica = a.Id
            JOIN [TIPO VIOLAZIONE] tv ON v.IdViolazione = tv.IdViolazione
            WHERE v.MultaContestata = 0";

        private const string SELECT_VIOLATIONS_OVER_400 =
           @"SELECT 
                v.IdVerbale,
                v.IdAnagrafica,
                v.IdViolazione,
                v.DataViolazione,
                v.IndirizzoViolazione,
                v.NominativoAgente,
                v.DataTrascrizioneVerbale,
                v.DecurtamentoPunti,
                v.MultaContestata,
                a.Cognome,
                a.Nome,
                a.Indirizzo,
                a.Città,
                a.CAP,
                a.CodFS,
                tv.Descrizione,
                tv.Importo
              FROM VERBALE v
              JOIN ANAGRAFICA a ON v.IdAnagrafica = a.Id
              JOIN [TIPO VIOLAZIONE] tv ON v.IdViolazione = tv.IdViolazione
              WHERE tv.Importo > 400";

        public ViolationEntity Create(ViolationEntity violation)
        {
            throw new NotImplementedException();
        }

        public ViolationEntity Delete(int ViolationId)
        {
            throw new NotImplementedException();
        }

        public ViolationEntity GetViolation(int ViolationId)
        {
            throw new NotImplementedException();
        }

        public ViolationEntity Update(int ViolationId, ViolationEntity violation)
        {
            throw new NotImplementedException();
        }

        public List<ViolationEntity> GetAllNotContestedViolation()
        {
            var violations = new List<ViolationEntity>();

            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(SELECT_ALL_NOT_CONTESTED_VIOLATIONS, conn);
                conn.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var violation = new ViolationEntity
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("IdVerbale")),
                        RegistryId = reader.GetInt32(reader.GetOrdinal("IdAnagrafica")),
                        ViolationId = reader.GetInt32(reader.GetOrdinal("IdViolazione")),
                        ViolationData = reader.GetDateTime(reader.GetOrdinal("DataViolazione")),
                        ViolationAddress = reader.GetString(reader.GetOrdinal("IndirizzoViolazione")),
                        OfficerName = reader.GetString(reader.GetOrdinal("NominativoAgente")),
                        VerbalTranscription = reader.GetDateTime(reader.GetOrdinal("DataTrascrizioneVerbale")),
                        LicencePoints = reader.GetInt32(reader.GetOrdinal("DecurtamentoPunti")),
                        ContestedTicket = reader.GetBoolean(reader.GetOrdinal("MultaContestata")),
                        Registry = new RegistryEntity
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("IdAnagrafica")),
                            Surname = reader.GetString(reader.GetOrdinal("Cognome")),
                            Name = reader.GetString(reader.GetOrdinal("Nome")),
                            Address = reader.GetString(reader.GetOrdinal("Indirizzo")),
                            City = reader.GetString(reader.GetOrdinal("Città")),
                            Cap = reader.GetString(reader.GetOrdinal("CAP")),
                            CodiceFiscale = reader.GetString(reader.GetOrdinal("CodFS"))
                        },
                        Violation = new ViolationEntity
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("IdViolazione")),
                            Description = reader.GetString(reader.GetOrdinal("Descrizione")),
                            Amount = reader.GetDecimal(reader.GetOrdinal("Importo"))
                        }
                    };
                    violations.Add(violation);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'accesso al database", ex);
            }

            return violations;
        }

        public async Task<List<ViolationEntity>> GetViolationsOver400Async()
        {
            var results = new List<ViolationEntity>();

            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(SELECT_VIOLATIONS_OVER_400, conn);
                conn.Open();
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var violation = new ViolationEntity
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("IdVerbale")),
                        RegistryId = reader.GetInt32(reader.GetOrdinal("IdAnagrafica")),
                        ViolationId = reader.GetInt32(reader.GetOrdinal("IdViolazione")),
                        ViolationData = reader.GetDateTime(reader.GetOrdinal("DataViolazione")),
                        ViolationAddress = reader.GetString(reader.GetOrdinal("IndirizzoViolazione")),
                        OfficerName = reader.GetString(reader.GetOrdinal("NominativoAgente")),
                        VerbalTranscription = reader.GetDateTime(reader.GetOrdinal("DataTrascrizioneVerbale")),
                        LicencePoints = reader.GetInt32(reader.GetOrdinal("DecurtamentoPunti")),
                        ContestedTicket = reader.GetBoolean(reader.GetOrdinal("MultaContestata")),
                        Registry = new RegistryEntity
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("IdAnagrafica")),
                            Surname = reader.GetString(reader.GetOrdinal("Cognome")),
                            Name = reader.GetString(reader.GetOrdinal("Nome")),
                            Address = reader.GetString(reader.GetOrdinal("Indirizzo")),
                            City = reader.GetString(reader.GetOrdinal("Città")),
                            Cap = reader.GetString(reader.GetOrdinal("CAP")),
                            CodiceFiscale = reader.GetString(reader.GetOrdinal("CodFS"))
                        },
                        Violation = new ViolationEntity
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("IdViolazione")),
                            Description = reader.GetString(reader.GetOrdinal("Descrizione")),
                            Amount = reader.GetDecimal(reader.GetOrdinal("Importo"))
                        }
                    };

                    results.Add(violation);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'accesso al database", ex);
            }

            return results;
        }

    }
}
