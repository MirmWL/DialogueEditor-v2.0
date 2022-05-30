using EditorInput.Mouse;
using UnityEditor;
using UnityEngine;

public class DialogueWindow : EditorWindow
{
    private Updates _updates;
    private NodeGenerator _nodeGenerator;

    private IRect _editNodePanelRect;
    private IPosition _editNodePanelPosition;
    private SpeechNodeFactory _nodeFactory;
    
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
         _editNodePanelRect = new CustomRect(_editNodePanelPosition, editNodePanelSize);

         _updates = new Updates();
         
         InitNodeFactory();
         InitNodeGenerator();
         InitEditNodePanel();
     }

     private void OnGUI()
     {
         _updates.Update();
     }

     private void InitNodeGenerator()
     {
         var screenSize = new ScreenSize();
         
         var panelTexture = new CustomSimpleTexture2D(Color.black, 1, 1);
         
         var panelSize = new OffsetPosition(new YVector(screenSize),
             new PositionAdapter(new Vector2(NodeGeneratorWidth, 0)));

         var panelPosition = new OffsetPosition(
             new XVector(screenSize), 
             new PositionAdapter(-new Vector2(NodeGeneratorWidth, 0)));
         
         var panelRect = new CustomRect(panelPosition, panelSize);

         var createNodeButtonSize =
             new PositionAdapter(new Vector2(panelRect.Get().width, CreateNodeButtonHeight));
         
         var createNodeButtonRect = new CustomRect(panelPosition, createNodeButtonSize);
         
         _nodeGenerator = new NodeGenerator(_nodeFactory, panelTexture, panelRect, createNodeButtonRect); 
         
         _updates.Add(createNodeButtonRect, panelRect, _nodeGenerator);
     }


     private void InitNodeFactory()
     {
         var nodeTexture = new CustomSimpleTexture2D(Color.blue, 1, 1);
         var dragTexture = new CustomSimpleTexture2D(Color.green, 1, 1);

         var dragInput = new MouseDrag();
         var selectInput = new MouseClick();

         var createConnectionButtonSize = new PositionAdapter(new Vector2(25, 25));
         
         var nodeDraggerPosition = new MousePosition();
         
         var dragUnpinnedSize = new PositionAdapter(new Vector2(NodeDragWidth, NodeHeight));
         var dragUnpinnedPosition = new OffsetPosition(nodeDraggerPosition, new PositionAdapter(-dragUnpinnedSize.Get() / 2));

         var dragPinnedSize = new PositionAdapter(new Vector2(DragPinnedWidth, _editNodePanelRect.Get().size.y));
         var dragPinnedPosition = new XVector(new OffsetPosition(_editNodePanelPosition,
             new PositionAdapter(_editNodePanelRect.Get().size)));
         var dragPinnedRect = new CustomRect(dragPinnedPosition, dragPinnedSize);

         var unpinnedPosition = new OffsetPosition(dragUnpinnedPosition, new XVector(dragUnpinnedSize));
         var unpinnedSize = new PositionAdapter(new Vector2(NodeHeight, NodeWidth));

         var createConnectionButtonPinnedPosition = new OffsetPosition(_editNodePanelPosition, new XVector(new PositionAdapter(_editNodePanelRect.Get().size)));

         var createConnectionButtonUnpinnedPosition =
             new OffsetPosition(unpinnedPosition, new XVector(unpinnedSize));
         
         _nodeFactory = new SpeechNodeFactory(
             nodeTexture,
             dragTexture,
             nodeDraggerPosition,
             _editNodePanelRect, 
             dragInput,
             selectInput,
             dragUnpinnedSize,
             unpinnedSize,
             createConnectionButtonSize,
             dragUnpinnedPosition,
             dragPinnedRect,
             unpinnedPosition, 
             createConnectionButtonPinnedPosition,
             createConnectionButtonUnpinnedPosition,
             _updates
             );
     }


     private void InitEditNodePanel()
     {
         var color = Color.Lerp(Color.blue, Color.white, 0.5f);
         var texture = new CustomBorderTexture2D(color, EditNodePanelBorderWidth, EditNodePanelWidth, EditNodePanelHeight);
         
         var editNodePanel = new EditNodePanel(texture, _editNodePanelRect);
         _updates.Add(editNodePanel);
     }
}
