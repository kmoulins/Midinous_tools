namespace midinous_tools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cmb_pointType.SelectedIndex = 0;
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

        }

        private void Btn_save_json_Click(object sender, EventArgs e)
        {

        }
    }
}
