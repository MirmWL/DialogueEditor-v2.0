using System.Collections.Generic;
using UnityEditor;

public interface IProperties
{
    IEnumerable<SerializedProperty> Get();
}