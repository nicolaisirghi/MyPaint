namespace FinalPaint;

internal class Typography : ToolControl
{
    private readonly Control control;

    private readonly Font Font;
    private string text;


    public Typography(Pen p, Tools tool, int size, Color color, Point start, Point end, Graphics g, Control control,
        Font font) : base(p, tool, size, color, start, end, g)
    {
        Font = font;

        this.control = control;
    }

    public new void Draw()
    {
        var textBox = new TextBox();
        textBox.Location = Start;
        textBox.Font = Font;

        control.Controls.Add(textBox);

        textBox.TextChanged += textBox_TextChanged;
        textBox.Leave += textBox_Leave;
    }

    private void textBox_Leave(object sender, EventArgs e)
    {
        var textBox = (TextBox)sender;
        textBox.Visible = false;
        textBox.Dispose();

        G.DrawString(text, Font, new SolidBrush(Color), Start);
    }

    private void textBox_TextChanged(object sender, EventArgs e)
    {
        var textBox = (TextBox)sender;
        textBox.Width = TextRenderer.MeasureText(textBox.Text, Font).Width;
        textBox.Height = TextRenderer.MeasureText(textBox.Text, Font).Height;
        text = textBox.Text;
    }
}