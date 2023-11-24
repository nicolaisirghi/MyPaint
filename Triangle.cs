namespace FinalPaint
{
    internal class Triangle : ToolControl
    {

        public Triangle(Pen p, Tools tool, int size, Color color, Point start, Point end, Graphics g) : base(p, tool, size, color, start, end, g)
        {

        }

        public new void Draw()
        {

            if (Tool == Tools.RightTriangle)
            {
                DrawRightTriangle();
            }
            else
            {
                DrawNormalTriangle();
            }

        }

        public void DrawRightTriangle()
        {
            G.DrawLines(P, new Point[] { new Point(Start.X, Start.Y), new Point(End.X, End.Y), new Point(Start.X, End.Y), new Point(Start.X, Start.Y) });
        }

        public void DrawNormalTriangle()
        {
            Point StartPoint = new Point(Math.Min(Start.X, End.X), Math.Min(Start.Y, End.Y));
            Point EndPoint = new Point(Math.Max(Start.X, End.X), Math.Max(Start.Y, End.Y));
            G.DrawLines(P, new Point[] { new Point(StartPoint.X, StartPoint.Y), new Point(EndPoint.X, EndPoint.Y), new Point(EndPoint.X-StartPoint.X, EndPoint.Y), new Point(StartPoint.X, StartPoint.Y) });
        }
    }
}
