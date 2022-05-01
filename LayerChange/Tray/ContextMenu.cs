using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LayerChange
{
    public class ContextMenu
    {
        bool isAboutLoaded = false;

		public ContextMenuStrip Create()
		{
			// Add the default menu options.
			ContextMenuStrip menu = new ContextMenuStrip();
			ToolStripMenuItem item;
			ToolStripSeparator sep;

			// Exit.
			item = new ToolStripMenuItem();
			item.Text = "Exit";
			item.Click += new System.EventHandler(Exit_Click);
			menu.Items.Add(item);

			return menu;
		}

		void Exit_Click(object sender, EventArgs e)
		{
			// Quit without further ado.
			Application.Exit();
		}
	}
}
