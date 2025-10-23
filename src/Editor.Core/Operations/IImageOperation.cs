using Editor.Core.Models;

namespace Editor.Core.Operations;

public interface IImageOperation
{
    string Name { get; }
    ImageState Apply(ImageState input);
}
