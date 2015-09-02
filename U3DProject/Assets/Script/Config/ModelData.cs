using UnityEngine;
using System.Collections;
namespace Config
{
    public class ModelData :
        Record<ModelData>
    {
        public static string filename = "config/ModelData";
        public string prefab;

        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;

        public GameObject GenerateModel()
        {
            GameObject prefabObj = Resources.Load(prefab) as GameObject;
            GameObject go = GameObject.Instantiate(prefabObj) as GameObject;
            go.transform.localPosition = position;
            go.transform.localRotation = Quaternion.Euler(rotation);
            go.transform.localScale = scale;
            return go;
        }
    }
}
