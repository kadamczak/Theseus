using System;
using Theseus.Domain.Models.GroupRelated;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.GroupViewModels.GroupCommandList.Info.Implementations;

namespace Theseus.WPF.Code.ViewModels.GroupViewModels.GroupCommandList.Info
{
    public class GroupInfoGranterFactory : InfoGranterFactory<Group, GroupInfo>
    {
        private readonly EmptyGroupInfoGranter _emptyInfoGranter;
        private readonly GeneralGroupInfoGranter _generalInfoGranter;

        public GroupInfoGranterFactory(EmptyGroupInfoGranter emptyInfoGranter, GeneralGroupInfoGranter generalInfoGranter)
        {
            _emptyInfoGranter = emptyInfoGranter;
            _generalInfoGranter = generalInfoGranter;
        }

        public override InfoGranter<Group> Create(GroupInfo chosenInfoType)
        {
            return chosenInfoType switch
            {
                GroupInfo.None => _emptyInfoGranter,
                GroupInfo.GeneralInfo => _generalInfoGranter,
                _ => throw new ArgumentException("Invalid group info type.")
            };
        }
    }
}