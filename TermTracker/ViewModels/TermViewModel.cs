using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TermTracker.CoreBusiness;
using TermTracker.Plugins.DataStore.InMemory;
using TermTracker.UseCases;
using TermTracker.UseCases.Interfaces;
using TermTracker.UseCases.PluginInterfaces;

namespace TermTracker.Maui.ViewModels;
public partial class TermViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Term> terms;

    private readonly IViewTermsUseCase viewTermUseCase;

    public TermViewModel(IViewTermsUseCase viewTermUseCase)
    {
        this.Terms = new ObservableCollection<Term>();

        this.viewTermUseCase = viewTermUseCase;
    }

    public async Task LoadTerms()
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
    public void SaveTerms() 
    { 
    
    }

    [RelayCommand]
    public void DeleteTerms()
    {

    }
}
