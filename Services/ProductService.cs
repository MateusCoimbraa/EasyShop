using System.Globalization;
using EasyShop.Data;
using EasyShop.Models;

namespace EasyShop.Services;

public class ProductService
{
    private readonly ProductRepository _repository;

    public ProductService(ProductRepository repository) => _repository = repository;

    public List<Product> GetAll() => _repository.GetAll();

    // attributesText no formato: cor=Azul; tamanho=M; garantia=12 meses
    public void Add(string name, string priceText, string? category, string? attributesText)
    {
        name = name?.Trim() ?? "";
        if (name.Length == 0)
            throw new InvalidOperationException("Informe o nome do produto.");

        if (!decimal.TryParse(priceText, NumberStyles.Number, CultureInfo.CurrentCulture, out var price) || price <= 0)
            throw new InvalidOperationException("Informe um preço válido maior que zero.");

        _repository.Insert(new Product
        {
            Name = name,
            Price = price,
            Category = string.IsNullOrWhiteSpace(category) ? null : category.Trim(),
            Attributes = ParseAttributes(attributesText)
        });
    }

    private static Dictionary<string, string> ParseAttributes(string? text)
    {
        var result = new Dictionary<string, string>();
        if (string.IsNullOrWhiteSpace(text)) return result;

        foreach (var pair in text.Split(';', StringSplitOptions.RemoveEmptyEntries))
        {
            var parts = pair.Split('=', 2);
            if (parts.Length == 2 && parts[0].Trim().Length > 0)
                result[parts[0].Trim()] = parts[1].Trim();
        }
        return result;
    }
}