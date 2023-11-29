using Theseus.Domain.Models.GroupRelated;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.GroupViewModels.GroupCommandList.Info.Implementations
{
    public class EmptyGroupInfoGranter : InfoGranter<Group>
    {
        public override string GrantInfo(CommandViewModel<Group> commandViewModel)
        {
            return string.Empty;
        }
    }
}