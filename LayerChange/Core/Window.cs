using LayerChange.Entities;
using LayerChange.Layers;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace LayerChange.Core
{
    public class Window
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", ExactSpelling = true)]
        static extern IntPtr GetAncestor(IntPtr hwnd, uint flags);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        private static void GetActiveWindowName()
        {
            // Try to get Parent Window Handle
            IntPtr activeWindowHandle = GetForegroundWindow();
            IntPtr parentWindowHandle = GetAncestor(activeWindowHandle, 3);

            uint winProcId, winParentId;

            GetWindowThreadProcessId(activeWindowHandle, out winProcId);
            GetWindowThreadProcessId(parentWindowHandle, out winParentId);

            Process winProcess = Process.GetProcessById((int)winProcId);
            Process parentProcess = Process.GetProcessById((int)winParentId);

            string winName = string.Empty;
            string processName = string.Empty;
            string fileDescription = string.Empty;

            try
            {
                if(winProcess != null)
                    winName = winProcess.MainWindowTitle;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            try
            {
                if (parentProcess != null)
                    processName = parentProcess.ProcessName;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            try
            {
                if(parentProcess != null)
                    fileDescription = parentProcess.MainModule.FileVersionInfo.FileDescription;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            WindowInfo windowInfo = new WindowInfo()
            {
                FileDescription = fileDescription != null? fileDescription : "",
                ProcessName = processName != null ? processName : "",
                WindowName = winName != null ? winName : ""
            };

            LayerManager.GetManager.SetCurrentLayer(windowInfo);
        }

        public static void OnWinEvent(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            GetActiveWindowName();
        }
    }
}
