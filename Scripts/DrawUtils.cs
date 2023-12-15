using UnityEngine;

namespace XTN.BoundTool
{
    public class DrawUtils
    {
        public static void DrawCube2D(Vector2 center, Vector2 size, Color color, float duration = -1)
        {
            if (duration <= 0)
            {
                duration = Time.deltaTime;
            }

            Vector3 centerV3 = new Vector3(center.x, 0, center.y);
            Vector3 ldV3 = centerV3 - new Vector3(size.x / 2, 0, size.y / 2);
            Debug.DrawLine(ldV3, ldV3 + new Vector3(size.x, 0, 0), color, duration);
            Debug.DrawLine(ldV3, ldV3 + new Vector3(0, 0, size.y), color, duration);
            Debug.DrawLine(ldV3 + new Vector3(size.x, 0, 0), ldV3 + new Vector3(size.x, 0, size.y), color, duration);
            Debug.DrawLine(ldV3 + new Vector3(0, 0, size.y), ldV3 + new Vector3(size.x, 0, size.y), color, duration);
        }

        public static void DrawCube2D(Vector2 center, float length, float clockwiseAngle, Color color, float duration = -1)
        {
            DrawCube2D(center, new Vector2(length, length), clockwiseAngle, color, duration);
        }

        public static void DrawCube2D(Vector2 center, Vector2 size, float clockwiseAngle, Color color, float duration = -1)
        {
            if (duration <= 0)
            {
                duration = Time.deltaTime;
            }

            Vector3 centerV3 = new Vector3(center.x, 0, center.y);
            float tanAngle = 90 - Mathf.Atan(size.y / size.x) * Mathf.Rad2Deg;
            float halfDistance = size.magnitude / 2;
            Vector3 topDir = new Vector3(0, 0, halfDistance);

            Debug.DrawLine(centerV3 + Quaternion.AngleAxis(tanAngle + clockwiseAngle, Vector3.up) * topDir, centerV3 + Quaternion.AngleAxis(-tanAngle + clockwiseAngle, Vector3.up) * topDir, color, duration);
            Debug.DrawLine(centerV3 + Quaternion.AngleAxis(tanAngle + 180 + clockwiseAngle, Vector3.up) * topDir, centerV3 + Quaternion.AngleAxis(-tanAngle - 180 + clockwiseAngle, Vector3.up) * topDir, color, duration);

            Debug.DrawLine(centerV3 + Quaternion.AngleAxis(tanAngle + clockwiseAngle, Vector3.up) * topDir, centerV3 + Quaternion.AngleAxis(-tanAngle - 180 + clockwiseAngle, Vector3.up) * topDir, color, duration);
            Debug.DrawLine(centerV3 + Quaternion.AngleAxis(-tanAngle + clockwiseAngle, Vector3.up) * topDir, centerV3 + Quaternion.AngleAxis(tanAngle + 180 + clockwiseAngle, Vector3.up) * topDir, color, duration);
        }

        public static void DrawCube2D(Vector3Int center, Vector2 size, Color color, float duration = -1)
        {
            DrawCube2D(new Vector2(center.x, center.z), size, color, duration);
        }

        public static void DrawSphere(Vector3 center, float radius, Color color, float duration = -1)
        {
            for (int i = 0; i < 359; i++)
            {
                Vector3 targetPos1 = center + Quaternion.AngleAxis(i, Vector3.up) * Vector3.right * radius;
                Vector3 targetPos2 = center + Quaternion.AngleAxis(i + 1, Vector3.up) * Vector3.right * radius;
                Debug.DrawLine(targetPos1, targetPos2, color, duration);
            }

            Vector3 rightPos = center + Quaternion.AngleAxis(0, Vector3.up) * Vector3.right * radius;
            Vector3 downPos = center + Quaternion.AngleAxis(90, Vector3.up) * Vector3.right * radius;
            Vector3 leftPos = center + Quaternion.AngleAxis(180, Vector3.up) * Vector3.right * radius;
            Vector3 upPos = center + Quaternion.AngleAxis(-90, Vector3.up) * Vector3.right * radius;
            Debug.DrawLine(rightPos, leftPos, color, duration);
            Debug.DrawLine(upPos, downPos, color, duration);
        }
    }
}