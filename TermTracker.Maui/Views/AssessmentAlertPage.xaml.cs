using TermTracker.CoreBusiness.Models;
using TermTracker.Maui.ViewModels;

namespace TermTracker.Maui.Views
{
    [QueryProperty(nameof(AssessmentType), "assessmentType")]
    public partial class AssessmentAlertPage : ContentPage
    {
        private CourseViewModel courseViewModel;
        private string assessmentType;

        public string AssessmentType
        {
            get => assessmentType;
            set
            {
                assessmentType = value;
                SetCurrentAssessment();
            }
        }

        public AssessmentAlertPage(CourseViewModel courseViewModel)
        {
            InitializeComponent();
            this.courseViewModel = courseViewModel;
            this.BindingContext = this.courseViewModel;
        }

        private void SetCurrentAssessment()
        {
            if (courseViewModel != null)
            {
                if (AssessmentType == "Objective")
                {
                    courseViewModel.CurrentAssessment = courseViewModel.Course.Assessments.OfType<ObjectiveAssessment>().FirstOrDefault();
                }
                else if (AssessmentType == "Performance")
                {
                    courseViewModel.CurrentAssessment = courseViewModel.Course.Assessments.OfType<PerformanceAssessment>().FirstOrDefault();
                }
                Title = $"{courseViewModel.CurrentAssessment.Name} Alerts";
            }
        }
    }
}
