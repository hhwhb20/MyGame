using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Common;

public class PhotonEngine : MonoBehaviour, IPhotonPeerListener
{

    public static PhotonPeer Peer
    {
        get
        {
            return peer;
        }
    }
    public static PhotonEngine Instance;
    private static PhotonPeer peer;

    private Dictionary<OperationCode, Request> requestDict = new Dictionary<OperationCode, Request>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    // Use this for initialization
    void Start()
    {
        //通过Listender接收服务器端的响应
        peer = new PhotonPeer(this, ConnectionProtocol.Udp);
        peer.Connect("127.0.0.1:5055", "MyGame1");
    }

    // Update is called once per frame
    void Update()
    {
        peer.Service();
    }

    void Ondestroy()
    {
        if (peer != null && peer.PeerState == PeerStateValue.Connected)
        {
            //断开连接
            peer.Disconnect();
        }
    }

    public void DebugReturn(DebugLevel level, string message)
    {

    }

    public void OnEvent(EventData eventData)
    {
        switch (eventData.Code)
        {
            case 1:
                {
                    Debug.Log("hahahaha");

                    Dictionary<byte, object> data = eventData.Parameters;
                    object intValue; object stringValue;
                    data.TryGetValue(1, out intValue);
                    data.TryGetValue(2, out stringValue);
                    Debug.Log(intValue.ToString() + stringValue.ToString());
                }
                break;
            default:
                break;
        }
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        OperationCode opCode = (OperationCode)operationResponse.OperationCode;
        Request request = null;
        bool temp = requestDict.TryGetValue(opCode, out request);

        if (temp)
        {
            request.OnOperationResponse(operationResponse);
        }
        else
        {
            Debug.Log("operationResponse 没找到");
        }
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        Debug.Log(statusCode);
    }

    public void AddRequest(Request request)
    {
        requestDict.Add(request.opCode, request);
    }

    public void RemoveRequest(Request request)
    {
        requestDict.Remove(request.opCode);
    }
}
