using LiteDB;

namespace EasyShop.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Category { get; set; }

    // Atributos dinâmicos: cada produto pode ter chaves diferentes
    public Dictionary<string, string> Attributes { get; set; } = new();

    [BsonIgnore] // só para exibir na tela, não é salvo no banco
    public string AttributesText => Attributes.Count == 0
        ? "Sem atributos"
        : string.Join(" | ", Attributes.Select(a => $"{a.Key}: {a.Value}"));
}