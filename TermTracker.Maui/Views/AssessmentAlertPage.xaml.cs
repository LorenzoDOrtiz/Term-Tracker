using TermTracker.Maui.ViewModels;

namespace TermTracker.Maui.Views
{
    public partial class AssessmentAlertPage : ContentPage
    {
        private CourseViewModel courseViewModel;

        public AssessmentAlertPage(CourseViewModel courseViewModel)
        {
            InitializeComponent();
            this.courseViewModel = courseViewModel;

            this.BindingContext = this.courseViewModel;

            Title = $"{this.courseViewModel.CurrentAssessment.Name} Alerts";
        }
    }
}
