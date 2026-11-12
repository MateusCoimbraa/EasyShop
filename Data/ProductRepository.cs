using EasyShop.Models;
using LiteDB;

namespace EasyShop.Data;

public class ProductRepository
{
    private readonly ILiteCollection<Product> _collection;

    public ProductRepository()
    {
        var path = Path.Combine(FileSystem.AppDataDirectory, "products.db");
        var db = new LiteDatabase($"Filename={path};Connection=shared");
        _collection = db.GetCollection<Product>("products");
        _collection.EnsureIndex(p => p.Name);
    }

    public List<Product> GetAll() =>
        _collection.FindAll().OrderBy(p => p.Name).ToList();

    public void Insert(Product product) => _collection.Insert(product);

    public void Delete(int id) => _collection.Delete(id);
}