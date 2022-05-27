using Akiled.Communication.Packets.Outgoing;using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.Database.Interfaces;using Akiled.HabboHotel.GameClients;using Akiled.HabboHotel.Rooms;using Akiled.HabboHotel.Users;using System.Text;namespace Akiled.Communication.Packets.Incoming.Structure{    class RemoveRightsEvent : IPacketEvent    {        public void Parse(GameClient Session, ClientPacket Packet)        {            if (Session.GetHabbo() == null)
                return;

            Room room = AkiledEnvironment.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);            if (room == null || !room.CheckRights(Session, true))                return;            StringBuilder DeleteParams = new StringBuilder();            int Amount = Packet.PopInt();            for (int index = 0; index < Amount; ++index)            {                if (index > 0)                    DeleteParams.Append(" OR ");                int UserId = Packet.PopInt();                if (room.UsersWithRights.Contains(UserId))                    room.UsersWithRights.Remove(UserId);                DeleteParams.Append("room_id = '" + room.Id + "' AND user_id = '" + UserId + "'");                RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabboId(UserId);                if (roomUserByHabbo != null && !roomUserByHabbo.IsBot)                {                    roomUserByHabbo.GetClient().SendPacket(new YouAreControllerComposer(0));                    roomUserByHabbo.RemoveStatus("flatctrl 1");                    roomUserByHabbo.SetStatus("flatctrl 0", "");                    roomUserByHabbo.UpdateNeeded = true;                }                ServerPacket Response2 = new ServerPacket(ServerPacketHeader.FlatControllerRemovedMessageComposer);                Response2.WriteInteger(room.Id);                Response2.WriteInteger(UserId);                Session.SendPacket(Response2);                if (room.UsersWithRights.Count <= 0)                {                    ServerPacket Response3 = new ServerPacket(ServerPacketHeader.RoomRightsListMessageComposer);                    Response3.WriteInteger(room.RoomData.Id);                    Response3.WriteInteger(0);                    Session.SendPacket(Response3);                }                else                {                    ServerPacket Response = new ServerPacket(ServerPacketHeader.RoomRightsListMessageComposer);                    Response.WriteInteger(room.RoomData.Id);                    Response.WriteInteger(room.UsersWithRights.Count);                    foreach (int UserId2 in room.UsersWithRights)                    {                        Habbo habboForId = AkiledEnvironment.GetHabboById(UserId2);                        Response.WriteInteger(UserId2);                        Response.WriteString((habboForId == null) ? "Undefined (error)" : habboForId.Username);                    }                    Session.SendPacket(Response);                }            }            using (IQueryAdapter queryreactor = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())                queryreactor.RunQuery("DELETE FROM room_rights WHERE " + (DeleteParams).ToString());        }    }}