using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameStateInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log(IngameState.HostIpToJoin);
    }
}
