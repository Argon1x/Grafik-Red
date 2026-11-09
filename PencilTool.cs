using Avalonia;
using Avalonia.Media;

namespace GrafikRed;

public class PencilTool : Tool
{
    private PencilShape? _currentShape;

    public PencilTool() : base()
    {
    }

    public PencilTool(Color color, double thickness) : base(color, thickness)
    {
    }

    public override void OnMouseDown(Point point, DrawingCanvas canvas)
    {
        IsDrawing = true;
        _currentShape = new PencilShape(Color, Thickness);
        _currentShape.Points.Add(point);
        canvas.AddShape(_currentShape);
    }

    public override void OnMouseMove(Point point, DrawingCanvas canvas)
    {
        if (!IsDrawing || _currentShape == null) return;

        _currentShape.Points.Add(point);
    }

    public override void OnMouseUp(Point point, DrawingCanvas canvas)
    {
        if (!IsDrawing || _currentShape == null) return;

        _currentShape.Points.Add(point);
        _currentShape = null;
        IsDrawing = false;
    }

    public override void DrawPreview(DrawingContext context)
    {
    }
}
