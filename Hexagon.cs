namespace FinalPaint
{
    internal class Hexagon : ToolControl
    {

        public Hexagon(Pen p, Tools tool, int size, Color color, Point start, Point end, Graphics g) : base(p, tool, size, color, start, end, g)
        {

        }

        public new void Draw()
        {

            Point[] curvePoints =
            {
                 new Point(Start.X + (End.X - Start.X) / 4, Start.Y),
                 new Point(End.X - (End.X - Start.X) / 4, Start.Y),
                 new Point(End.X, Start.Y + (End.Y - Start.Y) / 2),
                 new Point(End.X - (End.X - Start.X) / 4, End.Y),
                 new Point(Start.X + (End.X - Start.X) / 4, End.Y),
                 new Point(Start.X, Start.Y + (End.Y - Start.Y) / 2)
             };

            G.DrawPolygon(P, curvePoints);

        }

    }
}
