using UnityEngine;

namespace EditorInput.Mouse
{
    public class MouseUp : IInput
    {
        public bool HasInput()
        {
            return Event.current.type == EventType.MouseUp;
        }
    }
}