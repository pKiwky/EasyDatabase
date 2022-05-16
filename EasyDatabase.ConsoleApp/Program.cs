// See https://aka.ms/new-console-template for more information

using EasyDatabase;

var db = new EasyDatabaseMySQL("server=localhost;database=dashboard;UID=root;password=");

var query = await db.Query("SELECT * FROM web_shop");
if (query == null) {
	return;
}
