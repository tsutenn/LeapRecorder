using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Recorder))]
public class RecorderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        //拿到拓展的类 调用内部公开的方法
        Recorder recorder = (Recorder)target;
        
        if (GUILayout.Button("Start Record"))
        {
            recorder.StartRecord();
        }
        if (GUILayout.Button("Finish Record"))
        {
            recorder.FinishRecord();
        }
        if (GUILayout.Button("Start Play"))
        {
            recorder.StartPlay();
        }
        if (GUILayout.Button("Finish Play"))
        {
            recorder.FinishPlay();
        }
    }
}
