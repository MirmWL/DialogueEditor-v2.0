using UnityEngine;

public interface INode : IUpdate
{
    Rect Rect { get; }
    void Pin();
    void UnPin();
}