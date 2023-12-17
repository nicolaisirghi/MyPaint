namespace FinalPaint;

internal class Eraser : ToolControl
{
    public Eraser(Pen p, Tools tool, int size, Color color, Point start, Point end, Graphics g) : base(p, tool, size,
        color, start, end, g)
    {
    }

    public new void Draw()
    {
        P.Color = Color.White;
        G.DrawLine(P, Start, End);
    }
}