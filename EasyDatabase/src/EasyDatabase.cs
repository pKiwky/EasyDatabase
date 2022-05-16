using EasyDatabase.Interfaces;
using EasyDatabase.Models;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace EasyDatabase {

	public class EasyDatabaseMySQL : IEasyDatabase {
		private string _connectionString;

		public EasyDatabaseMySQL(string connectionString) {
			_connectionString = connectionString;
		}

		private string EscapeQuery(string query, params object[] parameters) {
			for (int i = 0; i < parameters.Length; ++i) {
				if (parameters[i].GetType() == typeof(string)) {
					parameters[i] = MySqlHelper.EscapeString((string)parameters[i]);
				} else if (parameters[i].GetType().IsEnum) {
					parameters[i] = (int)parameters[i];
				}
			}

			return string.Format(new System.Globalization.CultureInfo("en-US"), query, parameters);
		}

		public async Task<CMySQLResult> Query(string query, params object[] parameters) {
			CMySQLResult result = null;

			query = EscapeQuery(query, parameters);

			try {
				using (MySqlConnection conn = new MySqlConnection(_connectionString)) {
					await conn.OpenAsync();

					using (MySqlCommand command = new MySqlCommand(query, conn)) {
						if (query.StartsWith("DELETE") || query.StartsWith("UPDATE")) {
							int numRowsModified = await command.ExecuteNonQueryAsync();
							result = new CMySQLResult(numRowsModified);
						} else {
							using (DbDataReader reader = await command.ExecuteReaderAsync()) {
								result = new CMySQLResult(reader, (ulong)command.LastInsertedId);
							}
						}
					}
				}
			} catch (Exception ex) {
				Console.WriteLine(ex.ToString());
			}

			return result;
		}
	}

}
