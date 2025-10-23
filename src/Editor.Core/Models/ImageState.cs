namespace Editor.Core.Models;

public class ImageState
{
    public byte[] Pixels { get; }
    public int Width { get; }
    public int Height { get; }

    public ImageState(byte[] pixels, int width, int height)
    {
        Pixels = pixels;
        Width = width;
        Height = height;
    }
}
