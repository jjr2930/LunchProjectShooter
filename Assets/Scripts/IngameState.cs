using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class IngameState 
{
    public static string HostIpToJoin { get; set; }

    static IngameState()
    {
        EventContainer.onInputFieldChanged += OnInputFieldChanged;
    }

    private static void OnInputFieldChanged(UiId id, string value, InputFieldBase inputField)
    {
        HostIpToJoin = value;
    }
}
