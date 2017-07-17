using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGameServer.Model;
using Common.Map;

namespace MyGameServer.Manager
{
    class RoomManager
    {
        private static RoomManager _instance = null;

        private static Dictionary<long, Room> roomDict = new Dictionary<long, Room>();
        private static List<long> emptyList = new List<long>();
        private static long roomIndex = 0;

        public static RoomManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RoomManager();
                }

                return _instance;
            }
        }

        public Room GetNewRoom(RoomType type)
        {
            Room room = null;
            switch(type)
            {
                case RoomType.RT_2Players:
                    {
                        long roomId = GetNewRoomIndex();
                        room = new Room_2Players(roomId);
                    }
                    break;
                default:
                    break;
            }

            return room;
        }

        public void AddRoom(Room room)
        {
            if(room == null)
            {
                return;
            }

            if(roomDict.ContainsKey(room.Id))
            {
                roomDict[room.Id] = room;
            }
            else
            {
                roomDict.Add(room.Id, room);
            }
        }

        public void RemoveRoom(long Id)
        {
            if(roomDict.ContainsKey(Id))
            {
                roomDict[Id] = null;
                emptyList.Add(Id);
            }
        }

        public long GetNewRoomIndex()
        {
            long roomId;
            if (emptyList.Count > 0)
            {
                roomId = emptyList[0];
                emptyList.Remove(emptyList[0]);
            }
            else
            {
                roomId = roomIndex;
            }

            return roomId;
        }
    }
}
