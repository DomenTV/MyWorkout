using Test2.ViewModels;

namespace Test2;

public partial class FAQ : ContentPage
{
    public FAQ()
    {
        InitializeComponent();
        BindingContext = new FAQViewModel(Navigation);
    }
}
