using System.ComponentModel;
using System.Drawing.Imaging;

namespace FinalPaint
{
    public partial class PaintApp : Form
    {
        public PaintApp()
        {
            InitializeComponent();
            bmp = new Bitmap(Board.Width, Board.Height);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Closing += PaintApp_Leave;

            this.Name = "Paint";
            this.Text = "Paint";

            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            Board.Image = bmp;

            color = Color.Black;
            size = pencilSize = 1;
            sizeInput.Maximum = 10;
            Tool = Tools.Pencil;
            lastTool = Tools.Pencil;

            tools = new Control[] { PencilBtn, EraserBtn, LineBtn, RectangleBtn, EllipseBtn, FillBtn, TriangleBtn, RightTriangleBtn, PentagonBtn, HexagonBtn, StarBtn, RombBtn, TrapezBtn };


            PencilBtn.BackColor = Color.FromArgb(192, 255, 255);
            Board.Cursor = GetCursor(Properties.Resources.icons8_pencil_30);
            sampleTools = new Tools[] { Tools.Pencil, Tools.Eraser };
            fillTools = new Tools[] { Tools.Line, Tools.Rectangle, Tools.Ellipse, Tools.Triangle, Tools.RightTriangle, Tools.Pentagon, Tools.Hexagon, Tools.Star, Tools.Romb, Tools.Trapez };

            p = new Pen(color, pencilSize);
        }

        Bitmap bmp;
        Color color;
        Graphics g;
        Control[] tools;
        Point px, py;
        Tools Tool, lastTool;
        int pencilSize, size, x, y, cX, cY;
        Pen p;
        Tools[] sampleTools, fillTools;
        bool isDrawing = false;

        private void Board_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            py = e.Location;
            cX = e.X;
            cY = e.Y;
        }


        private void Draw(ToolControl tool)
        {
            switch (tool)
            {
                case Line line:
                    line.Draw();
                    break;
                case Eraser eraser:
                    eraser.Draw();
                    break;
                case Fill fill:
                    fill.Draw();
                    break;
                case Rectangle rectangle:
                    rectangle.Draw();
                    break;
                case Ellipse ellipse:
                    ellipse.Draw();
                    break;
                case Triangle triangle:
                    triangle.Draw();
                    break;
                case Pentagon pentagon:
                    pentagon.Draw();
                    break;
                case Hexagon hexagon:
                    hexagon.Draw();
                    break;
                case Star star:
                    star.Draw();
                    break;
                case Romb romb:
                    romb.Draw();
                    break;
                case Trapez trapez:
                    trapez.Draw();
                    break;
            }
        }

        private void Board_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                px = e.Location;



                if (Array.Exists(sampleTools, IsEqualTool))
                {
                    int size = Tool == Tools.Pencil ? pencilSize : this.size;

                    ToolControl tool = Utils.GetTool(p, Tool, size, color, py, px, g);
                    Draw(tool);

                }

                py = px;


            }

            Board.Refresh();

            x = e.X;
            y = e.Y;

            locationLabel.Text = "Location : {X=" + x + ", Y=" + y + "}";


        }

        private bool IsEqualTool(Tools tool)
        {
            return Tool == tool;
        }

        private void Board_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;


            if (Array.Exists(fillTools, IsEqualTool))
            {
                ToolControl tool = Utils.GetTool(p, Tool, size, color, new Point(cX, cY), new Point(x, y), g);
                Draw(tool);

            }
        }


        private void ChangeSize()
        {
            if (Tool == Tools.Pencil)
                return;

            if (lastTool == Tools.Pencil)
            {
                pencilSize = (int)sizeInput.Value;
            }

            sizeInput.Maximum = 100;
            sizeInput.Value = size;
        }


        private void ChangeTool(Tools tool, Cursor cursor, bool changeSize = true)
        {
            lastTool = Tool;
            Tool = tool;
            SetActiveTool();
            Board.Cursor = cursor;

            if (changeSize)
            {
                ChangeSize();
            }
        }

        private void RightTriangleBtn_Click(object sender, EventArgs e)
        {

            ChangeTool(Tools.RightTriangle, Cursors.Cross);

        }

        private void PencilBtn_Click(object sender, EventArgs e)
        {
            ChangeTool(Tools.Pencil, GetCursor(Properties.Resources.icons8_pencil_30), false);
            sizeInput.Value = pencilSize;


        }

        private void EraserBtn_Click(object sender, EventArgs e)
        {
            ChangeTool(Tools.Eraser, GetCursor(Properties.Resources.icons8_eraser_30));
        }

        private void EllipseBtn_Click(object sender, EventArgs e)
        {
            ChangeTool(Tools.Ellipse, Cursors.Cross);
        }

        private void RectangleBtn_Click(object sender, EventArgs e)
        {
            ChangeTool(Tools.Rectangle, Cursors.Cross);
        }

        private void LineBtn_Click(object sender, EventArgs e)
        {
            ChangeTool(Tools.Line, Cursors.Cross);
        }

        private void FillBtn_Click(object sender, EventArgs e)
        {
            ChangeTool(Tools.Fill, GetCursor(Properties.Resources.icons8_fill_color_30));

        }

        private void Triangle_Click(object sender, EventArgs e)
        {
            ChangeTool(Tools.Triangle, Cursors.Cross);
        }

        private void PentagonBtn_Click(object sender, EventArgs e)
        {
            ChangeTool(Tools.Pentagon, Cursors.Cross);
        }

        private void HexagonBtn_Click(object sender, EventArgs e)
        {
            ChangeTool(Tools.Hexagon, Cursors.Cross);
        }

        private void StarBtn_Click(object sender, EventArgs e)
        {
            ChangeTool(Tools.Star, Cursors.Cross);

        }

        private void RombBtn_Click(object sender, EventArgs e)
        {
            ChangeTool(Tools.Romb, Cursors.Cross);
        }

        private void HeartBtn_Click(object sender, EventArgs e)
        {
            ChangeTool(Tools.Trapez, Cursors.Cross);
        }

        private Cursor GetCursor(Bitmap cursor)
        {
            return new Cursor(cursor.GetHicon());
        }

        private void Board_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (isDrawing && Array.Exists(fillTools, IsEqualTool))
            {
                ToolControl tool = Utils.GetTool(p, Tool, size, color, new Point(cX, cY), new Point(x, y), g);
                Draw(tool);

            }
        }


        private void Board_Click(object sender, EventArgs e)
        {

            if (Tool == Tools.Fill)
            {
                MouseEventArgs me = (MouseEventArgs)e;

                new Fill(p, Tools.Fill, 1, color, me.Location, new Point(x, y), g, bmp).Draw();
            }
            if (Tool == Tools.Dropper)
            {
                MouseEventArgs me = (MouseEventArgs)e;
                color = bmp.GetPixel(me.X, me.Y);
                currentColor.BackColor = color;
                currentColor.ForeColor = color;
            }

        }

        private void sizeInput_ValueChanged(object sender, EventArgs e)
        {
            if (Tool == Tools.Pencil)
            {
                pencilSize = (int)sizeInput.Value;
                sizeInput.Maximum = 10;

                p = new Pen(color, pencilSize);
            }

            else
            {
                size = (int)sizeInput.Value;
            }
        }

        private void color_Click(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            color = l.BackColor;

            currentColor.BackColor = color;
            currentColor.ForeColor = color;

        }

        private void SetActiveTool()
        {
            foreach (Control c in tools)
            {
                c.BackColor = Color.Transparent;
                if (c.Tag.ToString() == Tool.ToString())
                {
                    c.BackColor = Color.FromArgb(192, 255, 255);
                }
            }

        }



        private void customColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.ShowDialog();
            color = cd.Color;
            currentColor.BackColor = color;
            currentColor.ForeColor = color;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JPEG|*.jpg";
            if (sfd.ShowDialog() == DialogResult.OK)
            {

                Bitmap bitmap = bmp.Clone(new System.Drawing.Rectangle(0, 0, Board.Width, Board.Height), bmp.PixelFormat);
                bmp.Save(sfd.FileName, ImageFormat.Jpeg);
                MessageBox.Show("Image Saved Successfully");
            }

        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            Board.Image = bmp;
        }



        private void PaintApp_Leave(object sender, CancelEventArgs e)
        {
            string msg = "Do you want to save before closing?";
            DialogResult result = MessageBox.Show(msg, "Close Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SaveBtn_Click(sender, e);

            }
            e.Cancel = false;

        }

        private void DroppperBtn_Click(object sender, EventArgs e)
        {
            ChangeTool(Tools.Dropper, GetCursor(Properties.Resources.icons8_dropper_30));
        }
    }
}