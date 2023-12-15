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
        /// 给物体添加合适的BoxCollider
        /// </summary>
        /// <param name="target"></param>
        /// <param name="includeChildren"></param>
        /// <returns></returns>
        public static BoxCollider AddBoxCollider(Transform target, bool includeChildren = true)
        {
            Transform originParent = target.parent;
            Vector3 originLocalPos = target.localPosition;
            Quaternion originLocalRot = target.localRotation;
            Vector3 originLocalScale = target.localScale;

            target.SetParent(null);
            target.localPosition = Vector3.zero;
            target.localRotation = Quaternion.identity;
            target.localScale = Vector3.one;

            Bounds bounds = GetBounds(target, includeChildren);
            BoxCollider bc = target.gameObject.AddComponent<BoxCollider>();
            bc.center = bounds.center - target.position;
            bc.size = bounds.size;

            target.SetParent(originParent);
            target.localPosition = originLocalPos;
            target.localRotation = originLocalRot;
            target.localScale = originLocalScale;

            return bc;
        }

        /// <summary>
        /// 将物体等比例填充到Bound内
        /// 不会超出Bound边界，且模型不会拉伸变形
        /// </summary>
        /// <param name="target"></param>
        /// <param name="bounds"></param>
        /// <param name="includeChildren"></param>
        public static void FillModelToBound(Transform target,Bounds bounds, bool includeChildren = true)
        {
            
        }

        /// <summary>
        /// 将Bound绘制出来
        /// </summary>
        /// <param name="bounds"></param>
        public static void DrawBound(Bounds bounds)
        {
            
        }
    }
}