using UnityEditor;
using UnityEngine;
using XTN.BoundTool;

namespace BoundToolPackage.XTNEditor
{
    public class BoundToolEditor: Editor
    {
        [MenuItem("GameObject/XTNTool/Bound/AddCollider", false,0)]
        public static void AddCollider()
        {
            GameObject go = Selection.gameObjects[0];
            if (go == null)
            {
                return;
            }

            BoundsUtils.AddBoxCollider(go.transform);
            EditorUtility.SetDirty(go);
        }
    }
}