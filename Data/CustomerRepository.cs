using EasyShop.Models;
using SQLite;

namespace EasyShop.Data;

public class CustomerRepository
{
    private readonly SQLiteAsyncConnection _db;
    private bool _initialized;

    public CustomerRepository()
    {
        var path = Path.Combine(FileSystem.AppDataDirectory, "customers.db3");
        _db = new SQLiteAsyncConnection(path);
    }

    private async Task InitAsync()
    {
        if (_initialized) return;
        await _db.CreateTableAsync<Customer>();
        _initialized = true;
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        await InitAsync();
        return await _db.Table<Customer>().OrderBy(c => c.Name).ToListAsync();
    }

    public async Task InsertAsync(Customer customer)
    {
        await InitAsync();
        await _db.InsertAsync(customer);
    }

    public async Task DeleteAsync(int id)
    {
        await InitAsync();
        await _db.DeleteAsync<Customer>(id);
    }
}