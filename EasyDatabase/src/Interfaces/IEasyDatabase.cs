using EasyDatabase.Models;

namespace EasyDatabase.Interfaces {

	public interface IEasyDatabase {
		public Task<CMySQLResult> Query(string query, params object[] parameters);
	}

}
