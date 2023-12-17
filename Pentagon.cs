namespace FinalPaint;

internal class Pentagon : ToolControl
{
    public Pentagon(Pen p, Tools tool, int size, Color color, Point start, Point end, Graphics g) : base(p, tool, size,
        color, start, end, g)
    {
    }

    public new void Draw()
    {
        Point[] curvePoints =
        {
            new(Start.X, Start.Y + (End.Y - Start.Y) / 2),
            new(Start.X + (End.X - Start.X) / 2, Start.Y),
            new(End.X, Start.Y + (End.Y - Start.Y) / 2),
            new(End.X - (End.X - Start.X) / 3, End.Y),
            new(Start.X + (End.X - Start.X) / 3, End.Y)
        };

        G.DrawPolygon(P, curvePoints);
    }
}