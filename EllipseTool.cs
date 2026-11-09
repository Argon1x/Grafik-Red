using Avalonia;
using Avalonia.Media;

namespace GrafikRed;

public class EllipseTool : Tool
{
    public EllipseTool() : base()
    {
    }

    public EllipseTool(Color color, double thickness) : base(color, thickness)
    {
    }

    public override void OnMouseDown(Point point, DrawingCanvas canvas)
    {
        IsDrawing = true;
        StartPoint = point;
        CurrentPoint = point;
    }

    public override void OnMouseMove(Point point, DrawingCanvas canvas)
    {
        if (IsDrawing)
        {
            CurrentPoint = point;
        }
    }

    public override void OnMouseUp(Point point, DrawingCanvas canvas)
    {
        if (!IsDrawing) return;

        CurrentPoint = point;
        canvas.AddShape(new EllipseShape(GetRect(StartPoint, CurrentPoint), Color, Thickness));
        IsDrawing = false;
    }

    public override void DrawPreview(DrawingContext context)
    {
        if (!IsDrawing) return;

        var pen = new Pen(new SolidColorBrush(Color), Thickness);
        Rect r = GetRect(StartPoint, CurrentPoint);
        Point center = new Point(r.X + r.Width / 2, r.Y + r.Height / 2);
        context.DrawEllipse(null, pen, center, r.Width / 2, r.Height / 2);
    }
}
