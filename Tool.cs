using System;
using Avalonia;
using Avalonia.Media;

namespace GrafikRed;

public abstract class Tool
{
    public Color Color { get; set; } = Colors.Black;
    public double Thickness { get; set; } = 2;
    public bool IsDrawing { get; protected set; }

    protected Point StartPoint;
    protected Point CurrentPoint;

    public Tool()
    {
        Color = Colors.Black;
        Thickness = 2;
        IsDrawing = false;
    }

    public Tool(Color color, double thickness)
    {
        Color = color;
        Thickness = thickness;
        IsDrawing = false;
    }

    public abstract void OnMouseDown(Point point, DrawingCanvas canvas);
    public abstract void OnMouseMove(Point point, DrawingCanvas canvas);
    public abstract void OnMouseUp(Point point, DrawingCanvas canvas);

    public virtual void DrawPreview(DrawingContext context)
    {
    }

    protected Rect GetRect(Point p1, Point p2)
    {
        double x = Math.Min(p1.X, p2.X);
        double y = Math.Min(p1.Y, p2.Y);
        double width = Math.Abs(p1.X - p2.X);
        double height = Math.Abs(p1.Y - p2.Y);

        return new Rect(x, y, width, height);
    }
}
