using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Menu : MonoBehaviour
{
    public static Menu instance;
    public event Action OnMenuClick;
    public event Action HideEvent;
    public event Action WalkEvent;
    public event Action RoundEvent;
    public event Action TimeTipEvent;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }
    public void OnHide()
    {
        HideEvent?.Invoke();
    }
    public void OnWalk()
    {
        WalkEvent?.Invoke();
    }
    public void OnRound()
    {
        RoundEvent?.Invoke();
    }
    public void OnTimeTip()
    {
        TimeTipEvent?.Invoke();
    }
}
