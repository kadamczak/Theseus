using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;

namespace Theseus.WPF.Code.Stores.ExamSets
{
    /// <summary>
    /// The <c>ExamSetReturnServiceStore</c> class stores the most recently chosen <c>ExamSet</c> return service.
    /// </summary>
    public class ExamSetReturnServiceStore
    {
        public NavigationService<ViewModelBase> ExamSetNavigationService { get; set; }
    }
}
