using UnityEngine;

public interface INode : IUpdate
{
    Rect Rect { get; }

}