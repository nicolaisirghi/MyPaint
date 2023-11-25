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
            throw new NotImplementedException();
        }





    }
}
