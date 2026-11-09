using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace GrafikRed;

public class ImageSaver
{
    public async Task<bool> SaveAsync(DrawingCanvas canvas, Window window)
    {
        var topLevel = TopLevel.GetTopLevel(window);
        if (topLevel == null) return false;

        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Сохранить изображение",
            DefaultExtension = "png",
            FileTypeChoices = new[]
            {
                new FilePickerFileType("PNG Image (*.png)") { Patterns = new[] { "*.png" } },
                new FilePickerFileType("JPEG Image (*.jpg)") { Patterns = new[] { "*.jpg", "*.jpeg" } }
            }
        });

        if (file != null)
        {
            string path = file.Path.LocalPath;
            canvas.SaveToFile(path);
            return true;
        }

        return false;
    }
}
