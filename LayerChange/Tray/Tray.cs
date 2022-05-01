using LayerChange.Core;
using LayerChange.Properties;
using System;
using System.Windows.Forms;

namespace LayerChange
{
    public class Tray : IDisposable
    {
        /// <summary>
        /// The NotifyIcon object.
        /// </summary>
        NotifyIcon notifyIcon;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tray"/> class.
        /// </summary>
        public Tray()
        {
            notifyIcon = new NotifyIcon();
        }

        /// <summary>
        /// Displays the icon in the system tray.
        /// </summary>
        public void Display()
        {
            // Put the icon in the system tray and allow it react to mouse clicks.
            notifyIcon.Icon = Resources.LayerChangeIcon;
            notifyIcon.Text = "Layer Change QMK";
            notifyIcon.Visible = true;

            // Attach context menu
            notifyIcon.ContextMenuStrip = new ContextMenu().Create();

            // Start Service
            HookManager.StartWinEventHook();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        public void Dispose()
        {
            notifyIcon.Visible=false;
            notifyIcon.Icon.Dispose();
            notifyIcon.Icon = null;
            notifyIcon.Dispose();
        }
    }
}
