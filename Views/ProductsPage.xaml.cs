using EasyShop.ViewModels;

namespace EasyShop.Views;

public partial class ProductsPage : ContentPage
{
    private readonly ProductsViewModel _vm;

    public ProductsPage(ProductsViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.LoadCommand.Execute(null);
    }
}