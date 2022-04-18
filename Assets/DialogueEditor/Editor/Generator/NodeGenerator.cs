﻿using UnityEditor;
using UnityEngine;

public class NodeGenerator : IUpdate
{
    private readonly ReferenceRect _panelRect;
    private readonly ReferenceRect _createButtonRect;
    private readonly Texture2D _texture;
    private readonly Updates _updates;
    
    private readonly IFactory<INode> _nodeFactory;

    public NodeGenerator(IFactory<INode> nodeFactory, Updates updates, CustomSimpleTexture2D simpleTexture2D, 
        ReferenceRect panelRect, ReferenceRect createButtonRect)
    {
        _nodeFactory = nodeFactory;
        _updates = updates;
        _texture = simpleTexture2D.Get();
        _panelRect = panelRect;
        _createButtonRect = createButtonRect;
    }

    public void Update()
    {
        GUI.DrawTexture(_panelRect.Get(), _texture);

        EditorGUILayout.BeginVertical();

        if (GUI.Button(_createButtonRect.Get(), "Create node"))
        {
            var node = _nodeFactory.Create(new ReferenceRect(new Rect(300,300,50,50)));
            _updates.Add(node);
        }
        
        EditorGUILayout.EndVertical();
    }
}