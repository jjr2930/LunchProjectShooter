using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonBase : MonoBehaviour
{
    [SerializeField] protected Button button;
    [SerializeField] protected UiId id;
    public virtual void Reset()
    {
        button = GetComponent<Button>();
    }

    public virtual void Awake()
    {
        button.onClick.AddListener(OnClicked);
    }

    public virtual void OnDestroy()
    {
        button.onClick.RemoveListener(OnClicked);
    }

    public virtual void OnClicked()
    {
        EventContainer.onButtonClicked?.Invoke(id, this);
    }
}
