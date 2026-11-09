using Avalonia;
using Avalonia.Media;

namespace GrafikRed;

public class LineTool : Tool
{
    public LineTool() : base()
    {
    }

    public LineTool(Color color, double thickness) : base(color, thickness)
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
        canvas.AddShape(new LineShape(StartPoint, CurrentPoint, Color, Thickness));
        IsDrawing = false;
    }

    public override void DrawPreview(DrawingContext context)
    {
        if (!IsDrawing) return;

        var pen = new Pen(new SolidColorBrush(Color), Thickness, lineCap: PenLineCap.Round);
        context.DrawLine(pen, StartPoint, CurrentPoint);
    }
}
