using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChacaterData : MonoBehaviour
{
    public TimeDataSO RunTime;
    public float Time
    {
        get
        {
            if(RunTime != null)
            {
                return RunTime.time;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            RunTime.time = value;
        }
    }

}
