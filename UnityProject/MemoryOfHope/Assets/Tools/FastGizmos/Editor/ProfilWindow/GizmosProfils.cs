using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class GizmosProfils : EditorWindow
{
    private StyleSheet _sheet;
    private VisualElement _root;
    private VisualTreeAsset _tree;
    private Transform _currentTarget;
    private GizmosProfil _newProfil;
    private SerializedObject _serializeProfil;
    private GizmosProfilEnum _profilEnum;
    private ObjectField _transformTarget;
    private ToolbarButton _createButton;
    private Button _saveButton;
    private string _currentNameNewProfil;
    private EnumField _profilEnumField;
    private bool _inCreateNewProfil; 
    private ScrollView _profilView;
    private string _newProfilName;
    private TextField _newProfilNameField;
    private StyleEnum<DisplayStyle> _NewProfilNameFieldDisplayStyle;
    [MenuItem("Tools/GizmosProfils")]
    public static void OpenWindow()
    {
   
        DestroyImmediate(GetWindow<GizmosProfils>());
        GizmosProfils wnd = GetWindow<GizmosProfils>();
      
        wnd.titleContent = new GUIContent("GizmosProfils");
    }

    private void OnEnable()
    {
        _root = rootVisualElement;
        _tree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
            "Assets/Tools/FastGizmos/Editor/ProfilWindow/GizmosProfils.uxml");
        _sheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(
            "Assets/Tools/FastGizmos/Editor/ProfilWindow/GizmosProfils.uss");
        
        _root.styleSheets.Add(_sheet);
        _root.Clear();
        _tree.CloneTree(_root);
        
         _saveButton = _root.Q<Button>("SaveProfil");
         _saveButton.clickable.clicked += SaveProfil;
         _saveButton.SetEnabled(false);
        
         _createButton = _root.Q<ToolbarButton>("CreateProfil");
                _createButton.clickable.clicked += CreateNewProfil;
                
             _transformTarget = _root.Q<ObjectField>("CurrentTarget");
                _transformTarget.objectType = typeof(Transform);
           
                
                _profilView = _root.Q<ScrollView>("ProfilView");
                _profilEnumField = _root.Q<EnumField>("ProfilEnum");
                _profilEnumField.Init(GizmosProfilEnum.None);

                Debug.Log("teststs");
                _newProfil = CreateInstance<GizmosProfil>();
                       _serializeProfil = new SerializedObject(_newProfil);
                       _newProfil.Reset();

                       _newProfilNameField = _root.Q<TextField>("ProfilName");
                       _newProfilNameField.SetEnabled(false);
                       _NewProfilNameFieldDisplayStyle = _newProfilNameField.style.display; 
                       _NewProfilNameFieldDisplayStyle.value = DisplayStyle.None;
    }


    private void OnInspectorUpdate()
    {
    
    }

    public void CreateGUI()
    {
        if (_transformTarget.RegisterValueChangedCallback(evt =>
        {
            _currentTarget = (Transform) _transformTarget.value;
            if (_currentTarget == null)
            {
                _saveButton.SetEnabled(false);
            }
            else
            {
                _saveButton.SetEnabled(true);
            }
        }))
            if (_inCreateNewProfil)
            {
                _serializeProfil.Update();
                _serializeProfil.ApplyModifiedProperties();        
                _newProfilName = _newProfilNameField.text;
            }



        _profilEnum = (GizmosProfilEnum) _profilEnumField.value;
    }
    

    void CreateNewProfil()
    {
        _NewProfilNameFieldDisplayStyle.value = DisplayStyle.Flex;
        _inCreateNewProfil = true;
        _newProfil.Reset();
        SerializedProperty serializedList = _serializeProfil.FindProperty("AllGizmos");
        serializedList.arraySize = 3;
 _profilEnumField.value = GizmosProfilEnum.None;
        for (int i = 0; i < serializedList.arraySize; i++)
        {
            PropertyField  _profilField = new PropertyField();
            _profilField.BindProperty(serializedList.GetArrayElementAtIndex(i));
           
            _profilField.AddToClassList("ListProfils");
       
            
           _profilView.Add(_profilField); 
           
        }
    }

    void SaveProfil()
    {
        // convertir le string en enum et l'assigner à l'object 
        // puis créer le scriptable Object et l'ajouter à la database
        
        _inCreateNewProfil = false; 
    }
}