using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Photon.SocketServer;
using Common.Tools;
using MyGameServer.Manager;
using MyGameServer.Model;

namespace MyGameServer.Handle
{
    class MatchHandle : BaseHandle
    {
        public MatchHandle()
        {
            opCode = OperationCode.Match;
        }

        public void MatchRoomFor2Players(ClientPeer player1, ClientPeer player2, Photon.SocketServer.SendParameters sendParameters)
        {
            if(player1 == null || player2 == null)
            {
                MyGameServer.Instance.PrintLog("有一方为空");
                return;
            }
            MyGameServer.Instance.PrintLog(string.Format("player1Id: {0}, player2Id: {1}", player1.user.Id, player2.user.Id));

            //设置状态
            player1.state = Player_State.PS_InRoom;
            player2.state = Player_State.PS_InRoom;

            Room_2Players room = RoomManager.Instance.GetNewRoom(RoomType.RT_2Players) as Room_2Players;

            player1.room = room;
            player2.room = room;

            room.Player1 = player1;
            room.Player2 = player2;

            //房间加入管理
            RoomManager.Instance.AddRoom(room);

            //下发消息
            { //for player1
                EventData ed = new EventData((int)EventCode.MatchOther);
                Dictionary<byte, object> data = new Dictionary<byte, object>();
                data.Add((byte)ParameterCode.Username, player2.user.Username);
                ed.Parameters = data;
                player1.SendEvent(ed, sendParameters);
            }

            { //for player2
                EventData ed = new EventData((int)EventCode.MatchOther);
                Dictionary<byte, object> data = new Dictionary<byte, object>();
                data.Add((byte)ParameterCode.Username, player1.user.Username);
                ed.Parameters = data;
                player2.SendEvent(ed, sendParameters);
            }
        }

        public override void OnOperationRequest(Photon.SocketServer.OperationRequest operationRequest, Photon.SocketServer.SendParameters sendParameters, ClientPeer peer)
        {
            bool isSuccess = (peer != null);

            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            if (isSuccess)
            {
                response.ReturnCode = (short)Common.ReturnCode.Success;

                if(peer.state == Player_State.PS_InRoom)
                {
                    return;
                }

                ClientPeer enemy = PlayerManager.Instance.FindPlayer(peer);
                if(enemy != null) //找到对手了
                {
                    MatchRoomFor2Players(peer, enemy, sendParameters);
                }
                else //没有对手的话，把玩家填入等待状态
                {
                    peer.state = Player_State.PS_Waiting;
                }
            }
            else
            {
                response.ReturnCode = (short)Common.ReturnCode.Failed;
            }

            peer.SendOperationResponse(response, sendParameters);
        }
    }
}
