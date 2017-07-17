using Common;
using UnityEngine;
using ExitGames.Client.Photon;

public abstract class Request : MonoBehaviour
{
    public OperationCode opCode;
    public abstract void OnOperationResponse(OperationResponse operationResponse);
    public abstract void DefaultRequest();

    public virtual void Start()
    {
        PhotonEngine.Instance.AddRequest(this);
    }

    public void OnDestroy()
    {
        PhotonEngine.Instance.RemoveRequest(this);
    }
}
