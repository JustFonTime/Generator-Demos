using System;
using System.Collections;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    public static EventBus Instance { get; private set; }

    public GetValueFromDropdown questInfluence;
    public GetValueFromSlider questLength;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public event Action OnRegenRequest;
    public void RegenRequest()
    {
        OnRegenRequest?.Invoke();
    }

}
