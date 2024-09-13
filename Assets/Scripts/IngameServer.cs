using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class IngameServer : MonoBehaviour
{
    [SerializeField] NetworkManager networkManager;
    [SerializeField] int maximumPlayerCount = 4;

    public void Reset()
    {
        networkManager = GetComponent<NetworkManager>();
    }

    public void Awake()
    {
        networkManager.ConnectionApprovalCallback += ConnectionApproval;
        networkManager.OnClientConnectedCallback += OnClientConnected;
        networkManager.OnClientDisconnectCallback += OnClientDisconnect;
        networkManager.OnClientStarted += OnClientStarted;
        networkManager.OnClientStopped += OnClientStopped;
        networkManager.OnConnectionEvent += OnConnectionEvent;
        networkManager.OnServerStarted += OnServerStarted;
        networkManager.OnServerStopped += OnServerStopped;
        networkManager.OnTransportFailure += OnTransportFailure;

        EventContainer.onButtonClicked += OnButtonClicked;
    }

    public void OnDestroy()
    {
        networkManager.Shutdown();
        networkManager.ConnectionApprovalCallback -= ConnectionApproval;
        networkManager.OnClientConnectedCallback -= OnClientConnected;
        networkManager.OnClientDisconnectCallback -= OnClientDisconnect;
        networkManager.OnClientStarted -= OnClientStarted;
        networkManager.OnClientStopped -= OnClientStopped;
        networkManager.OnConnectionEvent -= OnConnectionEvent;
        networkManager.OnServerStarted -= OnServerStarted;
        networkManager.OnServerStopped -= OnServerStopped;
        networkManager.OnTransportFailure -= OnTransportFailure;

        EventContainer.onButtonClicked -= OnButtonClicked;
    }

    private void OnButtonClicked(UiId id, ButtonBase button)
    {
        switch (id)
        {
            case UiId.Menu_StartHostButton:
                networkManager.StartHost();
                break;
            
            case UiId.Menu_JoinHostWithIpButton:
                //
                break;
            
            case UiId.Menu_JoinRanomHostButton:
                //networkManager.StartClient();
                break;

        }
    }

    private void ConnectionApproval(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        if(networkManager.ConnectedClientsList.Count == maximumPlayerCount)
        {
            response.Approved = false;
            response.Reason = "Server is full";
            return;
        }

        response.Approved = true;
        response.CreatePlayerObject = true;
        response.Rotation = Quaternion.identity;
        response.Position = Vector3.zero;

        Debug.Log($"{request.ClientNetworkId} ConnectionApproval : true");
    }

    private void OnClientConnected(ulong obj)
    {
        Debug.Log("OnClientConnected");
    }

    private void OnClientDisconnect(ulong obj)
    {
        Debug.Log("OnClientDisconnect");
    }

    private void OnClientStarted()
    {
        Debug.Log("OnClientStarted");
    }

    private void OnClientStopped(bool obj)
    {
        Debug.Log("OnClientStopped");
    }

    private void OnConnectionEvent(NetworkManager manager, ConnectionEventData data)
    {
        Debug.Log("OnConnectionEvent");
    }

    private void OnServerStarted()
    {
        Debug.Log("OnServerStarted, try to change scene to main");
        networkManager.SceneManager.LoadScene("Main", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    private void OnServerStopped(bool obj)
    {
        Debug.Log("OnServerStopped");
    }

    private void OnTransportFailure()
    {
        Debug.Log("OnTransportFailure");
    }
}
