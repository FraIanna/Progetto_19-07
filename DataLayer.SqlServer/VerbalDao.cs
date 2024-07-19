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
    public class VerbalDao : IVerbalDao
    {
        private readonly string _connectionString;

        public VerbalDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbString")!;
        }

        private const string INSERT_VERBAL = 
            @"INSERT INTO VERBALE (IdAnagrafica, IdViolazione, DataViolazione, IndirizzoViolazione, 
                                 NominativoAgente, DataTrascrizioneVerbale, DecurtamentoPunti, MultaContestata)
            OUTPUT INSERTED.IdVerbale
            VALUES (@IdAnagrafica, @IdViolazione, @DataViolazione, @IndirizzoViolazione, 
                    @NominativoAgente, @DataTrascrizioneVerbale, @DecurtamentoPunti, @MultaContestata)";

        private const string SELECT_TOTAL_VERBALS_BY_TRASGRESSOR =
           @"SELECT 
                a.Id AS RegistryId,
                a.Nome + ' ' + a.Cognome AS TrasgressoreName,
                COUNT(v.IdVerbale) AS TotalVerbalCount
              FROM VERBALE v
              JOIN ANAGRAFICA a ON v.IdAnagrafica = a.Id
              GROUP BY a.Id, a.Nome, a.Cognome";

        private const string SELECT_TOTAL_POINTS_BY_TRASGRESSOR =
           @"SELECT 
                a.Id AS RegistryId,
                a.Nome + ' ' + a.Cognome AS TrasgressoreName,
                SUM(v.DecurtamentoPunti) AS TotalPoints
              FROM VERBALE v
              JOIN ANAGRAFICA a ON v.IdAnagrafica = a.Id
              GROUP BY a.Id, a.Nome, a.Cognome";

        private const string SELECT_VIOLATIONS_OVER_10_POINTS =
           @"SELECT 
                a.Cognome,
                a.Nome,
                v.DataViolazione,
                tv.Importo,
                v.DecurtamentoPunti
              FROM VERBALE v
              JOIN ANAGRAFICA a ON v.IdAnagrafica = a.Id
              JOIN [TIPO VIOLAZIONE] tv ON v.IdViolazione = tv.IdViolazione
              WHERE v.DecurtamentoPunti > 10";

        public VerbalEntity Create(VerbalEntity verbal)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(INSERT_VERBAL, conn);
                cmd.Parameters.AddWithValue("@IdAnagrafica", verbal.RegistryId);
                cmd.Parameters.AddWithValue("@IdViolazione", verbal.ViolationId);
                cmd.Parameters.AddWithValue("@DataViolazione", verbal.ViolationData);
                cmd.Parameters.AddWithValue("@IndirizzoViolazione", verbal.ViolationAddress);
                cmd.Parameters.AddWithValue("@NominativoAgente", verbal.OfficerName);
                cmd.Parameters.AddWithValue("@DataTrascrizioneVerbale", verbal.VerbalTranscription);
                cmd.Parameters.AddWithValue("@DecurtamentoPunti", verbal.LicencePoints);
                cmd.Parameters.AddWithValue("@MultaContestata", verbal.ContestedTicket);

                conn.Open();
                verbal.Id = (int)cmd.ExecuteScalar();
                return verbal;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento del verbale nel database", ex);
            }
        }

        public VerbalEntity DeleteVerbal(int verbalId)
        {
            throw new NotImplementedException();
        }

        public VerbalEntity GetVerbal(int verbalId)
        {
            throw new NotImplementedException();
        }

        public VerbalEntity Update(int verbalId, VerbalEntity verbal)
        {
            throw new NotImplementedException();
        }

        public async Task<List<VerbalSummary>> GetTotalVerbalByTrasgressorAsync()
        {
            var results = new List<VerbalSummary>();

            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(SELECT_TOTAL_VERBALS_BY_TRASGRESSOR, conn);
                conn.Open();
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var summary = new VerbalSummary
                    {
                        RegistryId = reader.GetInt32(reader.GetOrdinal("RegistryId")),
                        OffenderName = reader.GetString(reader.GetOrdinal("TrasgressoreName")),
                        TotalVerbalCount = reader.GetInt32(reader.GetOrdinal("TotalVerbalCount"))
                    };

                    results.Add(summary);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'accesso al database", ex);
            }

            return results;
        }

        public async Task<List<PointsSummary>> GetTotalPointsByTrasgressorAsync()
        {
            var results = new List<PointsSummary>();

            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(SELECT_TOTAL_POINTS_BY_TRASGRESSOR, conn);
                conn.Open();
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var summary = new PointsSummary
                    {
                        RegistryId = reader.GetInt32(reader.GetOrdinal("RegistryId")),
                        OffenderName = reader.GetString(reader.GetOrdinal("TrasgressoreName")),
                        TotalPoints = reader.GetInt32(reader.GetOrdinal("TotalPoints"))
                    };

                    results.Add(summary);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'accesso al database", ex);
            }

            return results;
        }

        public async Task<List<ViolationDetails>> GetViolationsOver10PointsAsync()
        {
            var results = new List<ViolationDetails>();

            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(SELECT_VIOLATIONS_OVER_10_POINTS, conn);
                conn.Open();
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var details = new ViolationDetails
                    {
                        Cognome = reader.GetString(reader.GetOrdinal("Cognome")),
                        Nome = reader.GetString(reader.GetOrdinal("Nome")),
                        DataViolazione = reader.GetDateTime(reader.GetOrdinal("DataViolazione")),
                        Importo = reader.GetDecimal(reader.GetOrdinal("Importo")),
                        DecurtamentoPunti = reader.GetInt32(reader.GetOrdinal("DecurtamentoPunti"))
                    };

                    results.Add(details);
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
