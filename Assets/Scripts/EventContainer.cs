using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class EventContainer 
{
    public delegate void OnInputFieldChangedCallback(UiId id, string value, InputFieldBase inputField);
    public delegate void OnButtonClickedCallback(UiId id, ButtonBase button );
    public static OnButtonClickedCallback onButtonClicked;
    public static OnInputFieldChangedCallback onInputFieldChanged;
}
