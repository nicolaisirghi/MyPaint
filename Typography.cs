namespace FinalPaint
{
    internal class Typography : ToolControl
    {

        Font Font;
        Control control;
        string text;



        public Typography(Pen p, Tools tool, int size, Color color, Point start, Point end, Graphics g, Control control,Font font) : base(p, tool, size, color, start, end, g)
        {
            Font = font;       

            this.control = control;
        }

        public new void Draw()
        {
          
            TextBox textBox = new TextBox();
            textBox.Location = Start;
            textBox.Font = Font; 

            control.Controls.Add(textBox);

            textBox.TextChanged += new EventHandler(textBox_TextChanged);
            textBox.Leave += new EventHandler(textBox_Leave);
           
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Visible = false;
            textBox.Dispose();
            textBox = null;

            Label label = new Label();
            label.Location = Start;
            label.Text = text;
            label.Font = Font;
            label.AutoSize = true;
            control.Controls.Add(label);


        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Width = TextRenderer.MeasureText(textBox.Text, Font).Width;
            textBox.Height = TextRenderer.MeasureText(textBox.Text, Font).Height;
            text = textBox.Text;
        }
    }


}
