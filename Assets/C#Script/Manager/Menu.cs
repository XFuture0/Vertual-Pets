using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Menu : SingleTon<Menu>
{
    public event Action OnMenuClick;
    public event Action HideEvent;
    public event Action WalkEvent;
    public event Action RoundEvent;
    public event Action TimeTipEvent;
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
