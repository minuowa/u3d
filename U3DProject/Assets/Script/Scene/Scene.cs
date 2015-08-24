using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ExportNavigationData : XMLFile
{
    public List<int> indices;
    public List<Vector3> verts;
}
public class NpcData
{
    public Vector3 pos;
    public int npcid = 0;
    public int ai = 0;
    public string name;
}
public class SceneOjbects : XMLFile
{
    public List<NpcData> npcs;
}
public class Scene : MonoBehaviour
{
    public int sceneID = 100;

    public void ReloadObjects()
    {
        GameObject npcroot = RecreateNpcRoot();

        string outfile = MS<Setting>.Instance.ScenePath + sceneID.ToString() + "_objects";
        SceneOjbects scene = AResource.LoadXML<SceneOjbects>(outfile);

        foreach (var data in scene.npcs)
        {
            GameObject go = AResource.Instance("Prefabs/npcs/Npc");
            go.name = data.name;
            go.transform.localPosition = data.pos;
            go.transform.parent = npcroot.transform;
            go.AddComponent<Npc>();
            StatBeing stat = go.GetComponent<StatBeing>();
            stat.globalID = data.npcid;
        }
    }
    GameObject RecreateNpcRoot()
    {
        string name = "npcroot";
        GameObject npcroot = GameObject.Find(name);
        GameObject.DestroyImmediate(npcroot);
        return new GameObject(name);
    }
    public void RandomGenerateNpcs()
    {
        GameObject npcroot = RecreateNpcRoot();

        Terrain terrain = Terrain.activeTerrain;

        for (int i = 0; i < 20; ++i)
        {
            GameObject go = AResource.Instance("Prefabs/npcs/Npc");
            go.name = "xiao" + i.ToString();
            float x = Random.Range(0f, terrain.terrainData.size.x);
            float z = Random.Range(0f, terrain.terrainData.size.z);
            float y = terrain.terrainData.GetInterpolatedHeight(x, z);
            go.transform.localPosition = new Vector3(x, y, z);
            go.transform.parent = npcroot.transform;
            go.AddComponent<Being>();
            StatBeing stat = go.GetComponent<StatBeing>();
            stat.globalID = i;
        }
    }
    public void ExportObjects()
    {
        Npc[] beings = (Npc[])GameObject.FindObjectsOfType(typeof(Npc));

        SceneOjbects objects = new SceneOjbects();
        objects.npcs = new List<NpcData>();

        foreach (var being in beings)
        {
            StatBeing stat = being.GetComponent<StatBeing>();
            NpcData data = new NpcData();
            data.pos = being.gameObject.transform.localPosition;
            data.npcid = stat.globalID;
            data.ai = 0;
            data.name = being.gameObject.name;
            objects.npcs.Add(data);
        }

        string outfile = MS<Setting>.Instance.ScenePath + sceneID.ToString() + "_objects";
        AResource.SaveXML(objects, outfile);
    }
    public void ExportNavigation()
    {
        NavMeshTriangulation trianglations = NavMesh.CalculateTriangulation();
        ExportNavigationData nav = new ExportNavigationData();
        nav.indices = new List<int>();
        nav.verts = new List<Vector3>();
        nav.indices.AddRange(trianglations.indices);
        nav.verts.AddRange(trianglations.vertices);
        string outfile = MS<Setting>.Instance.ScenePath + sceneID.ToString() + "_scene";
        AResource.SaveXML(nav, outfile);
    }
}
