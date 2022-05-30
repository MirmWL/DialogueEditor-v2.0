using UnityEditor;
using UnityEngine;

public class NodeGenerator : IUpdate
{
    private readonly IRect _panelRect;
    private readonly IRect _createButtonRect;
    private readonly INodeFactory _nodeFactory;
    
    private readonly Texture2D _texture;

    public NodeGenerator(
        INodeFactory nodeFactory, 
        ITexture2D texture, 
        IRect panelRect, 
        IRect createButtonRect)
    {
        _nodeFactory = nodeFactory;
        _texture = texture.Get();
        _panelRect = panelRect;
        _createButtonRect = createButtonRect;
    }
    
    public void Update()
    {
        GUI.DrawTexture(_panelRect.Get(), _texture);
        
        EditorGUILayout.BeginVertical();

        if (GUI.Button(_createButtonRect.Get(), "Create node"))
            _nodeFactory.Create();

        EditorGUILayout.EndVertical();
    }
}