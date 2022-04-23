using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DialogueWindow : EditorWindow
{
    private PropertiesPanel _propertiesPanel;
    private Updates _updates;
    
    private const int NodeGeneratorWidth = 300;
    private const int CreateNodeButtonHeight = 30;
    
    private const int EditNodePanelWidth = 200;
    private const int EditNodePanelHeight= 200;
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

     private void InitEditNodePanel()
     {
         var editNodePanel = new EditNodePanel(new ReferenceRect(new Rect(0, 0, EditNodePanelWidth, EditNodePanelHeight)),
             new CustomBorderTexture2D(Color.Lerp(Color.blue, Color.white, 0.5f), 
                 EditNodePanelBorderWidth, 50, 50));
         
         _updates.Add(editNodePanel);
     }
}
