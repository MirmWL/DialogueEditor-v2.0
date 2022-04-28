using UnityEditor;
using UnityEngine;

public class DialogueWindow : EditorWindow
{
    private PropertiesPanel _propertiesPanel;
    private Updates _updates;
    private NodeGenerator _nodeGenerator;
    
    private readonly ReferenceRect _editNodePanelRect = new ReferenceRect(
        new Rect(new Vector2(0,0), new Vector2(EditNodePanelWidth, EditNodePanelHeight)));
    
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
         var mousePosition = new MousePosition();
        
         var nodeTexture = new CustomSimpleTexture2D(Color.blue, 1, 1);
         var nodeGeneratorTexture = new CustomSimpleTexture2D(Color.black, 1, 1);

         var nodeDragTexture = new CustomSimpleTexture2D(Color.green, 1, 1);

         var nodeGeneratorPanelRect = new ReferenceRect(
             new Rect(Screen.width - NodeGeneratorWidth, 
                 0, 
                 NodeGeneratorWidth,
                 Screen.height));
         
         var createButtonRect = new ReferenceRect(
             new Rect(0, 0, nodeGeneratorPanelRect.Get().width, CreateNodeButtonHeight));
         
         var nodeFactory = new Storage<INode>(new SpeechNodeFactory(nodeTexture, nodeDragTexture, mousePosition, _editNodePanelRect));
         
         _nodeGenerator = 
             new NodeGenerator(nodeFactory, 
                 _updates,
                 nodeGeneratorTexture,
                 nodeGeneratorPanelRect,
                 createButtonRect);
         
         _updates.Add(_nodeGenerator);
     }

     private void InitEditNodePanel()
     {
         var mouseUpInput = new EventInput(new MouseUpInput());
         var color = Color.Lerp(Color.blue, Color.white, 0.5f);
         var texture = new CustomBorderTexture2D(color, EditNodePanelBorderWidth, EditNodePanelWidth, EditNodePanelHeight);
         
         var editNodePanel = new EditNodePanel(mouseUpInput, texture, _nodeGenerator.Nodes, _editNodePanelRect);
         _updates.Add(editNodePanel);
     }
}
