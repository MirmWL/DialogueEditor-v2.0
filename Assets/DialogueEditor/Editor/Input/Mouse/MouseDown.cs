using UnityEngine;

namespace EditorInput.Mouse
{
    public class MouseDown : IInput
    {
        public bool HasInput()
        {
            return Event.current.type == EventType.MouseDown;
        }
    }
}