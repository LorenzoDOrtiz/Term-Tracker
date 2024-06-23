﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Threading.Channels;
using TermTracker.CoreBusiness;
using TermTracker.Maui.Interfaces;
using TermTracker.UseCases.Interfaces;

namespace TermTracker.Maui.ViewModels;
public partial class TermViewModel : ObservableObject
{
    [ObservableProperty]
    private Term term;

    private readonly IViewTermUseCase viewTermUseCase;
    private readonly IEditTermUseCase editTermUseCase;
    private readonly IAddTermUseCase addTermUseCase;
    private readonly IAlertService alertService;

    public record TermSavedMessage(Term Term);

    public TermViewModel(IViewTermUseCase viewTermUseCase, 
                         IEditTermUseCase editTermUseCase, 
                         IAddTermUseCase addTermUseCase, 
                         IAlertService alertService)
    {
        this.Term = new Term();
        this.viewTermUseCase = viewTermUseCase;
        this.editTermUseCase = editTermUseCase;
        this.addTermUseCase = addTermUseCase;
        this.alertService = alertService;
    }

    public async Task LoadTerm(int termId)
    {
        this.Term = await this.viewTermUseCase.ExecuteAsync(termId);
    }

    [RelayCommand]
    public async Task AddTerm()
    {
        if (!await  IsValidTerm())
        {
            return;
        }
        await this.addTermUseCase.ExecuteAsync(this.Term);

        // Send a messsage to reload terms since MAUI doesn't support databinding updating in the flyout/ flyout template
        WeakReferenceMessenger.Default.Send(new TermSavedMessage(this.Term));

        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    public async Task EditTerm()
    {
        if (!await IsValidTerm())
        {
            return;
        }
        await this.editTermUseCase.ExecuteAsync(this.Term.TermId, this.Term);

        WeakReferenceMessenger.Default.Send(new TermSavedMessage(this.Term));

        await Shell.Current.GoToAsync("..");
    }

    public async Task<bool> IsValidTerm()
    {

        if (string.IsNullOrEmpty(this.Term.TermName))
        {
            string message = "Term name can not be empty.";
            await this.alertService.ShowToast(message);
            return false;
        }

        if (Term.TermStartDate.Date > Term.TermEndDate.Date)
        {
            string message = "Term start date can not be after term end date.";
            await this.alertService.ShowToast(message);
            return false;
        }

        return true;
    }
}
