using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : SingleTon<Gamemanager>
{
    public ChacaterData PlayerData;
    public void InitializedPlayer(ChacaterData player)
    {
        PlayerData = player;
    }
}
