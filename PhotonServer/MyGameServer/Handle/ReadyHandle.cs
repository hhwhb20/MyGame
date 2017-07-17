using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Photon.SocketServer;
using MyGameServer.Model;

namespace MyGameServer.Handle
{
    class ReadyHandle : BaseHandle
    {
        public ReadyHandle()
        {
            opCode = OperationCode.Match;
        }

        public override void OnOperationRequest(Photon.SocketServer.OperationRequest operationRequest, Photon.SocketServer.SendParameters sendParameters, ClientPeer peer)
        {
            bool isSuccess = (peer != null);

            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            if (isSuccess)
            {
                response.ReturnCode = (short)Common.ReturnCode.Success;

                Room room = peer.room;

                if(room == null)
                {
                    return;
                }

                ClientPeer other = room.FindOther(peer);

                if(other != null)
                {
                    EventData ed = new EventData((int)EventCode.MatchOther);
                    other.SendEvent(ed, sendParameters);
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
