using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyShop.Models;
using EasyShop.Services;

namespace EasyShop.ViewModels;

public partial class CustomersViewModel : ObservableObject
{
    private readonly CustomerService _service;

    [ObservableProperty] private string name = "";
    [ObservableProperty] private string email = "";
    [ObservableProperty] private string message = "";

    public ObservableCollection<Customer> Customers { get; } = new();

    public CustomersViewModel(CustomerService service) => _service = service;

    [RelayCommand]
    private async Task LoadAsync()
    {
        Customers.Clear();
        foreach (var c in await _service.GetAllAsync())
            Customers.Add(c);
    }

    [RelayCommand]
    private async Task AddAsync()
    {
        try
        {
            await _service.AddAsync(Name, Email);
            Name = "";
            Email = "";
            Message = "Cliente salvo com sucesso!";
            await LoadAsync();
        }
        catch (InvalidOperationException ex)
        {
            Message = ex.Message;
        }
    }
}