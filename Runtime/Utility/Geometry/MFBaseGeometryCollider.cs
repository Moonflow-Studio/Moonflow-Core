using UnityEngine;

namespace Moonflow.Utility.Geometry
{
    public class MFBaseGeometryCollider
    {
        //2D
        public bool Circle(Vector2 targetPos, Vector2 center, float rad)
        {
            return Vector2.Distance(targetPos, center) < rad;
        }
        
        public bool Rectangle(Vector2 targetPos, Vector2 center, Vector2 left, Vector2 forward)
        {
            var dir = targetPos - center;
            return Mathf.Abs(Vector2.Dot(dir, forward.normalized)) < forward.magnitude &&
                   Mathf.Abs(Vector2.Dot(dir, left.normalized)) < left.magnitude;
        }
        
        
        //3D
        public bool Sphere(Vector3 targetPos, Vector3 center, float rad)
        {
            return Vector3.Distance(targetPos, center) < rad;
        }
        
        public bool Box(Vector3 targetPos, Matrix4x4 objToWorldMatrix, Vector3 size)
        {
            var inverseMatrix = Matrix4x4.Inverse(objToWorldMatrix);
            var localPos = inverseMatrix.MultiplyPoint(targetPos);
            localPos = new Vector3(Mathf.Abs(localPos.x), Mathf.Abs(localPos.y), Mathf.Abs(localPos.z));
            return localPos.x < size.x && localPos.y < size.y && localPos.z < size.z;
        }
        
        public bool Sector(Vector3 targetPos, Vector3 center, Vector3 direction, float radius, float angle)
        {
            var dir = targetPos - center;
            return dir.magnitude < radius &&
                   Vector3.Dot(dir.normalized, direction.normalized) > Mathf.Cos(angle * 0.5f);
        }
        
        //胶囊
        //圆柱
    }
}