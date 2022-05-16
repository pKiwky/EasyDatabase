# EasyDatabase

```c#
using EasyDatabase;

public class WebShopProduct {
	public string Title { get; set; }
	public string Description { get; set; }
	public int Discount { get; set; }

	public WebShopProduct() { }

	public WebShopProduct(string title, string description, int discount) {
		Title = title;
		Description = description;
		Discount = discount;
	}

	public async Task<ulong> Insert() {
		var db = new EasyDatabaseMySQL("server=localhost;database=dashboard;UID=root;password=");

		var query = await db.Query(
			"INSERT INTO web_shop (title, description, discount) VALUES ('{0}', '{1}', {2})",
			Title, Description, Discount
		);

		return query.LastInsertedId();
	}

	public async Task<bool> Select(int id) {
		var db = new EasyDatabaseMySQL("server=localhost;database=dashboard;UID=root;password=");

		var query = await db.Query("SELECT * FROM web_shop WHERE id = {0}", id);
		if (query == null) {
			return false;
		}

		var row = query.GetRow(0);
		Title = row.GetValue<string>("title");
		Description = row.GetValue<string>("description");
		Discount = row.GetValue<int>("discount");

		return true;
	}
}

class Program {
	public static async void AsyncProduct() {
		WebShopProduct p = new WebShopProduct();
		await p.Select(1);

		p.Title = "New title";
		await p.Insert();
	}

	public static void Main(string[] args) {
		AsyncProduct();
	}
}
```
