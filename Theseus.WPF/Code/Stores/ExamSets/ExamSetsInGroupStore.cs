using System.Collections.ObjectModel;
using Theseus.Domain.Models.ExamSetRelated;

namespace Theseus.WPF.Code.Stores.ExamSets
{
    public class ExamSetsInGroupStore
    {
        public ObservableCollection<ExamSet> SelectedExamSets = new ObservableCollection<ExamSet>();
    }
}