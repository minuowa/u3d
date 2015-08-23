/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Tencent is pleased to support the open source community by making behaviac available.
//
// Copyright (C) 2015 THL A29 Limited, a Tencent company. All rights reserved.
//
// Licensed under the BSD 3-Clause License (the "License"); you may not use this file except in compliance with
// the License. You may obtain a copy of the License at http://opensource.org/licenses/BSD-3-Clause
//
// Unless required by applicable law or agreed to in writing, software distributed under the License is
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEditor;

public class BehaviacMenus
{
    [MenuItem("Behaviac/Export Meta")]
    static void CreateBTMetaFile()
    {
        behaviac.Workspace.SetWorkspaceSettings("Assets/Game/BehaviourTrees");

        ////register names
        //behaviac.Agent.RegisterName<ParTestRegNameAgent>();

        behaviac.Workspace.ExportMetas("Assets/Game/BehaviourTrees/xmlmeta/bt_meta.xml");
    }

	[MenuItem("Behaviac/Export Behaviac Package")]
	static void ExportBehaviac()
	{
        AssetDatabase.ExportPackage("Assets/Scripts/behaviac/runtime", "../../intermediate/behaviac.unitypackage", ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies);
	}
}