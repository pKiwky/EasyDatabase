# EasyDatabase

```c#
using EasyDatabase;

var db = new EasyDatabaseMySQL("server=localhost;database=dashboard;UID=root;password=");

var query = await db.Query("SELECT * FROM web_shop");

foreach(var row in query.GetRows()) {
	Console.WriteLine(row["description"]);
}
```
