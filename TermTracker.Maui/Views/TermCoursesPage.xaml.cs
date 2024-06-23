using CommunityToolkit.Mvvm.ComponentModel;
using TermTracker.CoreBusiness;

namespace TermTracker.Maui.Views
{
    [QueryProperty(nameof(Term), "Term")]
    public partial class TermCoursesPage : ContentPage
    {
        private Term _term;

        public TermCoursesPage()
        {
            InitializeComponent();
        }

        public Term Term
        {
            get => _term;
            set
            {
                _term = value;
                OnPropertyChanged(nameof(Term)); 
                UpdateTitle();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateTitle(); 
        }

        private void UpdateTitle()
        {
            Title = ($"{Term.TermName} Courses");
        }
    }
}
