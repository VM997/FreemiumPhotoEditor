using Editor.Core.History;
using Editor.Core.Models;
using FluentAssertions;
using Xunit;

namespace Editor.Core.Tests;

public class HistoryTests
{
    private static ImageState Img(int w) => new(new byte[] { 1, 2, 3 }, w, 1);

    [Fact]
    public void UndoRedo_Works()
    {
        var h = new HistoryManager(Img(1));
        h.Push(Img(2));
        h.Push(Img(3));

        h.CanUndo.Should().BeTrue();
        h.Undo().Width.Should().Be(2);
        h.Undo().Width.Should().Be(1);
        h.CanUndo.Should().BeFalse();

        h.CanRedo.Should().BeTrue();
        h.Redo().Width.Should().Be(2);
        h.Redo().Width.Should().Be(3);
    }
}
