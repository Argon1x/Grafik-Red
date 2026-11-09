using System.Collections.Generic;
using Avalonia.Media;

namespace GrafikRed;

public class ToolManager
{
    public Tool CurrentTool { get; private set; }
    public string CurrentToolName { get; private set; } = "Pencil";
    public Color CurrentColor { get; private set; } = Colors.Black;
    public double CurrentThickness { get; private set; } = 2;

    private readonly Dictionary<string, Tool> _tools = new();

    public ToolManager()
    {
        _tools["Pencil"] = new PencilTool(CurrentColor, CurrentThickness);
        _tools["Line"] = new LineTool(CurrentColor, CurrentThickness);
        _tools["Rectangle"] = new RectangleTool(CurrentColor, CurrentThickness);
        _tools["Ellipse"] = new EllipseTool(CurrentColor, CurrentThickness);

        CurrentTool = _tools["Pencil"];
    }

    public void SetTool(string toolName)
    {
        if (_tools.TryGetValue(toolName, out Tool? tool))
        {
            CurrentTool = tool;
            CurrentToolName = toolName;
            CurrentTool.Color = CurrentColor;
            CurrentTool.Thickness = CurrentThickness;
        }
    }

    public void SetColor(Color color)
    {
        CurrentColor = color;
        if (CurrentTool != null)
        {
            CurrentTool.Color = color;
        }
    }

    public void SetThickness(double thickness)
    {
        CurrentThickness = thickness;
        if (CurrentTool != null)
        {
            CurrentTool.Thickness = thickness;
        }
    }
}
