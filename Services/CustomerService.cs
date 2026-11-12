using System.Net.Mail;
using EasyShop.Data;
using EasyShop.Models;
using SQLite;

namespace EasyShop.Services;

public class CustomerService
{
    private readonly CustomerRepository _repository;

    public CustomerService(CustomerRepository repository) => _repository = repository;

    public Task<List<Customer>> GetAllAsync() => _repository.GetAllAsync();

    public async Task AddAsync(string name, string email)
    {
        name = name?.Trim() ?? "";
        email = email?.Trim().ToLowerInvariant() ?? "";

        if (name.Length < 3)
            throw new InvalidOperationException("Informe o nome completo (mín. 3 caracteres).");

        if (!MailAddress.TryCreate(email, out _))
            throw new InvalidOperationException("E-mail inválido.");

        try
        {
            await _repository.InsertAsync(new Customer { Name = name, Email = email });
        }
        catch (SQLiteException)
        {
            throw new InvalidOperationException("Já existe um cliente com esse e-mail.");
        }
    }
}