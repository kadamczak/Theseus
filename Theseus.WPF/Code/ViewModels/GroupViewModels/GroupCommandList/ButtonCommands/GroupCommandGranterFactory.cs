using System;
using Theseus.Domain.Models.GroupRelated;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.GroupViewModels.GroupCommandList.ButtonCommands
{
    public class GroupCommandGranterFactory : CommandGranterFactory<Group, GroupButtonCommand>
    {
        private readonly ShowDetailsGroupCommandGranter _showDetailsGroupGranter;
        private readonly DeleteOrLeaveGroupCommandGranter _deleteGroupGranter;
        private readonly EmptyGroupCommandGranter _emptyGranter;

        public GroupCommandGranterFactory(ShowDetailsGroupCommandGranter showDetailsGroupGranter,
                                          DeleteOrLeaveGroupCommandGranter deleteGroupGranter,
                                          EmptyGroupCommandGranter emptyGranter)
        {
            _showDetailsGroupGranter = showDetailsGroupGranter;
            _deleteGroupGranter = deleteGroupGranter;
            _emptyGranter = emptyGranter;
        }

        public override CommandGranter<Group> Get(GroupButtonCommand chosenCommandType)
        {
            return chosenCommandType switch
            {
                GroupButtonCommand.ShowDetails => _showDetailsGroupGranter,
                GroupButtonCommand.DeleteOrLeave => _deleteGroupGranter,
                GroupButtonCommand.None => _emptyGranter,
                _ => throw new ArgumentException("Invalid group command type.")
            };
        }
    }
}
