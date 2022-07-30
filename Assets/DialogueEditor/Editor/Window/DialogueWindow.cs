﻿using EditorInput;
using EditorInput.Mouse;
using UnityEditor;
using UnityEngine;

public class DialogueWindow : EditorWindow
{
    private Updates _updates;

    private IRect _editNodePanelRect;
    private IPosition _editNodePanelPosition;
    
    private const int NodeGeneratorWidth = 300;
    private const int CreateNodeButtonHeight = 30;
    
    private const int EditNodePanelWidth = 300;
    private const int EditNodePanelHeight = 300;
    private const float EditNodePanelBorderWidth = 2;
    
    private const float DragPinnedWidth = 25;
    
    private const float NodeHeight = 100;
    private const float NodeWidth = 100;
    private const float NodeDragWidth = 25;

    [MenuItem("Window/Dialogue Editor")]
     public static void Open()
     {
         var window = GetWindow<DialogueWindow>();
         window.Show();
         window.minSize = new Vector2(600, 300);
     }

     private void OnEnable()
     {
         var editNodePanelSize = new PositionAdapter(new Vector2(EditNodePanelWidth, EditNodePanelHeight));
         _editNodePanelPosition = new PositionAdapter(Vector2.zero);
         _editNodePanelRect = new CachedRect(_editNodePanelPosition, editNodePanelSize);

         _updates = new Updates();
         
         InitNodeFactory();
         InitEditNodePanel();
     }

     private void OnGUI()
     {
         _updates.Update();
     }

     private void InitNodeFactory()
     {
         var nodeTexture = new CustomSimpleTexture2D(Color.blue, 1, 1);
         var dragTexture = new CustomSimpleTexture2D(Color.green, 1, 1);

         var drag = new MouseDrag();
         var click = new MouseClick();
         var screenSize = new ScreenSize();

         var createConnectionButtonSize = new PositionAdapter(new Vector2(25, 25));
         
         var mouse = new MousePosition();
         
         var dragUnpinnedSize = new PositionAdapter(new Vector2(NodeDragWidth, NodeHeight));
         var dragUnpinnedPosition = new OffsetPosition(mouse, new PositionAdapter(-dragUnpinnedSize.Get() / 2));

         var dragPinnedSize = new PositionAdapter(new Vector2(DragPinnedWidth, _editNodePanelRect.Get().size.y));
         
         var dragPinnedPosition = new XVector(new OffsetPosition(_editNodePanelPosition,
             new PositionAdapter(_editNodePanelRect.Get().size)));
         
         var dragPinnedRect = new CachedRect(dragPinnedPosition, dragPinnedSize);

         var unpinnedPosition = new OffsetPosition(dragUnpinnedPosition, new XVector(dragUnpinnedSize));
         var unpinnedSize = new PositionAdapter(new Vector2(NodeHeight, NodeWidth));

         var createConnectionButtonPinnedPosition = new OffsetPosition(
             _editNodePanelPosition, 
             new XVector(new PositionAdapter(_editNodePanelRect.Get().size)));

         var createConnectionButtonUnpinnedPosition =
             new OffsetPosition(unpinnedPosition, new XVector(unpinnedSize));
         
         var panelSize = new OffsetPosition(new YVector(screenSize),
             new PositionAdapter(new Vector2(NodeGeneratorWidth, 0)));

         var panelPosition = new OffsetPosition(
             new XVector(screenSize), 
             new PositionAdapter(-new Vector2(NodeGeneratorWidth, 0)));
         
         var panelRect = new CachedRect(panelPosition, panelSize);
         
         var createNodeButtonSize =
             new PositionAdapter(new Vector2(panelRect.Get().width, CreateNodeButtonHeight));
         
         var createNodeButtonRect = new CachedRect(panelPosition, createNodeButtonSize);

         var middleCenterStyle = new MiddleCenterAlignedStyle(new DefaultStyle());
         
         var createButton = new CustomButton(
             createNodeButtonRect,
             new CustomSimpleTexture2D(Color.cyan, 1, 1),
             middleCenterStyle, "Create node");
         
         var createButtonClick = new Predicates(
             new InputToPredicateAdapter(click),
             new InRect(createNodeButtonRect, mouse));
         
         var createConnectionButtonTexture = new CustomSimpleTexture2D(Color.cyan, 1, 1);

         var nodeFactory = new NodeFactory(
             nodeTexture,
             dragTexture,
             createConnectionButtonTexture,
             mouse,
             _editNodePanelRect,
             drag,
             click,
             dragUnpinnedSize,
             unpinnedSize,
             createConnectionButtonSize,
             dragUnpinnedPosition,
             dragPinnedRect,
             unpinnedPosition,
             createConnectionButtonPinnedPosition,
             createConnectionButtonUnpinnedPosition,
             middleCenterStyle,
             _updates,
             "+"
         );

         var factoryRules = new NodeFactoryBusinessRules(createButtonClick, nodeFactory);
         _updates.Add(createNodeButtonRect, createButton, factoryRules);
     }


     private void InitEditNodePanel()
     {
         var color = Color.Lerp(Color.blue, Color.white, 0.5f);
         var texture = new CustomBorderTexture2D(color, EditNodePanelBorderWidth, EditNodePanelWidth, EditNodePanelHeight);
         
         var editNodePanel = new EditNodePanel(texture, _editNodePanelRect);
         _updates.Add(editNodePanel);
     }
}
