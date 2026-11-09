using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;

namespace GrafikRed;

public interface IDrawableShape
{
    void Draw(DrawingContext context);
}

public class LineShape : IDrawableShape
{
    public Point Start { get; }
    public Point End { get; }
    public Color Color { get; }
    public double Thickness { get; }

    public LineShape(Point start, Point end, Color color, double thickness)
    {
        Start = start;
        End = end;
        Color = color;
        Thickness = thickness;
    }

    public void Draw(DrawingContext context)
    {
        var pen = new Pen(new SolidColorBrush(Color), Thickness, lineCap: PenLineCap.Round);
        context.DrawLine(pen, Start, End);
    }
}

public class RectangleShape : IDrawableShape
{
    public Rect Rectangle { get; }
    public Color Color { get; }
    public double Thickness { get; }

    public RectangleShape(Rect rectangle, Color color, double thickness)
    {
        Rectangle = rectangle;
        Color = color;
        Thickness = thickness;
    }

    public void Draw(DrawingContext context)
    {
        var pen = new Pen(new SolidColorBrush(Color), Thickness);
        context.DrawRectangle(null, pen, Rectangle);
    }
}

public class EllipseShape : IDrawableShape
{
    public Rect Bounds { get; }
    public Color Color { get; }
    public double Thickness { get; }

    public EllipseShape(Rect bounds, Color color, double thickness)
    {
        Bounds = bounds;
        Color = color;
        Thickness = thickness;
    }

    public void Draw(DrawingContext context)
    {
        var pen = new Pen(new SolidColorBrush(Color), Thickness);
        Point center = new Point(Bounds.X + Bounds.Width / 2, Bounds.Y + Bounds.Height / 2);
        context.DrawEllipse(null, pen, center, Bounds.Width / 2, Bounds.Height / 2);
    }
}

public class PencilShape : IDrawableShape
{
    public List<Point> Points { get; } = new();
    public Color Color { get; }
    public double Thickness { get; }

    public PencilShape(Color color, double thickness)
    {
        Color = color;
        Thickness = thickness;
    }

    public void Draw(DrawingContext context)
    {
        if (Points.Count < 2) return;

        var pen = new Pen(new SolidColorBrush(Color), Thickness, lineCap: PenLineCap.Round, lineJoin: PenLineJoin.Round);
        for (int i = 0; i < Points.Count - 1; i++)
        {
            context.DrawLine(pen, Points[i], Points[i + 1]);
        }
    }
}
