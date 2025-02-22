using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingleTon<DataManager>
{
    private void Update()
    {
        SaveData();
    }
    public void SaveData()
    {
        PlayerPrefs.SetFloat("AllRunTime",Gamemanager.Instance.PlayerData.Time);
    }
    public float LoadData()
    {
        return PlayerPrefs.GetFloat("AllRunTime");
    }
}
