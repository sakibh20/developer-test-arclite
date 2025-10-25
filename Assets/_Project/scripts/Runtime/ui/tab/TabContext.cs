using UnityEngine;

public class TabContext
{
    public Renderer TargetRenderer { get; }

    public TabContext(Renderer renderer)
    {
        TargetRenderer = renderer;
    }
}