using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGameServer.Model;

namespace MyGameServer.Manager
{
    class PlayerManager
    {
        private static PlayerManager _instance = null;

        private static Dictionary<int, ClientPeer> playerDict = new Dictionary<int, ClientPeer>();
        private static List<int> emptyList = new List<int>();

        private static int playerIndex = 0;

        public static PlayerManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlayerManager();
                }

                return _instance;
            }
        }

        public  void AddPlayer(ClientPeer peer, User user)
        {
            if (peer == null || user == null)
            {
                return;
            }

            peer.user = user;
            if(emptyList.Count > 0)
            {
                peer.playId = emptyList[0];
                emptyList.Remove(emptyList[0]);
            }
            else
            {
                peer.playId = playerIndex;
                playerIndex++;
            }

            playerDict.Add(peer.playId, peer);
            MyGameServer.Instance.PrintLog("一个玩家进入等待");
        }

        public void AddPlayer(ClientPeer peer)
        {
            if(peer == null || peer.playId == -1 || peer.user == null)
            {
                return;
            }

            playerDict.Add(peer.playId, peer);
        }

        public  ClientPeer FindPlayer(ClientPeer self) //寻找对手
        {
            MyGameServer.Instance.PrintLog(string.Format("数量：{0}", playerDict.Count));
            foreach (ClientPeer peer in playerDict.Values)
            {
                if ((peer != null) && (peer.room == null) && (peer.state == Player_State.PS_Waiting) && (self.playId != peer.playId))
                {
                    return peer;
                }
            }

            return null;
        }

        public void RemovePlayer(int playerId)
        {
            if (playerDict.ContainsKey(playerId))
            {
                playerDict[playerId] = null;
                emptyList.Add(playerId);
            }
        }
    }
}
