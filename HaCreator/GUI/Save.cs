using HaCreator.MapEditor;
using HaCreator.WzStructure;
using MapleLib.WzLib.WzStructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HaCreator.GUI
{
    public partial class Save : Form
    {
        private Board board;

        public Save(Board board)
        {
            this.board = board;
            InitializeComponent();
            idBox.Text = board.MapInfo.id == -1 ? "" : board.MapInfo.id.ToString();
            idBox_TextChanged(null, null);
        }

        private void idBox_TextChanged(object sender, EventArgs e)
        {
            int id = 0;
            if (idBox.Text == "")
            {
                statusLabel.Text = "Please choose an ID";
                saveButton.Enabled = false;
            }
            else if (!int.TryParse(idBox.Text, out id))
            {
                statusLabel.Text = "Must enter a number";
                saveButton.Enabled = false;
            }
            else if (id < WzConstants.MinMap || id > WzConstants.MaxMap)
            {
                statusLabel.Text = "Out of range";
                saveButton.Enabled = false;
            }
            else if (WzInfoTools.GetMapStringProp(id.ToString()) != null)
            {
                statusLabel.Text = "WARNING: Will overwrite existing map";
                saveButton.Enabled = true;
            }
            else
            {
                statusLabel.Text = "";
                saveButton.Enabled = true;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            MapSaver saver = new MapSaver(board);
            int newId = int.Parse(idBox.Text);
            saver.ChangeMapID(newId);
            saver.SaveMapImage();
            MessageBox.Show("Saved map with ID: " + newId.ToString());
            Close();
        }
    }
}
