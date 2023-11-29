using Theseus.Domain.Models.GroupRelated;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.GroupViewModels.GroupCommandList.Info
{
    public class GroupInfoGranterFactory : InfoGranterFactory<Group, GroupInfo>
    {
        public override InfoGranter<Group> Create(GroupInfo chosenInfoType)
        {
            throw new System.NotImplementedException();
        }
    }
}