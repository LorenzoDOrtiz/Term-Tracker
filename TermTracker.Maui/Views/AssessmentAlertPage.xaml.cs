using TermTracker.CoreBusiness.Models;
using TermTracker.Maui.ViewModels;

namespace TermTracker.Maui.Views
{
    [QueryProperty(nameof(Assessment), "Assessment")]
    public partial class AssessmentAlertPage : ContentPage
    {
        private readonly CourseViewModel courseViewModel;
        private Assessment assessment;

        public Assessment Assessment
        {
            get => assessment;
            set
            {
                assessment = value;
                this.courseViewModel.CurrentAssessment = assessment;
                Title = $"{this.courseViewModel.CurrentAssessment.Type} Alerts";
            }
        }

        public AssessmentAlertPage(CourseViewModel courseViewModel)
        {
            InitializeComponent();
            this.courseViewModel = courseViewModel;

            this.BindingContext = this.courseViewModel;
        }
    }
}
