using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace MyGameServer.Handle
{
    class DefaultHandle : BaseHandle
    {
        public DefaultHandle()
        {
            opCode = Common.OperationCode.Default;
        }
        public override void OnOperationRequest(Photon.SocketServer.OperationRequest operationRequest, Photon.SocketServer.SendParameters sendParameters, ClientPeer peer)
        {
            throw new NotImplementedException();
        }
    }
}
