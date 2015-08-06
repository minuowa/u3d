//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//public class AISystem : UnityEngine.MonoBehaviour
//{
//    /// <summary>
//    /// 导出目录
//    /// </summary>
//    public string workSpace = "Assets/Resources/behaviac/exported";
//    public string ballAI = "Ball";
//    public AISystem()
//    {
//        behaviac.Workspace.SetWorkspaceSettings(workSpace);
//    }
//    public void Load(behaviac.Agent instance, string behaviorTree)
//    {
//        if (behaviorTree.Length > 0)
//        {
//            bool btloadResult = instance.btload(behaviorTree, true);
//            if (btloadResult)
//                instance.btsetcurrent(behaviorTree);
//        }
//    }
//}
