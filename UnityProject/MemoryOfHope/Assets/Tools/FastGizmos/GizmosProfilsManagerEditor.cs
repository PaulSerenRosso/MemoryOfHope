using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class GizmosProfilsManagerEditor : Editor
{
    private VisualElement _root;
    private VisualTreeAsset _visualTree;
    private GizmosViewer _gizmosViewer;   
    void OnEnable()
    {
        _gizmosViewer = target as GizmosViewer;
        _root = new VisualElement();
        _visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("");
        var uss = AssetDatabase.LoadAssetAtPath<StyleSheet>("");
        _root.styleSheets.Add(uss);
    }

    public override VisualElement CreateInspectorGUI()
    {
        var root = _root; 
        root.Clear();
        _visualTree.CloneTree(root);
        //   var _fastGizmosList = _root.Q<>()
        return base.CreateInspectorGUI();
    }
}