using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using System.IO;

public class CreateAnimator : Editor
{
    [MenuItem("Tools/CreateAnimator")]
    static void DoCreateAnimationAssets()
    {
        string fullname = AssetDatabase.GetAssetPath(Selection.activeObject);
        string path = fullname.Substring(0, fullname.LastIndexOf('/'));
        string parentPath = path.Substring(path.LastIndexOf('/')+1);
        string ctrlName = "anim_" + parentPath + ".controller";
        AnimatorController animatorController = AnimatorController.CreateAnimatorControllerAtPath(path  + "/" + ctrlName);

        string[] files=Directory.GetFiles(path);
        foreach (var fi in files)
        {
            AnimatorControllerLayer layer = animatorController.GetLayer(0);
            if (fi.EndsWith(".fbx"))
            {
                AddStateTransition(fi, layer);
            }
        }
    }

    private static void AddStateTransition(string path, AnimatorControllerLayer layer)
    {
        UnityEditorInternal.StateMachine sm = layer.stateMachine;
        AnimationClip newClip = AssetDatabase.LoadAssetAtPath(path, typeof(AnimationClip)) as AnimationClip;
        State state = sm.AddState(newClip.name);
        state.SetAnimationClip(newClip, layer);
        Transition trans = sm.AddAnyStateTransition(state);
        trans.RemoveCondition(0);
    }
}
