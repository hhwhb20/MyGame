using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common.Tools;
using MyGameServer.Handle;
using Common;
using MyGameServer.Model;

namespace MyGameServer
{
    public enum Player_State
    {
        PS_Slience, //沉默状态
        PS_Waiting, //等待匹配状态
        PS_Gaming,  //游戏中
        PS_InRoom,  //在房间中
    };
    public class ClientPeer : Photon.SocketServer.ClientPeer
    {
        public Room room{ get; set; }
        public Player_State state { get; set; }
        public int playId { get; set; }
        public User user { get; set; }

        public ClientPeer(InitRequest initRequest):base(initRequest)
        {
            room = null;
            state = Player_State.PS_Slience;
            playId = -1;
            user = null;
        }

        //处理断开连接的后续工作
        protected override void OnDisconnect(PhotonHostRuntimeInterfaces.DisconnectReason reasonCode, string reasonDetail)
        {
           
        }

        //处理客户端的请求
        protected override void OnOperationRequest(OperationRequest operationRequest,SendParameters sendParameters)
        {
            MyGameServer.Instance.PrintLog(operationRequest.OperationCode.ToString());
            BaseHandle handle = DictTools.GetValue<OperationCode, BaseHandle>(MyGameServer.Instance.HandlerDict, (OperationCode)operationRequest.OperationCode);
            if(handle != null)
            {
                handle.OnOperationRequest(operationRequest, sendParameters, this);
            }
            else
            {
                BaseHandle defaultHandle = DictTools.GetValue<OperationCode, BaseHandle>(MyGameServer.Instance.HandlerDict, (OperationCode)operationRequest.OperationCode);
                defaultHandle.OnOperationRequest(operationRequest, sendParameters, this);
            }
        }
    }
}
