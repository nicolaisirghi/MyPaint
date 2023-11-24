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
            G.DrawLines(P, new Point[] { new Point(Start.X, Start.Y), new Point(End.X, End.Y), new Point(Start.X - (End.X - Start.X), End.Y), new Point(Start.X, Start.Y) });
        }
    }
}
