using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : SingleTon<MouseManager>
{
    public Vector2 MouseOffect;
    public Texture2D point1;
    public Texture2D point2;
    private bool IsPoint2;
    protected override void Awake()
    {
        base.Awake();
        Cursor.SetCursor(point1, MouseOffect, CursorMode.Auto);
    }
    private void Update()
    {
        SwitchMouseTexture();
    }
    private void SwitchMouseTexture()
    {
        if((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1)) && !IsPoint2)
        {
            IsPoint2 = true;
            Cursor.SetCursor(point2, MouseOffect, CursorMode.Auto);
            StartCoroutine(StayMouseswitch());
        }
    }
    private IEnumerator StayMouseswitch()
    {
        yield return new WaitForSeconds(1f);
        IsPoint2 = false;
        Cursor.SetCursor(point1, MouseOffect, CursorMode.Auto);
    }
}
