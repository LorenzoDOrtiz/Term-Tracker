using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TermTracker.CoreBusiness;
using TermTracker.UseCases.Interfaces;

namespace TermTracker.Maui.ViewModels;
public partial class TermViewModel : ObservableObject
{
    [ObservableProperty]
    private Term term;

    private readonly IViewTermUseCase viewTermUseCase;
    private readonly IEditTermUseCase editTermUseCase;
    private readonly IAddTermUseCase addTermUseCase;
    public record TermSavedMessage(Term Term);

    public TermViewModel(IViewTermUseCase viewTermUseCase, IEditTermUseCase editTermUseCase, IAddTermUseCase addTermUseCase)
    {
        this.Term = new Term();
        this.viewTermUseCase = viewTermUseCase;
        this.editTermUseCase = editTermUseCase;
        this.addTermUseCase = addTermUseCase;
    }

    public async Task LoadTerm(int termId)
    {
        this.Term = await this.viewTermUseCase.ExecuteAsync(termId);
    }

    [RelayCommand]
    public async Task AddTerm()
    {
        await this.addTermUseCase.ExecuteAsync(this.Term);

        // Send a messsage to reload terms since MAUI doesn't support databinding updating in the flyout/ flyout template
        WeakReferenceMessenger.Default.Send(new TermSavedMessage(this.Term));

        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    public async Task EditTerm()
    {
        await this.editTermUseCase.ExecuteAsync(this.Term.TermId, this.Term);

        WeakReferenceMessenger.Default.Send(new TermSavedMessage(this.Term));

        await Shell.Current.GoToAsync("..");
    }
}
