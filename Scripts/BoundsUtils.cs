using UnityEngine;

namespace XTN.BoundTool
{
    public static class BoundsUtils
    {
        /// <summary>
        ///     获取物体的包围盒
        /// </summary>
        /// <param name="target"></param>
        /// <param name="includeChildren"></param>
        /// <returns></returns>
        public static Bounds GetBounds(Transform target, bool includeChildren = true)
        {
            Vector3 center = Vector3.zero;
            Renderer[] renders = null;
            if (includeChildren)
            {
                renders = target.GetComponentsInChildren<Renderer>();
            }
            else
            {
                renders = new[]
                {
                    target.GetComponent<Renderer>()
                };
            }

            foreach (Renderer child in renders)
            {
                center += child.bounds.center;
            }

            center /= renders.Length;
            Bounds bounds = new Bounds(center, Vector3.zero);
            foreach (Renderer child in renders)
            {
                bounds.Encapsulate(child.bounds);
            }

            return bounds;
        }

        /// <summary>
        /// 如果已经存在BoxCollider，就不添加了
        /// </summary>
        /// <param name="target"></param>
        /// <param name="includeChildren"></param>
        /// <returns></returns>
        public static BoxCollider AddCollider(Transform target, bool includeChildren = true)
        {
            BoxCollider bc = target.GetComponent<BoxCollider>();
            if (bc != null)
            {
                Debug.Log($"想给物体{target.name}添加BoxCollider，但是物体上已经存在了");
            }
            else
            {
                
                Transform originParent = target.parent;
                Vector3 originLocalPos = target.localPosition;
                Quaternion originLocalRot = target.localRotation;
                Vector3 originLocalScale = target.localScale;
                
                target.SetParent(null);
                target.localPosition=Vector3.zero;
                target.localRotation=Quaternion.identity;
                target.localScale=Vector3.one;
                
                Bounds bounds = GetBounds(target, includeChildren);
                bc = target.gameObject.AddComponent<BoxCollider>();
                bc.center = bounds.center - target.position;
                bc.size = bounds.size;
                
                target.SetParent(originParent);
                target.localPosition = originLocalPos;
                target.localRotation = originLocalRot;
                target.localScale = originLocalScale;
            }

            return bc;
        }
    }
}