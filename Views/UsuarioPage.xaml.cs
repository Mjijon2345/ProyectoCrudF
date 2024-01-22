using ProyectoCrudF.ViewModels;

namespace ProyectoCrudF.Views;


public partial class UsuarioPage : ContentPage
{
	public UsuarioPage(UsuarioViewModel viewModel)
	{
		InitializeComponent();
		BindingContext= viewModel;
	}
}