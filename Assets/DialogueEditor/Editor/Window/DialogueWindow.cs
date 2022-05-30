using EditorInput.Mouse;
using UnityEditor;
using UnityEngine;

public class DialogueWindow : EditorWindow
{
    private Updates _updates;
    private NodeGenerator _nodeGenerator;

    private IRect _editNodePanelRect;
    private IPosition _editNodePanelPosition;
    
    private const int NodeGeneratorWidth = 300;
    private const int CreateNodeButtonHeight = 30;
    
    private const int EditNodePanelWidth = 300;
    private const int EditNodePanelHeight = 300;
    private const float EditNodePanelBorderWidth = 2;

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
         
         InitNodeGenerator();
         InitEditNodePanel();
     }

     private void OnGUI()
     {
         _updates.Update();
     }

     private void InitNodeGenerator()
     {
         var nodeDraggerPosition = new MousePosition();
         var screenSize = new ScreenSize();
         
         var panelTexture = new CustomSimpleTexture2D(Color.black, 1, 1);
         var nodeTexture = new CustomSimpleTexture2D(Color.blue, 1, 1);
         var dragTexture = new CustomSimpleTexture2D(Color.green, 1, 1);
         
         var panelSize = new OffsetPosition(new YVector(screenSize),
             new PositionAdapter(new Vector2(NodeGeneratorWidth, 0)));

         var panelPosition = new OffsetPosition(
             new XVector(screenSize), 
             new PositionAdapter(-new Vector2(NodeGeneratorWidth, 0)));
         
         var panelRect = new CustomRect(panelPosition, panelSize);

         var createNodeButtonSize =
             new PositionAdapter(new Vector2(panelRect.Get().width, CreateNodeButtonHeight));
         
         var createNodeButtonRect = new CustomRect(panelPosition, createNodeButtonSize);

         var nodeFactory = new SpeechNodeFactory(nodeTexture, dragTexture);
         
         _nodeGenerator = 
             new NodeGenerator(nodeFactory, 
                 _updates,
                 panelTexture,
                 panelRect,
                 createNodeButtonRect,
                 nodeDraggerPosition,
                 _editNodePanelRect,
                 _editNodePanelPosition,
                 new MouseDrag());
         
         _updates.Add(createNodeButtonRect, panelRect, _nodeGenerator);
     }

     private void InitEditNodePanel()
     {
         var color = Color.Lerp(Color.blue, Color.white, 0.5f);
         var texture = new CustomBorderTexture2D(color, EditNodePanelBorderWidth, EditNodePanelWidth, EditNodePanelHeight);
         
         var editNodePanel = new EditNodePanel(texture, _editNodePanelRect);
         _updates.Add(editNodePanel);
     }
}
