using TermTracker.CoreBusiness.Models;
using TermTracker.Maui.ViewModels;

namespace TermTracker.Maui.Views
{
    [QueryProperty(nameof(Term), "Term")]
    public partial class TermCoursesPage : ContentPage
    {
        private readonly CoursesViewModel coursesViewModel;
        private Term _term;

        public TermCoursesPage(CoursesViewModel coursesViewModel)
        {
            InitializeComponent();
            this.coursesViewModel = coursesViewModel;
            BindingContext = this.coursesViewModel;
        }

        public Term Term
        {
            get => _term;
            set
            {
                _term = value;
                OnPropertyChanged(nameof(Term));
                UpdateTitle();
                coursesViewModel.CurrentTerm = _term; // Pass the term to the ViewModel
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            UpdateTitle();
            await this.coursesViewModel.LoadCoursesAsync(Term.TermId);
        }

        private void UpdateTitle()
        {
            Title = ($"{Term.TermName} Courses");
        }
    }
}
