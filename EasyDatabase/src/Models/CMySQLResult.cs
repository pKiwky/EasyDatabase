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
	}

}
