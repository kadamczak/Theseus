using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Theseus.WPF.Code.Views.HelperClasses
{
    public class LineDrawer
    {
        //x1,y1---->x2,y1
        //|
        //|
        //V
        //x1,y2     x2,y2
        public void DrawLine(Canvas canvas, int x1, int y1, int x2, int y2, Color? color = null, int strokeThickness = 2, string? tag = null)
        {
            DrawLine(canvas, (x1, y1), (x2, y2), color, strokeThickness, tag);
        }

        public void DrawLine(Canvas canvas, (int x, int y) start, (int x, int y) end, Color? color = null, int strokeThickness = 2, string? tag = null)
        {
            Line line = new Line()
            {
                X1 = start.x,
                Y1 = start.y,
                X2 = end.x,
                Y2 = end.y
            };

            line.StrokeDashCap = PenLineCap.Round;
            line.StrokeStartLineCap = PenLineCap.Round;
            line.StrokeEndLineCap = PenLineCap.Round;

            if (tag is not null)
                line.Tag = tag;

            line.StrokeThickness = strokeThickness;
            line.Stroke = new SolidColorBrush(color ?? Colors.Black);

            canvas.Children.Add(line);
        }
    }
}