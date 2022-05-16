namespace EasyDatabase.Models {

	public class CMySQLRow {
		private readonly Dictionary<string, string> _row = new();

		public CMySQLRow() { }

		public string this[string strKey] {
			get => _row[strKey];
			set => _row[strKey] = value;
		}
	}

}
