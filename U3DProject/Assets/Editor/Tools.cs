using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
 
public class Tools :Editor {
    [MenuItem("Tools")]
    void DoCreateAnimationAssets()
    {
        foreach (var obj in targets)
        {
            Debug.Log(obj.ToString());
        }
        ////创建animationController文件，保存在Assets路径下
        //AnimatorController animatorController = AnimatorController.CreateAnimatorControllerAtPath("Assets/animation.controller");
        ////得到它的Layer， 默认layer为base 你可以去拓展
        //AnimatorControllerLayer layer = animatorController.GetLayer(0);
        ////把动画文件保存在我们创建的AnimationController中
        //AddStateTransition("Assets/Resources/airenlieren@Idle.FBX", layer);
        //AddStateTransition("Assets/Resources/attack@attack.FBX", layer);
        //AddStateTransition("Assets/Resources/aersasi@Run.FBX", layer);
    }
    [MenuItem("Tools/OpenStoryMaker")]
    static public void OpenStoryPanel()
    {
        EditorWindow.GetWindow<StoryMaker>(false, "StoryMaker", true).Show();
    }

    [MenuItem("Tools/Test/TestConfig")]
    static public void TestConfig()
    {
        Config.TaskData.recordMap = null;
        Config.TaskData data = Config.TaskData.Get(1000);
        Log.Assert(data, "Config Field!");
    }

    [MenuItem("Tools/Scene/RandomGenerateNpcs")]
    static public void RandomGenerateNpcs()
    {
        MS<Scene>.Instance.RandomGenerateNpcs();
    }
    [MenuItem("Tools/Scene/ExportObjects")]
    static public void ExportObjects()
    {
        MS<Scene>.Instance.ExportObjects();
    }
    [MenuItem("Tools/Scene/ReloadObjects")]
    static public void ReloadObjects()
    {
        MS<Scene>.Instance.ReloadObjects();
    }
    [MenuItem("RPG/Scene/ExportNavigation")]
    static public void ExportNavigation()
    {
        MS<Scene>.Instance.ExportNavigation();
    }
    //private static void AddStateTransition(string path, AnimatorControllerLayer layer)
    //{
    //    //UnityEditorInternal.StateMachine sm = layer.stateMachine;
    //    ////根据动画文件读取它的AnimationClip对象
    //    //AnimationClip newClip = AssetDatabase.LoadAssetAtPath(path, typeof(AnimationClip)) as AnimationClip;
    //    ////取出动画名子 添加到state里面
    //    //State state = sm.AddState(newClip.name);
    //    //state.SetAnimationClip(newClip, layer);
    //    ////把state添加在layer里面
    //    //Transition trans = sm.AddAnyStateTransition(state);
    //    ////把默认的时间条件删除
    //    //trans.RemoveCondition(0);
    //}
}
