using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WaitingUI : MonoBehaviour
{
    public void Awake()
    {
        EventContainer.onButtonClicked += OnButtonClicked;
        gameObject.SetActive(false);
    }

    public void OnDestroy()
    {
        EventContainer.onButtonClicked -= OnButtonClicked;
    }

    private void OnButtonClicked( UiId id, ButtonBase button)
    {
        switch (id)
        {
            case UiId.Menu_StartHostButton:
            case UiId.Menu_JoinHostWithIpButton:
            case UiId.Menu_JoinRanomHostButton:
                gameObject.SetActive(true);
                break;
            
            default:
                if (gameObject.activeSelf)
                    gameObject.SetActive(false);
                break;
        }
    }
}
