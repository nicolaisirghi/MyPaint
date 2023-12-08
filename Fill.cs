namespace FinalPaint
{
    internal class Fill:ToolControl
    {
     public Fill(Pen p, Tools tool, int size, Color color, Point start,Point end ,Graphics g,Bitmap bmp) : base(p,tool, size, color, start,end,g,bmp)
        {

        }

        public new void Draw()
        {
            FillObjectOptimized(Bmp, Start.X,Start.Y, Color);
        }


        public void FillObjectOptimized(Bitmap bmp, int x, int y, Color newColor)
        {
            if (bmp == null || bmp.Width <= 0 || bmp.Height <= 0)
            {
                return;
            }

            Color oldColor = bmp.GetPixel(x, y);

            if (oldColor.ToArgb() == newColor.ToArgb())
            {
                // The new color is the same as the old color, nothing to fill.
                return;
            }

            Stack<Point> stack = new Stack<Point>();
            stack.Push(new Point(x, y));
            bmp.SetPixel(x, y, newColor);

            int oldColorArgb = oldColor.ToArgb();
            int newColorArgb = newColor.ToArgb();

            while (stack.Count > 0)
            {
                Point p = stack.Pop();

                if (p.X > 0 && p.X < bmp.Width - 1 && p.Y > 0 && p.Y < bmp.Height - 1)
                {
                    ValidateOptimized(bmp, stack, p.X - 1, p.Y, oldColorArgb, newColorArgb);
                    ValidateOptimized(bmp, stack, p.X, p.Y - 1, oldColorArgb, newColorArgb);
                    ValidateOptimized(bmp, stack, p.X + 1, p.Y, oldColorArgb, newColorArgb);
                    ValidateOptimized(bmp, stack, p.X, p.Y + 1, oldColorArgb, newColorArgb);
                }
            }
        }

        private void ValidateOptimized(Bitmap bmp, Stack<Point> stack, int x, int y, int oldColorArgb, int newColorArgb)
        {
            Color cx = bmp.GetPixel(x, y);
            if (cx.ToArgb() == oldColorArgb)
            {
                bmp.SetPixel(x, y, Color.FromArgb(newColorArgb));
                stack.Push(new Point(x, y));
            }
        }




        private void Validate(Bitmap bmp, Stack<Point> stack, int x, int y, Color oldColor, Color newColor)
        {
            Color cx = bmp.GetPixel(x, y);
            if (cx == oldColor)
            {
                bmp.SetPixel(x, y, newColor);
                stack.Push(new Point(x, y));
            }

        }

        public void FillObject(Bitmap bmp, int x, int y, Color newColor)
        {
            if(bmp == null)
            {
                return;
            }

            try
            {
                Color oldColor = bmp.GetPixel(x, y);
                Stack<Point> stack = new Stack<Point>();
                stack.Push(new Point(x, y));
                bmp.SetPixel(x, y, newColor);


                if(newColor.ToArgb() == oldColor.ToArgb())
                {
                    return;
                }   


                
                 
                while (stack.Count > 0)
                {
                    Point p = stack.Pop();

                    if (p.X > 0 && p.X < bmp.Width - 1 && p.Y > 0 && p.Y < bmp.Height - 1)
                    {
                        Validate(bmp, stack, p.X - 1, p.Y, oldColor, newColor);
                        Validate(bmp, stack, p.X, p.Y - 1, oldColor, newColor);
                        Validate(bmp, stack, p.X + 1, p.Y, oldColor, newColor);
                        Validate(bmp, stack, p.X, p.Y + 1, oldColor, newColor);
                    }
                }
            }
            catch (Exception)
            {

                return;
            }
           
        }
    }
}
