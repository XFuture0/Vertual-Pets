using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
public class MainPet : MonoBehaviour
{
    [DllImport("user32.dll")]
    public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(IntPtr intPtr, IntPtr hWnddInsertAfter,int x, int y, int cx, int cy,uint uFlags);//设定窗口位置和层级
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();//活动窗口
    [DllImport("user32.dll")]
    private static extern int SetLayeredWindowAttributes(IntPtr intPtr, uint cr, uint Alpha, uint dwwFlags);//设置窗口透明度
    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr intPtr,int Index,uint dwNewLong);//改变窗口样式属性
    private struct MARGINS//结构体(窗口尺寸)
    {
        public int cxL;
        public int cxR;
        public int cyL;
        public int cyR;
    };
    [DllImport("Dwmapi.dll")]//设定窗口接口
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr intPtr, ref MARGINS margins);//窗口透明化
    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);//窗口置顶
    private IntPtr intPtr;
    private void Start()
    {
        intPtr = GetActiveWindow();
        MARGINS margins = new MARGINS { cxL = -1 };//玻璃化
        DwmExtendFrameIntoClientArea(intPtr, ref margins);
        SetWindowLong(intPtr,-20, 0x00080000);//分层
        SetLayeredWindowAttributes(intPtr, 0, 0, 0x00000001);//将窗口中所以颜色为0的地方变为透明
        SetWindowPos(intPtr, HWND_TOPMOST, 0, 0, 0, 0, 0);
        Application.runInBackground = true;//后台运行
    }
}