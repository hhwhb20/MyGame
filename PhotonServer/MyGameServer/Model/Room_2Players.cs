using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameServer.Model
{
    public class Room_2Players : Room
    {
        public ClientPeer Player1 { get; set; }
        public ClientPeer Player2 { get; set; }

        public Room_2Players(long id)
        {
            Id = id;
            roomType = RoomType.RT_2Players;
            Player1 = null;
            Player2 = null;
        }

        public override ClientPeer FindOther(ClientPeer self)
        {
            if(self == Player1)
            {
                return Player2;
            }
            else if(self == Player2)
            {
                return Player1;
            }

            return null;
        }
    }
}
