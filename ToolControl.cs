namespace FinalPaint;

internal class ToolControl : Paint
{
    protected Bitmap Bmp;
    protected Color Color;
    protected Point End;
    protected Graphics G;
    protected Pen P;
    protected int Size;
    protected Point Start;
    protected Tools Tool;


    public ToolControl(Pen p, Tools tool, int size, Color color, Point start, Point end, Graphics g, Bitmap bmp = null)
    {
        P = p;
        Tool = tool;
        Size = size;
        Color = color;
        Start = start;
        End = end;
        G = g;
        Bmp = bmp;

        p.Color = color;
        p.Width = size;
    }


    public void Draw()
    {
        throw new NotImplementedException();
    }
}