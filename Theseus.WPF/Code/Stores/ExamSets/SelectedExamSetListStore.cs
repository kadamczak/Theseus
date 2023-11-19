using System.Collections.Generic;
using Theseus.Domain.Models.ExamSetRelated;

namespace Theseus.WPF.Code.Stores.ExamSets
{
    public class SelectedExamSetListStore
    {
        public IEnumerable<ExamSet> ExamSets { get; set; }
    }
}