using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyShop.Models;
using EasyShop.Services;

namespace EasyShop.ViewModels;

public partial class ProductsViewModel : ObservableObject
{
    private readonly ProductService _service;

    [ObservableProperty] private string name = "";
    [ObservableProperty] private string priceText = "";
    [ObservableProperty] private string category = "";
    [ObservableProperty] private string attributesText = "";
    [ObservableProperty] private string message = "";

    public ObservableCollection<Product> Products { get; } = new();

    public ProductsViewModel(ProductService service) => _service = service;

    [RelayCommand]
    private void Load()
    {
        Products.Clear();
        foreach (var p in _service.GetAll())
            Products.Add(p);
    }

    [RelayCommand]
    private void Add()
    {
        try
        {
            _service.Add(Name, PriceText, Category, AttributesText);
            Name = PriceText = Category = AttributesText = "";
            Message = "Produto salvo com sucesso!";
            Load();
        }
        catch (InvalidOperationException ex)
        {
            Message = ex.Message;
        }
    }
}