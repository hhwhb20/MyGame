using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameServer.Model
{
    public enum RoomType
    {
        RT_2Players,
    };
    public abstract class Room
    {
        public long Id;
        public RoomType roomType;

        public abstract ClientPeer FindOther(ClientPeer self);
    }
}
