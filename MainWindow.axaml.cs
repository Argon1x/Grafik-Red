using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace GrafikRed;

public partial class MainWindow : Window
{
    private readonly ToolManager _toolManager;
    private readonly ImageSaver _imageSaver;

    public MainWindow()
    {
        InitializeComponent();

        _toolManager = new ToolManager();
        _imageSaver = new ImageSaver();

        var canvas = this.FindControl<DrawingCanvas>("Canvas");
        if (canvas != null)
        {
            canvas.ToolManager = _toolManager;
        }

        var pencilBtn = this.FindControl<Button>("PencilButton");
        var lineBtn = this.FindControl<Button>("LineButton");
        var rectBtn = this.FindControl<Button>("RectangleButton");
        var ellipseBtn = this.FindControl<Button>("EllipseButton");
        var colorCombo = this.FindControl<ComboBox>("ColorComboBox");
        var thicknessInput = this.FindControl<NumericUpDown>("ThicknessInput");
        var clearBtn = this.FindControl<Button>("ClearButton");
        var saveBtn = this.FindControl<Button>("SaveButton");

        if (pencilBtn != null) pencilBtn.Click += (s, e) => { _toolManager.SetTool("Pencil"); UpdateToolButtons(); };
        if (lineBtn != null) lineBtn.Click += (s, e) => { _toolManager.SetTool("Line"); UpdateToolButtons(); };
        if (rectBtn != null) rectBtn.Click += (s, e) => { _toolManager.SetTool("Rectangle"); UpdateToolButtons(); };
        if (ellipseBtn != null) ellipseBtn.Click += (s, e) => { _toolManager.SetTool("Ellipse"); UpdateToolButtons(); };

        if (colorCombo != null) colorCombo.SelectionChanged += OnColorChanged;
        if (thicknessInput != null) thicknessInput.ValueChanged += OnThicknessChanged;
        if (clearBtn != null) clearBtn.Click += (s, e) => canvas?.Clear();
        if (saveBtn != null) saveBtn.Click += async (s, e) => { if (canvas != null) await _imageSaver.SaveAsync(canvas, this); };

        UpdateToolButtons();
    }

    private void UpdateToolButtons()
    {
        var pencilBtn = this.FindControl<Button>("PencilButton");
        var lineBtn = this.FindControl<Button>("LineButton");
        var rectBtn = this.FindControl<Button>("RectangleButton");
        var ellipseBtn = this.FindControl<Button>("EllipseButton");

        if (pencilBtn != null) pencilBtn.Background = _toolManager.CurrentToolName == "Pencil" ? Brushes.LightBlue : null;
        if (lineBtn != null) lineBtn.Background = _toolManager.CurrentToolName == "Line" ? Brushes.LightBlue : null;
        if (rectBtn != null) rectBtn.Background = _toolManager.CurrentToolName == "Rectangle" ? Brushes.LightBlue : null;
        if (ellipseBtn != null) ellipseBtn.Background = _toolManager.CurrentToolName == "Ellipse" ? Brushes.LightBlue : null;
    }

    private void OnColorChanged(object? sender, SelectionChangedEventArgs e)
    {
        var combo = sender as ComboBox;
        if (combo?.SelectedItem is ComboBoxItem item && item.Tag is string tag)
        {
            Color color = tag switch
            {
                "Red" => Colors.Red,
                "Green" => Colors.Green,
                "Blue" => Colors.Blue,
                "Yellow" => Colors.Yellow,
                "Orange" => Colors.Orange,
                _ => Colors.Black
            };

            _toolManager.SetColor(color);

            var preview = this.FindControl<Border>("ColorPreviewBorder");
            if (preview != null)
            {
                preview.Background = new SolidColorBrush(color);
            }
        }
    }

    private void OnThicknessChanged(object? sender, NumericUpDownValueChangedEventArgs e)
    {
        if (e.NewValue.HasValue)
        {
            _toolManager.SetThickness((double)e.NewValue.Value);
        }
    }
}
