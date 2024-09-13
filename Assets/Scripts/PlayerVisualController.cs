using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerVisualController : NetworkBehaviour
{
    [SerializeField] new Renderer renderer;
    public void Reset()
    {
        renderer = GetComponent<Renderer>();
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (this.IsLocalPlayer)
        {
            renderer.material.SetColor("_Tint", Color.green);
        }
        else
        {
            renderer.material.SetColor("_Tint", Color.blue);
        }
    }
}
