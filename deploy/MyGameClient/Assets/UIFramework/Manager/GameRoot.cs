using UnityEngine;
using System.Collections;

public class GameRoot : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("start");
        UIManager.Instance.PushPanel(UIPanelType.Login);
	}
	

}
