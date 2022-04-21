using UnityEditor;
using UnityEngine;

public class DialogueWindow : EditorWindow
{
    private PropertiesPanel _propertiesPanel;
    private Updates _updates;
    
    private const int NodeGeneratorWidth = 300;
    private const int CreateNodeButtonHeight = 30;

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

         var nodeGeneratorPanelRect = new ReferenceRect(
             new Rect(Screen.width - NodeGeneratorWidth, 
                 0, 
                 NodeGeneratorWidth,
                 Screen.height));
         
         var createButtonRect = new ReferenceRect(
             new Rect(0, 0, nodeGeneratorPanelRect.Get().width, CreateNodeButtonHeight));
         
         var nodeGenerator =
             new NodeGenerator(
                 new SpeechNodeFactory(nodeTexture, mousePosition), 
                 _updates,
                 nodeGeneratorTexture,
                 nodeGeneratorPanelRect,
                 createButtonRect);
         
         _updates.Add(nodeGenerator);
     }
}
