using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using MyGameServer.Model;
using MyGameServer.Manager;
using Photon.SocketServer;
using Common.Tools;

namespace MyGameServer.Handle
{
    class RegisterHandle : BaseHandle
    {
        public RegisterHandle()
        {
            opCode = OperationCode.Register;
        }

        public override void OnOperationRequest(Photon.SocketServer.OperationRequest operationRequest, Photon.SocketServer.SendParameters sendParameters, ClientPeer peer)
        {
            string username = DictTools.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Username) as string;
            string password = DictTools.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Password) as string;

            User user = UserManager.Instance.GetByUsername(username);
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            if(user == null)
            {
                user = new User(){Username = username,Password = password};
                UserManager.Instance.Add(user);
                response.ReturnCode = (short)ReturnCode.Success;
            }
            else
            {
                response.ReturnCode = (short)ReturnCode.Failed;
            }

            peer.SendOperationResponse(response, sendParameters);
        }
    }
}
