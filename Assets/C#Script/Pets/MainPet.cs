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
    private static extern bool SetWindowPos(IntPtr intPtr, IntPtr hWnddInsertAfter,int x, int y, int cx, int cy,uint uFlags);//�趨����λ�úͲ㼶
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();//�����
    [DllImport("user32.dll")]
    private static extern int SetLayeredWindowAttributes(IntPtr intPtr, uint cr, uint Alpha, uint dwwFlags);//���ô���͸����
    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr intPtr,int Index,uint dwNewLong);//�ı䴰����ʽ����
    private struct MARGINS//�ṹ��(���ڳߴ�)
    {
        public int cxL;
        public int cxR;
        public int cyL;
        public int cyR;
    };
    [DllImport("Dwmapi.dll")]//�趨���ڽӿ�
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr intPtr, ref MARGINS margins);//����͸����
    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);//�����ö�
    private IntPtr intPtr;
    private void Start()
    {
        intPtr = GetActiveWindow();
        MARGINS margins = new MARGINS { cxL = -1 };//������
        DwmExtendFrameIntoClientArea(intPtr, ref margins);
        SetWindowLong(intPtr,-20, 0x00080000);//�ֲ�
        SetLayeredWindowAttributes(intPtr, 0, 0, 0x00000001);//��������������ɫΪ0�ĵط���Ϊ͸��
        SetWindowPos(intPtr, HWND_TOPMOST, 0, 0, 0, 0, 0);
        Application.runInBackground = true;//��̨����
    }
}