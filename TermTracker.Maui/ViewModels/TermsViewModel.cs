using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TermTracker.CoreBusiness.Models;
using TermTracker.Maui.Views;
using TermTracker.UseCases.Interfaces;
//using static TermTracker.Maui.ViewModels.TermViewModel;

namespace TermTracker.Maui.ViewModels;
public partial class TermsViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Term> terms;

    private readonly IViewTermsUseCase viewTermUseCase;
    private readonly IDeleteTermUseCase deleteTermUseCase;

    public TermsViewModel(IViewTermsUseCase viewTermUseCase, IDeleteTermUseCase deleteTermUseCase)
    {
        this.viewTermUseCase = viewTermUseCase;
        this.deleteTermUseCase = deleteTermUseCase;
        this.Terms = new ObservableCollection<Term>();
    }

    public async Task LoadTermsAsync()
    {
        this.Terms.Clear();

        var Terms = await viewTermUseCase.ExecuteAsync();

        if (Terms != null && Terms.Count > 0)
        {
            foreach (var term in Terms)
            {
                this.Terms.Add(term);
            }
        }
    }

    [RelayCommand]
    public async Task DeleteTerm(int termId)
    {
        await deleteTermUseCase.ExecuteAsync(termId);
        await LoadTermsAsync();
    }

    [RelayCommand]
    public async Task GotoEditTerm(int termId)
    {
        await Shell.Current.GoToAsync($"{nameof(EditTermPage)}?Id={termId}");
        Shell.Current.FlyoutIsPresented = false;
    }

    [RelayCommand]
    public async Task GotoAddTerm()
    {
        await Shell.Current.GoToAsync(nameof(AddTermPage));
        Shell.Current.FlyoutIsPresented = false;
    }

    [RelayCommand]
    public async Task GotoTermCourses(Term term)
    {
        // ?Term={term} only works when using primative types, not objects, so we have to pass the term to the page using a dictionary
        var queryParams = new Dictionary<string, object>
        {
            { "Term", term }
        };
        await Shell.Current.GoToAsync(nameof(TermCoursesPage), queryParams);
        Shell.Current.FlyoutIsPresented = false;
    }
}
