using UnityEditor;
using UnityEngine;

public class DialogueWindow : EditorWindow
{
    private PropertiesPanel _propertiesPanel;
    private Updates _updates;

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

         var createButtonRect = new ReferenceRect(new Rect(50,50,100,100));
         var nodeGeneratorPanelRect = new ReferenceRect(new Rect(50, 50, 150, 150));

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
