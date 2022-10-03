using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
using UnityEditor.UI;

[CustomEditor(typeof(RenderTexture))]
public class RenderTextureInspector : Editor
{
    private Editor m_editor;

    public override void OnInspectorGUI()
    {
        if (m_editor == null)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var a in assemblies)
            {
                var type = a.GetType("UnityEditor.RenderTextureEditor");
                if (type != null)
                {
                    m_editor = Editor.CreateEditor(target, type);
                    break;
                }
            }
        }

        if (m_editor != null)
            m_editor.OnInspectorGUI();
    }
}

[CustomPreview(typeof(RenderTexture))]
public class RenderTexturePreview : ObjectPreview
{
    public override bool HasPreviewGUI()
    {
        return true;
    }

    public override void OnPreviewGUI(Rect r, GUIStyle background)
    {
        RenderTexture renderTexture = (RenderTexture)target;

        GUI.DrawTexture(r, renderTexture, ScaleMode.ScaleToFit, false);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label(renderTexture.width + "x" + renderTexture.height + " " + renderTexture.graphicsFormat + " " + renderTexture.name);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Save to .png"))
        {
            var path = EditorUtility.SaveFilePanel("Save RenderTexture as png", "", renderTexture.name + ".png", "png");
            if (path.Length != 0)
            {
                Texture2D tex = ToTexture2D((RenderTexture)renderTexture);
                byte[] data = tex.EncodeToPNG();
                File.WriteAllBytes(path, data);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                EditorUtility.FocusProjectWindow();
                string relativePath = "Assets" + path.Substring(Application.dataPath.Length);
                Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(relativePath);
            }
        }
    }

    Texture2D ToTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.ARGB32, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}
