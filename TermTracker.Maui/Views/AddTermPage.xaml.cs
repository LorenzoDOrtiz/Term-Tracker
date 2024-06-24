using TermTracker.CoreBusiness.Models;
using TermTracker.Maui.ViewModels;

namespace TermTracker.Maui.Views;

public partial class AddTermPage : ContentPage
{
    private readonly TermViewModel termViewModel;

    public AddTermPage(TermViewModel termViewModel)
    {
        InitializeComponent();
        this.termViewModel = termViewModel;

        this.BindingContext = termViewModel;


    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        this.termViewModel.Term = new Term
        {
            TermStartDate = DateTime.Now,
            TermEndDate = DateTime.Now.AddMonths(6)
        };
    }
}