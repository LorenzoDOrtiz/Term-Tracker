using System.Runtime.CompilerServices;

namespace TermTracker.Maui.Views.Controls;

public partial class CourseControl : ContentView
{
    public bool IsForEdit { get; set; }
    public bool IsForAdd { get; set; }

    public CourseControl()
    {
        InitializeComponent();
    }
    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (IsForAdd)
            btnSave.SetBinding(Button.CommandProperty, "AddCourseCommand");
    }
}