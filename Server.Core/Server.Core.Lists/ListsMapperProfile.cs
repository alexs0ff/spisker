using System;
using Server.Core.Common.Entities.Lists;
using Server.Core.Common.Entities.Users;
using Server.Core.Common.Mapper;
using Server.Core.Lists.Models;
using Server.Core.Lists.Workflow;

namespace Server.Core.Lists
{
    /// <summary>
    /// Профиль для списков.
    /// </summary>
    public class ListsMapperProfile: MapperBaseProfile
    {
        public ListsMapperProfile()
        {
            RegisterWorkflowParams<ListParametersBinder>();

            RegisterList();
            RegisterListItem();
            RegisterOwnedList();
        }

        private void RegisterListItem()
        {
            CreateMap<ListItem, ListItemModel>()
                .ForMember(m => m.CreateEventTime, u =>u.MapFrom(s => s.CreateEventTimeUTC))
                .ForMember(m => m.EditEventTime, u => u.MapFrom(l => l.EditEventTimeUTC))
                .ForMember(m => m.OwnerId, u => u.MapFrom(l => l.PortalUserID))
                .ForMember(m => m.Id, u => u.MapFrom(l => l.ListItemID.ToString()));

            CreateMap<ListItemModel, ListItem>()
                .ForMember(m => m.CreateEventTimeUTC, u => u.MapFrom(s => s.CreateEventTime))
                .ForMember(m => m.EditEventTimeUTC, u => u.MapFrom(l => l.EditEventTime))
                .ForMember(m => m.PortalUserID, u => u.MapFrom(l => new Guid(l.OwnerId)))
                .ForMember(m => m.ListItemID, u => u.MapFrom(l => new Guid(l.Id)));
        }

        private void RegisterOwnedList()
        {
            CreateMap<OwnedList, ListModel>()
                .ForMember(m => m.CreateEventTime, u => u.MapFrom(s => s.CreateEventTimeUTC))
                .ForMember(m => m.Id, u => u.MapFrom(l => l.ListID))
                .ForMember(m => m.OwnerId, u => u.MapFrom(l => l.PortalUserID))
                .ForMember(m => m.OwnerFullName, u => u.MapFrom(l => $"{l.LastName} {l.FirstName} {l.MiddleName}"))
                .ForMember(m => m.OwnerLogin, u => u.MapFrom(l => l.UserName))
                .ForMember(m => m.ListCheckItemKind, u => u.MapFrom(l => l.ListCheckItemKindID))
                .ForMember(m => m.ListKind, u => u.MapFrom(l => l.ListKindID))
                .ForMember(m => m.PublicId, u => u.MapFrom(l => l.PublicID))
                .ForMember(m => m.Items, u => u.MapFrom(l => l.ListItem))
                .ForMember(m => m.OriginId, u => u.MapFrom(l => l.OriginalPortalUserID))
                .ForMember(m => m.OriginFullName, u => u.MapFrom(l => $"{l.OriginLastName} {l.OriginFirstName} {l.OriginMiddleName}"))
                .ForMember(m => m.OriginLogin, u => u.MapFrom(l => l.OriginUserName));

            CreateMap<ListModel, OwnedList>()
                .ForMember(m => m.CreateEventTimeUTC, u => u.MapFrom(s => s.CreateEventTime))
                .ForMember(m => m.ListID, u => u.MapFrom(l => l.Id))
                .ForMember(m => m.ListCheckItemKindID, u => u.MapFrom(l => l.ListCheckItemKind))
                .ForMember(m => m.ListKindID, u => u.MapFrom(l => l.ListKind))
                .ForMember(m => m.UserName, u => u.MapFrom(l => l.OwnerLogin))
                .ForMember(m => m.PortalUserID, u => u.MapFrom(l => new Guid(l.OwnerId)));
        }

        private void RegisterList()
        {
            CreateMap<List, ListModel>()
                .ForMember(m => m.CreateEventTime, u => u.MapFrom(s => s.CreateEventTimeUTC))
                .ForMember(m => m.Id, u => u.MapFrom(l => l.ListID))
                .ForMember(m => m.OwnerId, u => u.MapFrom(l => l.PortalUserID))
                .ForMember(m => m.Items, u => u.MapFrom(l => l.ListItem));

            CreateMap<ListModel, List>()
                .ForMember(m => m.CreateEventTimeUTC, u => u.MapFrom(s => s.CreateEventTime))
                .ForMember(m => m.ListID, u => u.MapFrom(l => l.Id))
                .ForMember(m => m.PortalUserID, u => u.MapFrom(l => new Guid(l.OwnerId)));

            CreateMap<PortalUser, ListModel>()
                .ForMember(m => m.OwnerId, u => u.MapFrom(s => s.PortalUserID))
                .ForMember(m => m.OwnerFullName, u => u.MapFrom(s => GetFullName(s)))
                .ForMember(m => m.OwnerLogin, u => u.MapFrom(s => s.UserName));
        }

        private string GetFullName(PortalUser user)
        {
            return $"{user.LastName} {user.FirstName} {user.MiddleName}";
        }
    }
}
