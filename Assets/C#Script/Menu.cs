using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("¹ã²¥")]
    public VoidEventSO HideEvent;
    public VoidEventSO WalkEvent;
    public VoidEventSO RoundEvent;
    public void OnHide()
    {
        HideEvent.RaiseEvent();
    }
    public void OnWalk()
    {
        WalkEvent.RaiseEvent();
    }
    public void OnRound()
    {
        RoundEvent.RaiseEvent();
    }
}
