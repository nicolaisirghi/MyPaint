namespace FinalPaint
{
    internal class Fill:ToolControl
    {
     public Fill(Pen p, Tools tool, int size, Color color, Point start,Point end ,Graphics g,Bitmap bmp) : base(p,tool, size, color, start,end,g,bmp)
        {

        }

        public new void Draw()
        {
            FillObject(Bmp, Start.X,Start.Y, Color);
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
