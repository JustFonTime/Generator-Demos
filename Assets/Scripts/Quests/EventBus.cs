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

    public event Action OnNodeCreated;
    public void NodeCreated()
    {
        OnNodeCreated?.Invoke();
    }

    public event Action OnAllNodesCreated;

    public void AllNodesCreated()
    {
        OnAllNodesCreated?.Invoke();
    }

    public event Action OnRegenerateRequest;
    public void RegenerateRequest()
    {
        OnRegenerateRequest?.Invoke();
    }

    public event Action OnNodeOverlapped;

    public void NodeOverlap()
    {
        OnNodeOverlapped?.Invoke();
    }
}
