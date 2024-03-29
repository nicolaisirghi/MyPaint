﻿namespace FinalPaint;

internal class Star : ToolControl
{
    public Star(Pen p, Tools tool, int size, Color color, Point start, Point end, Graphics g) : base(p, tool, size,
        color, start, end, g)
    {
    }

    public new void Draw()
    {
        Point[] curvePoints =
        {
            new(Start.X + (End.X - Start.X) / 2, Start.Y),
            new(Start.X + (End.X - Start.X) / 3, Start.Y + (End.Y - Start.Y) / 3),
            new(Start.X, Start.Y + (End.Y - Start.Y) / 3),
            new(Start.X + (End.X - Start.X) / 4, Start.Y + (End.Y - Start.Y) / 2),
            new(Start.X, End.Y),
            new(Start.X + (End.X - Start.X) / 2, Start.Y + Start.Y / 10 + (End.Y - Start.Y) / 2),
            new(End.X, End.Y),
            new(Start.X + (End.X - Start.X) / 4 * 3, Start.Y + (End.Y - Start.Y) / 2),
            new(End.X, Start.Y + (End.Y - Start.Y) / 3),
            new(Start.X + (End.X - Start.X) / 3 * 2, Start.Y + (End.Y - Start.Y) / 3)
        };

        G.DrawPolygon(P, curvePoints);
    }
}