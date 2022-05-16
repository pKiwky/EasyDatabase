using System.Data.Common;

namespace EasyDatabase.Models {

	public class CMySQLResult {
		private List<CMySQLRow> _rows = new();

		private int _rowsAffected;
		private ulong _insertId;

		public CMySQLResult(int rowsAffected) {
			_rowsAffected = rowsAffected;
		}

		public CMySQLResult(DbDataReader reader, ulong insertId) {
			while (reader.Read()) {
				var row = new CMySQLRow();

				for (int i = 0; i < reader.FieldCount; i++) {
					string value = "";
					if (reader.IsDBNull(i) == false) {
						value = reader.GetString(i);
					}

					string key = reader.GetName(i);
					row[key] = value;
				}

				_rows.Add(row);
			}

			reader.Close();
			_ = reader.DisposeAsync();

			_insertId = insertId;
			_rowsAffected = 0;
		}

		/// <summary>
		/// Get total affected rows by this query.
		/// </summary>
		public int RowsAffected() => _rowsAffected;

		/// <summary>
		/// Get inserted id by this query.
		/// </summary>
		public ulong LastInsertedId() => _insertId;

		/// <summary>
		/// Get row by id drom rows list.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public CMySQLRow GetRow(int id) => _rows[id];

		/// <summary>
		/// Get a list with all rows fetched.
		/// </summary>
		public IReadOnlyList<CMySQLRow> GetAllRows() => _rows;
	}

}
