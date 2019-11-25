using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CAK
{
    public partial class MainForm : Form
    {
        public static Universe Universe { get; set; }
        public static bool EnableTimeTravel { get; set; }
        private int _cellSize = 25;

        public MainForm()
        {
            Universe = new Universe(15, 15);
            InitializeComponent();
        }

        public void UpdateState()
        {
            int index = TimeList.SelectedIndex;

            if (index < 0)
            {
                index = 0;
            }

            TimeList.Items.Clear();

            for (int i = 0; i < Universe.TimeSpace.Count; i++)
            {
                TimeList.Items.Add((i + 1).ToString());
            }


            TimeList.SelectedIndex = index;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.SelectedIndex = 0;
            UpdateState();
            drawSurface.Invalidate();
            EnableTimeTravel = toolStripButton3.Checked;


            if (!Directory.Exists("Images"))
            {
                Directory.CreateDirectory("Images");
            }
        }
        

        private void drawSurface_Paint(object sender, PaintEventArgs e)
        {
            using (var img = new Bitmap((_cellSize + 2) * Universe.Width, (_cellSize + 2) * Universe.Height))
            using (var g = Graphics.FromImage(img))
            {
                g.Clear(Color.Black);


                for (int h = 0; h < Universe.Height; h++)
                {
                    for (int w = 0; w < Universe.Width; w++)
                    {
                        var x = (_cellSize + 2) * w;
                        var y = (_cellSize + 2) * h;

                        g.FillRectangle(
                            Universe.TimeSpace[TimeList.SelectedIndex][h * Universe.Width + w] == 1
                                ? Brushes.White
                                : Brushes.Gray, x, y, _cellSize, _cellSize);
                    }
                }

                e.Graphics.Clear(Color.Black);
                e.Graphics.DrawImageUnscaled(img, 0, 0);
            }
        }

        private void drawSurface_MouseClick(object sender, MouseEventArgs e)
        {
            var x = e.X / (_cellSize + 2);
            var y = e.Y / (_cellSize + 2);

            var val = Universe.TimeSpace[TimeList.SelectedIndex][y * Universe.Width + x];
            Universe.TimeSpace[TimeList.SelectedIndex][y * Universe.Width + x] = val == 0 ? 1 : 0;

            drawSurface.Invalidate();
        }

        public void Step()
        {
            int off = 0;
            if (toolStripComboBox1.SelectedIndex == 0)
            {
                off = Universe.StepSingleInstanceEngine(TimeList.SelectedIndex);
            }
            else if (toolStripComboBox1.SelectedIndex == 1)
            {
                off = TimeList.SelectedIndex = Universe.StepContinuous(TimeList.SelectedIndex);
            }
            else if (toolStripComboBox1.SelectedIndex == 2)
            {
                off = TimeList.SelectedIndex = Universe.StepBranchContinuous(TimeList.SelectedIndex);
            }


            UpdateState();
            TimeList.SelectedIndex = off;
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                Step();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Step();
        }

        private void TimeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            drawSurface.Invalidate();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Universe = new Universe(25, 25);
            TimeList.SelectedIndex = -1;
            UpdateState();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            EnableTimeTravel = toolStripButton3.Checked;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 50; i++)
            {
                Step();
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            using (var img = new Bitmap((_cellSize + 2) * Universe.Width, (_cellSize + 2) * Universe.Height))
            using (var g = Graphics.FromImage(img))
            {
                g.Clear(Color.Black);


                for (int h = 0; h < Universe.Height; h++)
                {
                    for (int w = 0; w < Universe.Width; w++)
                    {
                        var x = (_cellSize + 2) * w;
                        var y = (_cellSize + 2) * h;

                        g.FillRectangle(
                            Universe.TimeSpace[TimeList.SelectedIndex][h * Universe.Width + w] == 1
                                ? Brushes.White
                                : Brushes.Gray, x, y, _cellSize, _cellSize);
                    }
                }

                
                img.Save(Path.Combine("Images", TimeList.SelectedIndex + ".png"));
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < TimeList.Items.Count; i++)
            {
                using (var img = new Bitmap((_cellSize + 2) * Universe.Width, (_cellSize + 2) * Universe.Height))
                using (var g = Graphics.FromImage(img))
                {
                    g.Clear(Color.Black);


                    for (int h = 0; h < Universe.Height; h++)
                    {
                        for (int w = 0; w < Universe.Width; w++)
                        {
                            var x = (_cellSize + 2) * w;
                            var y = (_cellSize + 2) * h;

                            g.FillRectangle(
                                Universe.TimeSpace[i][h * Universe.Width + w] == 1
                                    ? Brushes.White
                                    : Brushes.Gray, x, y, _cellSize, _cellSize);
                        }
                    }

                
                    img.Save(Path.Combine("Images", i + ".png"));
                }
            }
        }
    }
}