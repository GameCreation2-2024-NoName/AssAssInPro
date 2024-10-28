using UnityEngine;

namespace Pditine.Utility
{
    public static class PhysicsUtility
    {
        
        /// <summary>
        /// 计算完全弹性碰撞后的速度向量
        /// </summary>
        /// <param name="velocity1">物体1的初始速度向量</param>
        /// <param name="velocity2">物体2的初始速度向量</param>
        /// <param name="mass1">物体1的质量</param>
        /// <param name="mass2">物体2的质量</param>
        /// <param name="collisionNormal">碰撞法线单位向量</param>
        /// <returns>两个物体碰撞后的速度向量</returns>
        public static (Vector2 v1Prime, Vector2 v2Prime) ElasticCollision(Vector2 velocity1, Vector2 velocity2, float mass1, float mass2, Vector2 collisionNormal)
        {
            // 计算沿碰撞法线的速度分量
            Vector2 v1Normal = Vector2.Dot(velocity1, collisionNormal) * collisionNormal;
            Vector2 v2Normal = Vector2.Dot(velocity2, collisionNormal) * collisionNormal;

            // 计算沿碰撞切线的速度分量
            Vector2 v1Tangent = velocity1 - v1Normal;
            Vector2 v2Tangent = velocity2 - v2Normal;

            // 计算碰撞后沿法线方向的速度
            Vector2 v1NormalPrime = (v1Normal * (mass1 - mass2) + 2 * mass2 * v2Normal) / (mass1 + mass2);
            Vector2 v2NormalPrime = (v2Normal * (mass2 - mass1) + 2 * mass1 * v1Normal) / (mass1 + mass2);

            // 重新组合速度向量
            Vector2 v1Prime = v1NormalPrime + v1Tangent;
            Vector2 v2Prime = v2NormalPrime + v2Tangent;

            return (v1Prime, v2Prime);
        }

        /// <summary>
        /// 计算完全弹性碰撞后的速度向量
        /// </summary>
        /// <param name="velocity1">物体1的初始速度向量</param>
        /// <param name="velocity2">物体2的初始速度向量</param>
        /// <param name="mass1">物体1的质量</param>
        /// <param name="mass2">物体2的质量</param>
        /// <param name="position1">物体1的位置,用以计算碰撞法线</param>
        /// <param name="position2">物体2的位置,用以计算碰撞法线</param>
        /// <returns>两个物体碰撞后的速度向量</returns>
        public static (Vector2 v1Prime, Vector2 v2Prime) ElasticCollision(Vector2 velocity1, Vector2 velocity2, float mass1, float mass2, Vector2 position1,Vector2 position2)
        {
            return ElasticCollision(velocity1, velocity2, mass1, mass2, CollisionNormal(position1, position2));
        }
        
        /// <summary>
        /// 计算两点之间的单位法线向量
        /// </summary>
        /// <param name="position1">物体1的位置</param>
        /// <param name="position2">物体2的位置</param>
        /// <returns>碰撞法线单位向量</returns>
        public static Vector2 CollisionNormal(Vector2 position1, Vector2 position2)
        {
            Vector2 difference = position1 - position2;
            return difference.normalized;
        }
        
        /// <summary>
        /// 获得碰撞后，火花特效的方向
        /// </summary>
        /// <param name="velocity">入射角方向</param>
        /// <param name="normal"></param>
        /// <returns></returns>
        public static Vector2 SparkDir(Vector2 velocity, Vector2 normal, float sparkSpeed = 30)
        {
            var plantDir = new Vector2(-normal.y, normal.x);
            if(Vector2.Dot(plantDir, -velocity) < 0)
            {
                plantDir = -plantDir;
            }
            return (-velocity + plantDir) / 2 * sparkSpeed;
        }
        
    }
}