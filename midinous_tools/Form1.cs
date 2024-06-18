using System.Diagnostics;
using System.Security;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace midinous_tools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cmb_pointType.SelectedIndex = 0; 
            listBoxPoints.DataSource = savefile.lines_point;
        }

        private void num_scale_ValueChanged(object sender, EventArgs e)
        {
            lbl_scale.Text = "(" + num_scale.Value / 144 + ")";
        }

        private void num_orig_y_ValueChanged(object sender, EventArgs e)
        {
            refresh_orig_lbl();
        }

        private void num_orig_x_ValueChanged(object sender, EventArgs e)
        {
            refresh_orig_lbl();
        }

        private void refresh_orig_lbl()
        {
            lbl_origin.Text = "(" + num_orig_x.Value / 144 + "," + num_orig_y.Value / 144 + ")";
        }

        private void Btn_load_json_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog()
            {
                FileName = "Select a Midinous json file",
                Filter = "Json files (*.json)|*.json",
                Title = "Open Midinous json file"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = ofd.FileName;
                    savefile.LoadJson(File.ReadAllLines(filePath));
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
            listBoxPoints.DataSource = savefile.lines_point;
            listBoxPoints.Refresh();
        }

        mn_json savefile = new mn_json();

        private void Btn_save_json_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                FileName = "Select a Midinous json file",
                Filter = "Json files (*.json)|*.json",
                Title = "Save Midinous json file"
            };


            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = saveFileDialog.FileName;

                    string save_data = savefile.SaveJson();
                    File.WriteAllText(filePath, save_data);
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }



        private void btn_generate_regular_polygon_Click(object sender, EventArgs e)
        {
            savefile.generate_poly_points((int)num_scale.Value, (int)num_sides.Value, ((double)num_orig_x.Value, (double)num_orig_y.Value),cmb_pointType.SelectedIndex,chkbx_GenerateCwPaths.Checked,chkbx_GenerateCcwPaths.Checked);

            listBoxPoints.DataSource = savefile.lines_point;
            listBoxPoints.Refresh();
        }

        private void btn_fix_Click(object sender, EventArgs e)
        {
            savefile.reset_Ids();
            listBoxPoints.DataSource = savefile.lines_point;
            listBoxPoints.Refresh();
            MessageBox.Show("Ids have been reset", "Info", MessageBoxButtons.OK);
        }
    }
}
