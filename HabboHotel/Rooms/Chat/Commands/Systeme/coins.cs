using Akiled.HabboHotel.GameClients;namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd{    class Coins : IChatCommand    {        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)        {            Room currentRoom = Session.GetHabbo().CurrentRoom;            GameClient clientByUsername = AkiledEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);            if (clientByUsername != null)            {                int result = 0;                if (int.TryParse(Params[2], out result))                {                    clientByUsername.GetHabbo().Credits = clientByUsername.GetHabbo().Credits + result;                    clientByUsername.GetHabbo().UpdateCreditsBalance();                    clientByUsername.SendNotification(Session.GetHabbo().Username + AkiledEnvironment.GetLanguageManager().TryGetValue("coins.awardmessage1", Session.Langue) + result.ToString() + AkiledEnvironment.GetLanguageManager().TryGetValue("coins.awardmessage2", Session.Langue));                    Session.SendNotification(AkiledEnvironment.GetLanguageManager().TryGetValue("coins.updateok", Session.Langue));                }                else                    Session.SendNotification(AkiledEnvironment.GetLanguageManager().TryGetValue("input.intonly", Session.Langue));            }            else                Session.SendNotification(AkiledEnvironment.GetLanguageManager().TryGetValue("input.usernotfound", Session.Langue));        }    }}