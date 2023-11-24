using  Rect  =  System.Drawing.Rectangle;
namespace FinalPaint
{
    internal class Rectangle:ToolControl
    {
     public Rectangle(Pen p, Tools tool, int size, Color color, Point start,Point end ,Graphics g) : base(p,tool, size, color, start,end,g)
        {

        }

        public new void Draw()
        {
            Rect rect = new Rect(Start.X, Start.Y, End.X - Start.X, End.Y - Start.Y);
            G.DrawRectangle(P, rect);
        }
    }
}
