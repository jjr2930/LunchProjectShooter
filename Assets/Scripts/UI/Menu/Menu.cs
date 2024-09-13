using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void Awake()
    {
        EventContainer.onButtonClicked += OnButtonClicked;
    }

    public void OnDestroy()
    {
        EventContainer.onButtonClicked -= OnButtonClicked;
    }

    private void OnButtonClicked(UiId id, ButtonBase button)
    {
        switch (id)
        {
            case UiId.Menu_StartHostButton:
                break;
            case UiId.Menu_JoinHostWithIpButton:
                break;
            case UiId.Menu_JoinRanomHostButton:
                break;
            case UiId.Menu_IpInputField:
                break;
            default:
                break;
        }
    }
}
