using Avalonia;
using Avalonia.Media;

namespace GrafikRed;

public class RectangleTool : Tool
{
    public RectangleTool() : base()
    {
    }

    public RectangleTool(Color color, double thickness) : base(color, thickness)
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
        canvas.AddShape(new RectangleShape(GetRect(StartPoint, CurrentPoint), Color, Thickness));
        IsDrawing = false;
    }

    public override void DrawPreview(DrawingContext context)
    {
        if (!IsDrawing) return;

        var pen = new Pen(new SolidColorBrush(Color), Thickness);
        context.DrawRectangle(null, pen, GetRect(StartPoint, CurrentPoint));
    }
}
