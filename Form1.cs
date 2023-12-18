using System.ComponentModel;
using System.Drawing.Imaging;
using FinalPaint.Properties;
using Rect = System.Drawing.Rectangle;

namespace FinalPaint;

public partial class PaintApp : Form
{
    private Bitmap bmp;
    private Color color;

    private readonly ContextMenuStrip ContextMenu;
    private readonly ToolStripMenuItem ContextMenuFileAbout;
    private readonly ToolStripMenuItem ContextMenuFileExit;

    private readonly ToolStripMenuItem ContextMenuFileOpen;
    private readonly ToolStripMenuItem ContextMenuFileRedo;
    private readonly ToolStripMenuItem ContextMenuFileReset;
    private readonly ToolStripMenuItem ContextMenuFileSave;
    private readonly ToolStripMenuItem ContextMenuFileUndo;
    private Font font;
    private FontFamily fontFamily;
    private int fontSize;
    private FontStyle fontStyle;
    private Graphics g;
    private bool isDrawing;
    private bool isSelected;
    private int keyCode;

    private int counter = 0;

    private readonly MenuStrip MainMenu;

    private readonly ToolStripMenuItem MenuEdit;
    private readonly ToolStripMenuItem MenuEditRedo;
    private readonly ToolStripMenuItem MenuEditReset;
    private readonly ToolStripMenuItem MenuEditUndo;
    private readonly ToolStripMenuItem MenuFile;
    private readonly ToolStripMenuItem MenuFileExit;
    private readonly ToolStripMenuItem MenuFileOpen;
    private readonly ToolStripMenuItem MenuFileSave;

    private readonly ToolStripMenuItem MenuHelp;
    private readonly ToolStripMenuItem MenuHelpAbout;
    private Pen p;
    private int pencilSize, size, x, y, cX, cY;
    private Point px, py;
    private readonly Stack<Bitmap> redoStack;
    private readonly Tools[] sampleTools;
    private readonly Tools[] fillTools;
    private Rect SelectionRect;

    private int stackCounter;
    private Tools Tool, lastTool;
    private readonly Control[] tools;


    private readonly Stack<Bitmap> undoStack;

    public PaintApp()
    {
        InitializeComponent();
        bmp = new Bitmap(Board.Width, Board.Height);

        Closing += PaintApp_Leave;

        Name = "Paint";
        Text = "Paint";

        g = Graphics.FromImage(bmp);
        g.Clear(Color.White);
        Board.Image = bmp;

        var fontFamily = new FontFamily("Segoe UI");
        font = new Font(fontFamily, 12, FontStyle.Regular);

        color = Color.Black;
        size = pencilSize = 1;
        sizeInput.Maximum = 10;
        Tool = Tools.Pencil;
        lastTool = Tools.Pencil;

        tools = new Control[]
        {
            PencilBtn, EraserBtn, LineBtn, DroppperBtn, RectangleBtn, EllipseBtn, FillBtn, TriangleBtn,
            RightTriangleBtn, PentagonBtn, HexagonBtn, StarBtn, RombBtn, TrapezBtn, TypographyBtn, selectionBtn
        };


        PencilBtn.BackColor = Color.FromArgb(192, 255, 255);
        Board.Cursor = GetCursor(Resources.icons8_pencil_30);
        sampleTools = new[] { Tools.Pencil, Tools.Eraser };
        fillTools = new[]
        {
            Tools.Line, Tools.Rectangle, Tools.Ellipse, Tools.Triangle, Tools.RightTriangle, Tools.Pentagon,
            Tools.Hexagon, Tools.Star, Tools.Romb, Tools.Trapez, Tools.Selection
        };

        p = new Pen(color, pencilSize);


        MenuFile = new ToolStripMenuItem("File");


        MenuFileOpen = new ToolStripMenuItem("Open image");
        MenuFileOpen.Click += uploadBtn_Click;
        MenuFileOpen.ShortcutKeys = Keys.Control | Keys.U;

        MenuFileSave = new ToolStripMenuItem("Save");
        MenuFileSave.Click += SaveBtn_Click;
        MenuFileSave.ShortcutKeys = Keys.Control | Keys.S;


        MenuFileExit = new ToolStripMenuItem("Exit");
        MenuFileExit.Click += PaintApp_Leave;


        ContextMenuFileSave = new ToolStripMenuItem("Save");
        ContextMenuFileSave.Click += SaveBtn_Click;
        ContextMenuFileSave.ShortcutKeys = Keys.Control | Keys.S;
        ContextMenuFileSave.ShowShortcutKeys = true;


        ContextMenuFileExit = new ToolStripMenuItem("Exit");
        ContextMenuFileExit.Click += PaintApp_Leave;


        MenuFile.DropDownItems.AddRange(new ToolStripItem[]
        {
            MenuFileOpen,
            new ToolStripSeparator(),
            MenuFileSave,
            new ToolStripSeparator(),
            MenuFileExit
        });


        MenuEdit = new ToolStripMenuItem("&Edit");

        MenuEditReset = new ToolStripMenuItem("&Reset");
        MenuEditReset.ShortcutKeys = Keys.Control | Keys.R;
        MenuEditReset.ShowShortcutKeys = true;
        MenuEditReset.Click += ResetBtn_Click;

        MenuEditUndo = new ToolStripMenuItem("&Undo");
        MenuEditUndo.ShortcutKeys = Keys.Control | Keys.Z;
        MenuEditUndo.ShowShortcutKeys = true;
        MenuEditUndo.Click += UndoAction;


        MenuEditRedo = new ToolStripMenuItem("&Redo");
        MenuEditRedo.ShortcutKeys = Keys.Control | Keys.Y;
        MenuEditRedo.ShowShortcutKeys = true;
        MenuEditRedo.Click += RedoAction;

        MenuEdit.DropDownItems.AddRange(new ToolStripItem[]
        {
            MenuEditReset,
            new ToolStripSeparator(),
            MenuEditUndo,
            new ToolStripSeparator(),
            MenuEditRedo
        });


        ContextMenuFileOpen = new ToolStripMenuItem("Open image");
        ContextMenuFileOpen.Click += uploadBtn_Click;
        ContextMenuFileOpen.ShortcutKeys = Keys.Control | Keys.U;
        ContextMenuFileOpen.ShowShortcutKeys = true;


        ContextMenuFileUndo = new ToolStripMenuItem("Undo");
        ContextMenuFileUndo.Click += UndoAction;
        ContextMenuFileUndo.ShortcutKeys = Keys.Control | Keys.Z;
        ContextMenuFileUndo.ShowShortcutKeys = true;

        ContextMenuFileRedo = new ToolStripMenuItem("Redo");
        ContextMenuFileRedo.Click += RedoAction;
        ContextMenuFileRedo.ShortcutKeys = Keys.Control | Keys.Y;
        ContextMenuFileRedo.ShowShortcutKeys = true;


        ContextMenuFileReset = new ToolStripMenuItem("Reset");
        ContextMenuFileReset.Click += ResetBtn_Click;
        ContextMenuFileReset.ShortcutKeys = Keys.Control | Keys.R;
        ContextMenuFileReset.ShowShortcutKeys = true;


        MenuHelp = new ToolStripMenuItem("&Help");

        MenuHelpAbout = new ToolStripMenuItem("&Shortcuts");
        MenuHelpAbout.Click += ShortcutsBtn_Click;

        MenuHelp.DropDownItems.AddRange(new ToolStripItem[]
        {
            MenuHelpAbout
        });


        ContextMenuFileAbout = new ToolStripMenuItem("About");
        ContextMenuFileAbout.Click += ShortcutsBtn_Click;


        MainMenu = new MenuStrip();

        MainMenu.Items.AddRange(new ToolStripItem[]
        {
            MenuFile,
            MenuEdit,
            MenuHelp
        });


        ContextMenu = new ContextMenuStrip();


        ContextMenu.Items.AddRange(new ToolStripItem[]
        {
            ContextMenuFileOpen,
            new ToolStripSeparator(),
            ContextMenuFileSave,
            new ToolStripSeparator(),
            ContextMenuFileUndo,
            new ToolStripSeparator(),
            ContextMenuFileRedo,
            new ToolStripSeparator(),
            ContextMenuFileReset,
            new ToolStripSeparator(),
            ContextMenuFileAbout,
            new ToolStripSeparator(),
            ContextMenuFileExit
        });

        undoStack = new Stack<Bitmap>();
        redoStack = new Stack<Bitmap>();

        Controls.Add(MainMenu);
        ContextMenuStrip = ContextMenu;
    }

    private void Board_MouseDown(object sender, MouseEventArgs e)
    {
        if ((e.Button & MouseButtons.Left) != 0)
        {
            isDrawing = true;
            py = e.Location;
            cX = e.X;
            cY = e.Y;
        }
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
            case Selection selection:
                selection.Draw();
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
                var size = Tool == Tools.Pencil ? pencilSize : this.size;

                var tool = Utils.GetTool(p, Tool, size, color, py, px, g);
                Draw(tool);

                var bitmap = bmp.Clone(new Rect(0, 0, Board.Width, Board.Height),
                    bmp.PixelFormat);


                if (stackCounter == 40)
                {
                    undoStack.Push(bitmap);
                    stackCounter = 0;
                }
                else
                {
                    stackCounter++;
                }
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
        if (e.Button == MouseButtons.Left)
        {
            if (Array.Exists(fillTools, IsEqualTool))
            {
                var tool = Utils.GetTool(p, Tool, size, color, new Point(cX, cY), new Point(x, y), g);
                Draw(tool);

                var bitmap = bmp.Clone(new Rect(0, 0, Board.Width, Board.Height),
                    bmp.PixelFormat);

                undoStack.Push(bitmap);
            }

            if (Tool == Tools.Selection)
            {
                isSelected = true;


                var StartPosition = new Point(Math.Min(cX, x), Math.Min(cY, y));
                var size = new Size(Math.Abs(cX - x), Math.Abs(cY - y));

                SelectionRect = new Rect(StartPosition, size);
            }
            else
            {
                isSelected = false;
                SelectionRect = new Rect(0, 0, 0, 0);
            }
        }
    }


    private void ChangeSize()
    {
        if (Tool == Tools.Pencil)
            return;

        if (lastTool == Tools.Pencil) pencilSize = (int)sizeInput.Value;

        sizeInput.Maximum = 100;
        sizeInput.Value = size;
    }


    private void ChangeTool(Tools tool, Cursor cursor, bool changeSize = true)
    {
        lastTool = Tool;
        Tool = tool;
        SetActiveTool();
        Board.Cursor = cursor;

        if (changeSize) ChangeSize();

        if (lastTool == Tools.Selection)
        {
            isSelected = false;
        }

        if (lastTool == Tools.Typography)
        {
            var bitmap = bmp.Clone(new Rect(0, 0, Board.Width, Board.Height),
                bmp.PixelFormat);

            undoStack.Push(bitmap);
        }
    }

    private void RightTriangleBtn_Click(object sender, EventArgs e)
    {
        ChangeTool(Tools.RightTriangle, Cursors.Cross);
    }

    private void PencilBtn_Click(object sender, EventArgs e)
    {
        ChangeTool(Tools.Pencil, GetCursor(Resources.icons8_pencil_30), false);
        sizeInput.Value = pencilSize;
    }

    private void EraserBtn_Click(object sender, EventArgs e)
    {
        ChangeTool(Tools.Eraser, GetCursor(Resources.icons8_eraser_30));
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
        ChangeTool(Tools.Fill, GetCursor(Resources.icons8_fill_color_30));
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
        var g = e.Graphics;

        if (isDrawing && Array.Exists(fillTools, IsEqualTool))
        {
            var tool = Utils.GetTool(p, Tool, size, color, new Point(cX, cY), new Point(x, y), g);
            Draw(tool);
        }
    }


    private void Board_Click(object sender, EventArgs e)
    {
        if (Tool == Tools.Fill)
        {
            var me = (MouseEventArgs)e;

            new Fill(p, Tools.Fill, 1, color, me.Location, new Point(x, y), g, bmp).Draw();


            var bitmap = bmp.Clone(new Rect(0, 0, Board.Width, Board.Height),
                bmp.PixelFormat);
            undoStack.Push(bitmap);
        }

        if (Tool == Tools.Dropper)
        {
            var me = (MouseEventArgs)e;
            color = bmp.GetPixel(me.X, me.Y);
            currentColor.BackColor = color;
            currentColor.ForeColor = color;
        }

        if (Tool == Tools.Typography)
            new Typography(p, Tools.Typography, size, color, new Point(x, y), new Point(x, y), g, Board, font)
                .Draw();

        if (Tool == Tools.Selection)
        {
            isSelected = false;
            g.DrawRectangle(new Pen(Color.White, 3), SelectionRect);
        }
    }


    private void TypographyBtn_Click(object sender, EventArgs e)
    {
        ChangeTool(Tools.Typography, Cursors.IBeam);
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
        var l = (Label)sender;
        color = l.BackColor;

        currentColor.BackColor = color;
        currentColor.ForeColor = color;
    }

    private void SetActiveTool()
    {
        foreach (var c in tools)
        {
            c.BackColor = Color.Transparent;
            if (c.Tag.ToString() == Tool.ToString()) c.BackColor = Color.FromArgb(192, 255, 255);
        }
    }


    private void customColor_Click(object sender, EventArgs e)
    {
        var cd = new ColorDialog();
        cd.ShowDialog();
        color = cd.Color;
        currentColor.BackColor = color;
        currentColor.ForeColor = color;
    }

    private void SaveBtn_Click(object sender, EventArgs e)
    {
        var sfd = new SaveFileDialog();
        sfd.Filter = "JPEG|*.jpg";
        if (sfd.ShowDialog() == DialogResult.OK)
        {
            var bitmap = bmp.Clone(new Rect(0, 0, Board.Width, Board.Height),
                bmp.PixelFormat);
            bmp.Save(sfd.FileName, ImageFormat.Jpeg);
            MessageBox.Show("Image Saved Successfully");
        }
    }

    private void ResetBtn_Click(object sender, EventArgs e)
    {
        g.Clear(Color.White);
        Board.Image = bmp;
    }


    private void PaintApp_Leave(object sender, EventArgs e)
    {
        if (undoStack.Count == 0) return;

        var msg = "Do you want to save before closing?";
        var result = MessageBox.Show(msg, "Close Confirmation",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (result == DialogResult.Yes) SaveBtn_Click(sender, e);

        var cancelEvent = e as CancelEventArgs;

        cancelEvent.Cancel = false;
    }

    private void DroppperBtn_Click(object sender, EventArgs e)
    {
        ChangeTool(Tools.Dropper, GetCursor(Resources.icons8_dropper_30));
    }


    private void SetTooltip()
    {
        toolTip1.SetToolTip(PencilBtn, "Pencil (Ctrl+P)");
        toolTip1.SetToolTip(EraserBtn, "Eraser (Ctrl+E)");
        toolTip1.SetToolTip(LineBtn, "Line (Ctrl+L)");
        toolTip1.SetToolTip(RectangleBtn, "Rectangle");
        toolTip1.SetToolTip(EllipseBtn, "Ellipse");
        toolTip1.SetToolTip(FillBtn, "Fill (Ctrl+F)");
        toolTip1.SetToolTip(TriangleBtn, "Triangle (Ctrl+T)");
        toolTip1.SetToolTip(RightTriangleBtn, "Right Triangle");
        toolTip1.SetToolTip(PentagonBtn, "Pentagon");
        toolTip1.SetToolTip(HexagonBtn, "Hexagon");
        toolTip1.SetToolTip(StarBtn, "Star");
        toolTip1.SetToolTip(RombBtn, "Romb");
        toolTip1.SetToolTip(TrapezBtn, "Trapez");
        toolTip1.SetToolTip(DroppperBtn, "Dropper (Ctrl+D)");
        toolTip1.SetToolTip(sizeInput, "Size (Ctrl +/-)");
        toolTip1.SetToolTip(currentColor, "Color");
        toolTip1.SetToolTip(customColor, "Custom Color (Ctrl+C)");
        toolTip1.SetToolTip(ShortcutsBtn, "Help (Ctrl+?)");
    }

    private void PaintApp_Load(object sender, EventArgs e)
    {
        SetTooltip();
    }

    private void UndoAction(object sender, EventArgs e)
    {
        if (undoStack.Count > 0)
        {
            redoStack.Push(new Bitmap(bmp, Board.Width, Board.Height));
            bmp = undoStack.Pop();
            g = Graphics.FromImage(bmp);
            Board.Image = bmp;
        }
    }

    private void RedoAction(object sender, EventArgs e)
    {
        if (redoStack.Count > 0)
        {
            undoStack.Push(new Bitmap(bmp, Board.Width, Board.Height));
            bmp = redoStack.Pop();
            g = Graphics.FromImage(bmp);
            Board.Image = bmp;
        }
    }

    private void HelpBtn_Click(object sender, EventArgs e)
    {
        MessageBox.Show(
            "Keyboard Shortcuts:\n\n" +
            "Ctrl + P - Pencil\n" +
            "Ctrl + E - Eraser\n" +
            "Ctrl + F - Fill\n" +
            "Ctrl + D - Dropper\n" +
            "Ctrl + L - Line\n\n\n" +
            "Ctrl + Plus - Increase Size\n" +
            "Ctrl + Minus - Decrease Size\n" +
            "Ctrl + C - Custom Color\n\n\n" +
            "Ctrl + Z - Undo\n" +
            "Ctrl + Y - Redo\n" +
            "Ctrl + S - Save\n" +
            "Ctrl + R - Reset\n" +
            "Ctrl + U - Upload\n\n\n" +
            "Ctrl + ? - Help\n"
        );
    }

    private void PasteImage()
    {
        if (Clipboard.ContainsImage())
        {
            var bitmap = (Bitmap)Clipboard.GetImage();
            g.DrawImage(bitmap, cX, cY);
        }
    }

    private void CopyImage()
    {
        if (isSelected && SelectionRect.Width > 2 && SelectionRect.Height > 2)
        {
            var bitmap =
                bmp.Clone(
                    new Rect(SelectionRect.X + 1, SelectionRect.Y + 1, SelectionRect.Width - 2,
                        SelectionRect.Height - 2),
                    bmp.PixelFormat);
            Clipboard.SetImage(bitmap);
        }
        else if (!isSelected)
        {
            var bitmap = bmp.Clone(new Rect(0, 0, Board.Width, Board.Height),
                bmp.PixelFormat);
            Clipboard.SetImage(bitmap);
        }
    }


    private void PaintApp_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.ControlKey)
            keyCode = 1;

        else if (e.KeyCode != Keys.ControlKey && keyCode == 1)
            switch (e.KeyCode)
            {
                case Keys.P:
                    PencilBtn_Click(sender, e);
                    break;
                case Keys.F:
                    FillBtn_Click(sender, e);
                    break;
                case Keys.E:
                    EraserBtn_Click(sender, e);
                    break;
                case Keys.D:
                    DroppperBtn_Click(sender, e);
                    break;


                case Keys.L:
                    LineBtn_Click(sender, e);
                    break;

                case Keys.Oemplus:
                    if (sizeInput.Value < sizeInput.Maximum)
                        sizeInput.Value += 1;
                    break;

                case Keys.OemMinus:
                    if (sizeInput.Value > sizeInput.Minimum)
                        sizeInput.Value -= 1;
                    break;


                case Keys.C:
                    CopyImage();
                    break;

                case Keys.Z:
                    UndoAction(null, null);
                    break;
                case Keys.Y:
                    RedoAction(null, null);
                    break;
                case Keys.S:
                    SaveBtn_Click(sender, e);
                    break;
                case Keys.R:
                    ResetBtn_Click(sender, e);
                    break;
                case Keys.OemQuestion:
                    HelpBtn_Click(sender, e);
                    break;
                case Keys.U:
                    uploadBtn_Click(sender, e);
                    break;
                case Keys.V:
                    PasteImage();
                    break;
                case Keys.Delete:
                    RemoveSelection();
                    break;
                default:
                    keyCode = 0;
                    break;
            }
    }


    private void RemoveSelection()
    {
        g.FillRectangle(new SolidBrush(Color.White), SelectionRect);
        Board.Image = bmp;
    }

    private void uploadBtn_Click(object sender, EventArgs e)
    {
        var ofd = new OpenFileDialog();
        ofd.Filter = "JPEG|*.jpg";
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            var bitmap = new Bitmap(ofd.FileName);
            bitmap = new Bitmap(bitmap, Board.Width, Board.Height);
            bmp = bitmap;

            g = Graphics.FromImage(bmp);
            Board.Image = bmp;
        }
    }

    private void ShortcutsBtn_Click(object sender, EventArgs e)
    {
        HelpBtn_Click(sender, e);
    }

    private void PaintApp_SizeChanged(object sender, EventArgs e)
    {
        bmp = new Bitmap(bmp, Board.Width, Board.Height);
        g = Graphics.FromImage(bmp);
        Board.Image = bmp;
    }

    private void changeFont_btn_Click(object sender, EventArgs e)
    {
        var fd = new FontDialog();
        if (fd.ShowDialog() == DialogResult.OK)
        {
            font = fd.Font;
            fontSizeLabel.Text = font.Size.ToString();
            fontFamilyLabel.Text = font.FontFamily.Name;
            fontStyleLabel.Text = font.Style.ToString();
        }
    }

    private void selectionBtn_Click(object sender, EventArgs e)
    {
        ChangeTool(Tools.Selection, Cursors.Cross);
    }
}