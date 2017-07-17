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
    class LoginHandle : BaseHandle
    {
        public LoginHandle()
        {
            opCode = OperationCode.Login;
        }

        public override void OnOperationRequest(Photon.SocketServer.OperationRequest operationRequest, Photon.SocketServer.SendParameters sendParameters, ClientPeer peer)
        {
            string username = DictTools.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Username) as string;
            string password = DictTools.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Password) as string;

            bool isSuccess = UserManager.Instance.VerifyUser(username, password);

            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            if(isSuccess)
            {
                response.ReturnCode = (short)Common.ReturnCode.Success;

                User user = UserManager.Instance.GetByUsername(username);
                PlayerManager.Instance.AddPlayer(peer, user);
            }
            else
            {
                response.ReturnCode = (short)Common.ReturnCode.Failed;
            }

            peer.SendOperationResponse(response, sendParameters);
        }
    }
}
