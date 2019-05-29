using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class ServerSetup : MonoBehaviour
{
    [SerializeField]
    NetworkManager networkManager;

    [SerializeField]
    InputField serverNameField;

    // Start is called before the first frame update
    void Start()
    {
        serverNameField.text = networkManager.networkAddress;
    }

    public void SetNetworkAddress(string name)
    {
        networkManager.networkAddress = name;
    }

    public void CreateServer()
    {
        networkManager.StartServer();
    }
}
