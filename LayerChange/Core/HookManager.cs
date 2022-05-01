using System;
using System.Runtime.InteropServices;

namespace LayerChange.Core
{
    public class HookManager
    {
        delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);
        
        static WinEventDelegate eventDelegate;
        public static void StartWinEventHook()
        {
            eventDelegate = new WinEventDelegate(Window.OnWinEvent);
            IntPtr m_hhook = SetWinEventHook(3, 3, IntPtr.Zero, eventDelegate, 0, 0, 1);
        }

    }
}
