
namespace FinalPaint
{
    internal class Utils
    {
        static public ToolControl GetTool(Pen P, Tools Tool, int Size, Color Color, Point Start, Point End, Graphics G,
            Bitmap Bmp = null)
        {
            switch (Tool)
            {
                case Tools.Pencil:
                    return new Line(P, Tool, Size, Color, Start, End, G);
                case Tools.Eraser:
                    return new Eraser(P, Tool, Size, Color, Start, End, G);
                case Tools.Fill:
                    return new Fill(P, Tool, 1, Color, Start, End, G, Bmp);
                case Tools.Line:
                    return new Line(P, Tool, Size, Color, Start, End, G);
                case Tools.Rectangle:
                    return new Rectangle(P, Tool, Size, Color, Start, End, G);
                case Tools.Ellipse:
                    return new Ellipse(P, Tool, Size, Color, Start, End, G);
                case Tools.Triangle:
                case Tools.RightTriangle:
                    return new Triangle(P, Tool, Size, Color, Start, End, G);
                case Tools.Pentagon:
                    return new Pentagon(P, Tool, Size, Color, Start, End, G);
                case Tools.Hexagon:
                    return new Hexagon(P, Tool, Size, Color, Start, End, G);
                case Tools.Star:
                    return new Star(P, Tool, Size, Color, Start, End, G);
                case Tools.Romb:
                    return new Romb(P, Tool, Size, Color, Start, End, G);
                case Tools.Trapez:
                    return new Trapez(P, Tool, Size, Color, Start, End, G);
                case Tools.Selection:
                    return new Selection(P, Tool, Size, Color, Start, End, G);
                default:
                    return new ToolControl(P, Tools.Default, Size, Color, Start, End, G);
            }
        }
    }
}

