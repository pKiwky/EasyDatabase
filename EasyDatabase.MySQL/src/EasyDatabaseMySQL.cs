using EasyDatabase.Interfaces;
using EasyDatabase.Models;

namespace EasyDatabase.MySQL {

	public class EasyDatabaseMySQL : IEasyDatabase {
		public async Task<CMySQLResult> Query(string query, params object[] parameters) {
			CMySQLResult result = null;

			return result;
		}
	}

}
