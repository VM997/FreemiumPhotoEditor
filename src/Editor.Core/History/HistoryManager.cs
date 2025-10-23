using Editor.Core.Models;
using System.Collections.Generic;
namespace Editor.Core.History;

public class HistoryManager
{
    private readonly Stack<ImageState> _undo = new();
    private readonly Stack<ImageState> _redo = new();
    public int Max = 10;

    public ImageState Current { get; private set; }

    public HistoryManager(ImageState initial)
    {
        Current = initial;
    }

    public void Push(ImageState next)
    {
        _undo.Push(Current);
        Current = next;
        _redo.Clear();
        Trim();
    }

    public bool CanUndo => _undo.Count > 0;
    public bool CanRedo => _redo.Count > 0;

    public ImageState Undo()
    {
        if (!CanUndo) return Current;
        _redo.Push(Current);
        Current = _undo.Pop();
        return Current;
    }

    public ImageState Redo()
    {
        if (!CanRedo) return Current;
        _undo.Push(Current);
        Current = _redo.Pop();
        return Current;
    }

    private void Trim()
    {
        while (_undo.Count > Max) _undo.RemoveBottom();
    }
}

static class StackExtensions
{
    public static void RemoveBottom<T>(this Stack<T> stack)
    {
        // Simple trim: rebuild stack without bottom-most element (rare path).
        var arr = stack.ToArray(); // top→bottom
        stack.Clear();
        for (int i = 0; i < arr.Length - 1; i++)
            stack.Push(arr[i]);
    }
}
