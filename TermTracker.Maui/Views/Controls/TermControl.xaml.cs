using System.Runtime.CompilerServices;

namespace TermTracker.Maui.Views.Controls;

public partial class TermControl : ContentView
{
    public bool IsForEdit { get; set; }
    public bool IsForAdd { get; set; }

    public TermControl()
    {
        InitializeComponent();
    }
    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (IsForAdd && !IsForEdit)
            btnSave.SetBinding(Button.CommandProperty, "AddTermCommand");
        else if (!IsForAdd && IsForEdit)
            btnSave.SetBinding(Button.CommandProperty, "EditTermCommand");
    }
}