//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using System.Reflection;

/// <summary>
/// StoryMaker lets you create font prefabs with a single click of a button.
/// </summary>

public class StoryMaker : EditorWindow
{

	/// <summary>
	/// Update all labels associated with this font.
	/// </summary>

	void MarkAsChanged ()
	{
		if (NGUISettings.ambigiousFont != null)
		{
			List<UILabel> labels = NGUIEditorTools.FindAll<UILabel>();

			foreach (UILabel lbl in labels)
			{
				if (lbl.ambigiousFont == NGUISettings.ambigiousFont)
				{
					lbl.ambigiousFont = null;
					lbl.ambigiousFont = NGUISettings.ambigiousFont;
				}
			}
		}
	}


	void OnSelectionChange () { Repaint(); }
	void OnUnityFont (UnityEngine.Object obj) { NGUISettings.ambigiousFont = obj; }

	/// <summary>
	/// Draw the UI for this tool.
	/// </summary>
    Vector2 mScroll = Vector2.zero;
    public string defaultPath = "UI/Story/";

    Story _cur;
    UnityEngine.Object _prefab;
    List<Story> _stories=new List<Story>();

    void Check(bool update)
    {
        if (update)
        {
            _stories.Clear();
        }
    }
    void OnStory(UnityEngine.Object obj)
    {
        _prefab = obj;
        if (_cur)
            GameObject.DestroyImmediate(_cur);
        _cur = GameObject.Instantiate(obj) as Story;
        _cur.name = obj.name;
    }

    void DrawHeader()
    {
        GUILayout.BeginHorizontal("AS TextArea", GUILayout.MinHeight(20f));
        {
            if (GUILayout.Button("Story", GUILayout.Width(50f)))
            {
                ComponentSelector.Show<Story>(OnStory, new string[] { ".prefab" });
            }
            string curname = string.Empty;
            if (_cur)
            {
                curname = _cur.name;
            }
            else
            {
                curname = "NewStory";
            }

            GUILayout.Label(curname, GUILayout.Width(200));

            if (GUILayout.Button("New"))
            {
                string path = EditorUtility.SaveFilePanelInProject("Save As",
"New Story.prefab", "prefab", "Save atlas as...", defaultPath);
                if (!string.IsNullOrEmpty(path))
                {
                    UnityEngine.Object prefab = PrefabUtility.CreateEmptyPrefab(path);
                    string atlasName = path.Replace(".prefab", "");

                    GameObject go = new GameObject(atlasName);
                    go.AddComponent<Story>();

                    PrefabUtility.ReplacePrefab(go, prefab);
                    DestroyImmediate(go);
                }
            }

            if (GUILayout.Button("Save"))
            {
                if (_cur)
                {
                    _prefab = PrefabUtility.ReplacePrefab(_cur.gameObject, _prefab);
                }
            }

            GUILayout.EndHorizontal();
        }
    }
    void DrawList()
    {
        GUILayout.BeginHorizontal();
        {

            GUILayout.Space(3f);
            GUILayout.BeginVertical();
            {
                mScroll = GUILayout.BeginScrollView(mScroll);
                if(_cur)
                {
                    for (int i = 0; i < _cur.gameObject.transform.childCount; ++i)
                    {
                        GameObject go = _cur.gameObject.transform.GetChild(i).gameObject;
                        GUILayout.BeginHorizontal("AS TextArea", GUILayout.MinHeight(20f));

                        GUILayout.Label(i.ToString(), GUILayout.Width(24f));
                        if (GUILayout.Button(go.name,GUILayout.Width(200)))
                            Selection.activeGameObject = go;

                        DrawType(go);

                        if (GUILayout.Button("X", GUILayout.Width(30f)))
                            _cur.Delete(go);

                        GUILayout.EndHorizontal();
                    }
                }
                GUILayout.EndScrollView();

                if (_cur)
                {
                    GUILayout.BeginHorizontal("AS TextArea", GUILayout.MinHeight(20f));
                        if (GUILayout.Button("AddStep"))
                    {
                        Selection.activeGameObject = _cur.Add(_cur.transform.childCount.ToString());
                    }
                    GUILayout.EndHorizontal();
                }


            }
            GUILayout.EndVertical();
        }
        GUILayout.EndHorizontal();
    }
    StoryIndex GetIndex(GameObject go)
    {
        if (!go)
            return StoryIndex.Empty;

        Component[] coms = go.GetComponents(typeof(MonoBehaviour));
        foreach (var com in coms)
        {
            Type tp = com.GetType();
            FieldInfo info=tp.GetField("storyType");
            if (info != null)
            {
                return (StoryIndex)info.GetValue(com);
            }
        }

        return StoryIndex.Empty;
    }
    void Operator(GameObject go,StoryIndex index, bool add)
    {
        if (!go)
            return;
        Assembly ass = typeof(StoryType.WarGuide).Assembly;
        var types = ass.GetTypes();
        Type tarType = null;
        foreach (var item in types)
        {
            if (item.Namespace == "StoryType")
            {
                FieldInfo info = item.GetField("storyType");
                if (info != null && (StoryIndex)info.GetValue(null) == index)
                {
                    tarType = item;
                    break;
                }
            }
        }
        if (tarType != null)
        {
            if(add)
                go.AddComponent(tarType);
            else
                GameObject.DestroyImmediate(go.GetComponent(tarType));
        }
    }
    void DrawType(GameObject go)
    {
        StoryIndex idx = GetIndex(go);
        GUI.changed = false;
        StoryIndex newIdx = (StoryIndex)EditorGUILayout.EnumPopup("Type", idx);

        if (GUI.changed)
        {
            Selection.activeGameObject = go;
            Operator(go, idx, false);
            Operator(go, newIdx, true);
        }
    }

    void OnGUI()
    {
        GUI.backgroundColor = Color.white;
        NGUIEditorTools.DrawHeader("Items", true);

        GUILayout.BeginVertical();
        {
            DrawHeader();
            DrawList();
            GUILayout.EndVertical();
        }
    }
        
}
