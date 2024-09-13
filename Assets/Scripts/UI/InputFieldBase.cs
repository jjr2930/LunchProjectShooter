using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class InputFieldBase : MonoBehaviour
{
    [SerializeField] protected TMP_InputField inputField;
    [SerializeField] protected UiId id;

    public virtual void Reset()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    public virtual void Awake()
    {
        inputField.onValueChanged.AddListener(OnValueChanged);
    }

    public virtual void OnDestroy()
    {
        inputField.onValueChanged.RemoveListener(OnValueChanged);
    }

    public virtual void OnValueChanged(string value)
    {
        EventContainer.onInputFieldChanged?.Invoke(id, value, this);
    }
}
