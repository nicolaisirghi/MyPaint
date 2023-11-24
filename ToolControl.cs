namespace FinalPaint
{
    internal class ToolControl : Paint
    {
        protected Tools Tool;
        protected Pen P;
        protected Graphics G;
        protected int Size;
        protected Color Color;
        protected Point Start;
        protected Point End;
        protected Bitmap Bmp;



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
            switch (Tool)
            {
                case Tools.Pencil:
                    new Line(P, Tools.Pencil, Size, Color, Start, End, G).Draw();
                    return;
                case Tools.Eraser:
                    new Eraser(P, Tools.Eraser, Size, Color, Start, End, G).Draw();
                    return;
                case Tools.Fill:
                    new Fill(P, Tools.Fill, 1, Color, Start, End, G, Bmp).Draw();
                    return;
                case Tools.Line:
                    new Line(P, Tools.Line, Size, Color, Start, End, G).Draw();
                    return;
                case Tools.Rectangle:
                    new Rectangle(P, Tools.Rectangle, Size, Color, Start, End, G).Draw();
                    return;
                case Tools.Ellipse:
                    new Ellipse(P, Tools.Ellipse, Size, Color, Start, End, G).Draw();
                    return;
                case Tools.Triangle:
                case Tools.RightTriangle:
                    new Triangle(P, Tool, Size, Color, Start, End, G).Draw();
                    return;
                case Tools.Pentagon:
                    new Pentagon(P, Tools.Pentagon, Size, Color, Start, End, G).Draw();
                    return;
                case Tools.Hexagon:
                    new Hexagon(P, Tools.Hexagon, Size, Color, Start, End, G).Draw();
                    return;
                case Tools.Star:
                    new Star(P, Tools.Star, Size, Color, Start, End, G).Draw();
                    return;
                case Tools.Romb:
                    new Romb(P, Tools.Romb, Size, Color, Start, End, G).Draw();
                    return;
                case Tools.Trapez:
                    new Trapez(P, Tools.Trapez, Size, Color, Start, End, G).Draw();
                    return;
                default:
                    throw new NotImplementedException();


            }
        }





    }
}
