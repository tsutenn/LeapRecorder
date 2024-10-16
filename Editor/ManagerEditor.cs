using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SimpleLeapManager))]
public class ManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        //拿到拓展的类 调用内部公开的方法
        SimpleLeapManager manager = (SimpleLeapManager)target;
        if (GUILayout.Button("Auto Bind"))
        {
            manager.Bind();
        }
    }
}
