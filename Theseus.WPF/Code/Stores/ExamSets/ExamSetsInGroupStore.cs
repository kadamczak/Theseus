using System.Collections.ObjectModel;
using Theseus.Domain.Models.ExamSetRelated;

namespace Theseus.WPF.Code.Stores.ExamSets
{
    /// <summary>
    /// The <c>ExamSetsInGroupStore</c> class stores currently selected <c>ExamSet</c>s to be available in a <c>Group</c>.
    /// </summary>
    public class ExamSetsInGroupStore
    {
        public ObservableCollection<ExamSet> SelectedExamSets = new ObservableCollection<ExamSet>();
    }
}