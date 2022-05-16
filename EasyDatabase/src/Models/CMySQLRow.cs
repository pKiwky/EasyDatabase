namespace EasyDatabase.Models {

	public class CMySQLRow {
		private readonly Dictionary<string, string> _row = new();

		public CMySQLRow() { }

		public T GetValue<T>(string strKey) {
			return (T)Convert.ChangeType(_row[strKey], typeof(T));
		}

		public Dictionary<string, string> GetFields() {
			return _row;
		}

		public string this[string strKey] {
			get => _row[strKey];
			set => _row[strKey] = value;
		}
	}

}
