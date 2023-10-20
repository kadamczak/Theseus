using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Theseus.WPF.Code.Views.HelperClasses
{
    public class LineDrawer
    {
        private readonly Canvas _canvas;

        public LineDrawer(Canvas canvas)
        {
            _canvas = canvas;
        }

        //x1,y1---->x2,y1
        //|
        //|
        //V
        //x1,y2     x2,y2
        public void DrawLine(float x1, float y1, float x2, float y2, System.Windows.Media.Color? color = null, float strokeThickness = 2, string? tag = null)
        {
            DrawLine(new PointF(x1, y1), new PointF(x2, y2), color, strokeThickness, tag);
        }

        public void DrawLine(PointF start, PointF end, System.Windows.Media.Color? color = null, float strokeThickness = 2, string? tag = null)
        {
            Line line = new Line()
            {
                X1 = start.X,
                Y1 = start.Y,
                X2 = end.X,
                Y2 = end.Y
            };

            line.StrokeDashCap = PenLineCap.Round;
            line.StrokeStartLineCap = PenLineCap.Round;
            line.StrokeEndLineCap = PenLineCap.Round;

            if (tag is not null)
                line.Tag = tag;

            line.StrokeThickness = strokeThickness;
            line.Stroke = new SolidColorBrush(color ?? Colors.Black);

            this._canvas.Children.Add(line);
        }
    }
}