namespace FinalPaint;

internal class Romb : ToolControl
{
    public Romb(Pen p, Tools tool, int size, Color color, Point start, Point end, Graphics g) : base(p, tool, size,
        color, start, end, g)
    {
    }

    public new void Draw()
    {
        Point[] curvePoints =
        {
            new(Start.X + (End.X - Start.X) / 2, Start.Y),
            new(End.X, Start.Y + (End.Y - Start.Y) / 2),
            new(Start.X + (End.X - Start.X) / 2, End.Y),
            new(Start.X, Start.Y + (End.Y - Start.Y) / 2)
        };

        G.DrawPolygon(P, curvePoints);
    }
}