﻿using Rect = System.Drawing.Rectangle;
namespace FinalPaint
{
    internal class Selection : ToolControl
    {
        public Selection(Pen p, Tools tool, int size, Color color, Point start, Point end, Graphics g) : base(p, tool, size, color, start, end, g)
        {

        }

        public new void Draw()
        {

            Point StartPosition = new Point(Math.Min(Start.X, End.X), Math.Min(Start.Y, End.Y));
            Size size = new Size(Math.Abs(Start.X - End.X), Math.Abs(Start.Y - End.Y));
 

            Rect rect  = new Rect(StartPosition, size);
            P.Color = Color.FromArgb(145, 200, 228);
            P.Width = 2;
            G.DrawRectangle(P, rect);
        }
    }
}