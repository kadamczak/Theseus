using Theseus.Domain.Models.GroupRelated;

namespace Theseus.WPF.Code.ViewModels.Bindings.GroupBindings
{
    public class GroupCommandViewModel : CommandViewModel
    {
        public Group Group { get; set; }

        public GroupCommandViewModel(Group group)
        {
            Group = group;
        }
    }
}