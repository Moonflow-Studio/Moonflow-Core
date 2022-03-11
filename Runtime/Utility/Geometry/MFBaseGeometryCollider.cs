using UnityEngine;

namespace Moonflow.Utility.Geometry
{
    /// <summary>
    /// 简单集合碰撞检测
    /// </summary>
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
        
        /// <summary>
        /// 立方体碰撞检测
        /// </summary>
        /// <param name="targetPos">被检测的点</param>
        /// <param name="objToWorldMatrix">碰撞体的Obj2World转换矩阵</param>
        /// <param name="size">碰撞体本地尺寸</param>
        /// <returns></returns>
        public bool Box(Vector3 targetPos, Matrix4x4 objToWorldMatrix, Vector3 size)
        {
            var inverseMatrix = Matrix4x4.Inverse(objToWorldMatrix);
            var localPos = inverseMatrix.MultiplyPoint(targetPos);
            localPos = new Vector3(Mathf.Abs(localPos.x), Mathf.Abs(localPos.y), Mathf.Abs(localPos.z));
            return localPos.x < size.x && localPos.y < size.y && localPos.z < size.z;
        }
        
        /// <summary>
        /// 扇形碰撞检测
        /// </summary>
        /// <param name="targetPos">被检测的点</param>
        /// <param name="center">扇形圆心</param>
        /// <param name="direction">扇形方向</param>
        /// <param name="radius">扇形半径</param>
        /// <param name="angle">扇形角度</param>
        /// <returns></returns>
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