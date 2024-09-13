using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UiId 
{
    [Obsolete("IT IS NOT RECOMMENDED TO USE THIS VALUE")]
    MENU = 100000,
    [Obsolete("IT IS NOT RECOMMENDED TO USE THIS VALUE")]
    GAME = 200000,
    [Obsolete("IT IS NOT RECOMMENDED TO USE THIS VALUE")]
    BUTTON =10000,
    [Obsolete("IT IS NOT RECOMMENDED TO USE THIS VALUE")]
    INPUT_FIELD = 20000,

    #region BUTTONS
    Menu_StartHostButton = MENU + BUTTON + 1,
    Menu_JoinHostWithIpButton ,
    Menu_JoinRanomHostButton,
    #endregion

    #region INPUT_FIELDS
    Menu_IpInputField = MENU + INPUT_FIELD + 1
    #endregion
}