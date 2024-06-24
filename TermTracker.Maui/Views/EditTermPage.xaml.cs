using TermTracker.Maui.ViewModels;

namespace TermTracker.Maui.Views;

[QueryProperty(nameof(TermId), "Id")]
public partial class EditTermPage : ContentPage
{
    private readonly TermViewModel termViewModel;

    public EditTermPage(TermViewModel termViewModel)
    {
        InitializeComponent();
        this.termViewModel = termViewModel;

        this.BindingContext = this.termViewModel;
    }

    public string TermId
    {
        set
        {
            if (!string.IsNullOrWhiteSpace(value) && int.TryParse(value, out int termId))
            {
                LoadTerm(termId);
            }
        }
    }

    private async void LoadTerm(int termId)
    {
        await this.termViewModel.LoadTerm(termId);
    }
}