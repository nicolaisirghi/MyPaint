namespace FinalPaint;

internal class Trapez : ToolControl
{
    public Trapez(Pen p, Tools tool, int size, Color color, Point start, Point end, Graphics g) : base(p, tool, size,
        color, start, end, g)
    {
    }

    public new void Draw()

    {
        Point[] curvePoints =
        {
            new(End.X, End.Y),
            new(Start.X, End.Y),
            new(Start.X - (Start.X - End.X) / 4, Start.Y),
            new(End.X + (Start.X - End.X) / 4, Start.Y)
        };

        G.DrawPolygon(P, curvePoints);
    }
}