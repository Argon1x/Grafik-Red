using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace GrafikRed;

public class DrawingCanvas : Control
{
    private readonly List<IDrawableShape> _shapes = new();
    public ToolManager ToolManager { get; set; } = null!;

    public DrawingCanvas()
    {
        ClipToBounds = true;
    }

    public void AddShape(IDrawableShape shape)
    {
        _shapes.Add(shape);
        InvalidateVisual();
    }

    public void Clear()
    {
        _shapes.Clear();
        InvalidateVisual();
    }

    public void SaveToFile(string filePath)
    {
        int width = Math.Max(1, (int)Bounds.Width);
        int height = Math.Max(1, (int)Bounds.Height);

        var rtb = new RenderTargetBitmap(new PixelSize(width, height), new Vector(96, 96));
        using (var ctx = rtb.CreateDrawingContext())
        {
            ctx.DrawRectangle(Brushes.White, null, new Rect(0, 0, width, height));
            foreach (var shape in _shapes)
            {
                shape.Draw(ctx);
            }
        }
        using var stream = System.IO.File.Create(filePath);
        rtb.Save(stream);
    }

    private Point Clamp(Point p)
    {
        double x = Math.Max(0, Math.Min(p.X, Bounds.Width - 1));
        double y = Math.Max(0, Math.Min(p.Y, Bounds.Height - 1));
        return new Point(x, y);
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        var props = e.GetCurrentPoint(this).Properties;
        if (props.IsLeftButtonPressed && ToolManager?.CurrentTool != null)
        {
            Point p = Clamp(e.GetPosition(this));
            ToolManager.CurrentTool.OnMouseDown(p, this);
            InvalidateVisual();
        }
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        base.OnPointerMoved(e);
        if (ToolManager?.CurrentTool != null && ToolManager.CurrentTool.IsDrawing)
        {
            Point p = Clamp(e.GetPosition(this));
            ToolManager.CurrentTool.OnMouseMove(p, this);
            InvalidateVisual();
        }
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);
        if (ToolManager?.CurrentTool != null && ToolManager.CurrentTool.IsDrawing)
        {
            Point p = Clamp(e.GetPosition(this));
            ToolManager.CurrentTool.OnMouseUp(p, this);
            InvalidateVisual();
        }
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        context.DrawRectangle(Brushes.White, null, new Rect(0, 0, Bounds.Width, Bounds.Height));

        foreach (var shape in _shapes)
        {
            shape.Draw(context);
        }

        if (ToolManager?.CurrentTool != null && ToolManager.CurrentTool.IsDrawing)
        {
            ToolManager.CurrentTool.DrawPreview(context);
        }
    }
}
