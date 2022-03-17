using System.Collections.Generic;

namespace MoonflowCore.Runtime.Framework.Data
{
    /// <summary>
    /// 场景切换系统控制
    /// </summary>
    public class MFSceneCycleManager: MFSingleton<MFSceneCycleManager>
    {
        private List<IMFSceneCycle> sceneCycleSystems { get;}

        /// <summary>
        /// 
        /// </summary>
        public MFSceneCycleManager()
        {
            sceneCycleSystems = new List<IMFSceneCycle>();
        }
        
        /// <summary>
        /// 把需要进行逐场景切换的系统加入场景切换控制
        /// </summary>
        /// <param name="sceneCycleSystem">被操作的系统</param>
        public void AddSystem(IMFSceneCycle sceneCycleSystem)
        {
            sceneCycleSystems.Add(sceneCycleSystem);
        }

        /// <summary>
        /// 进入场景时执行
        /// </summary>
        public void EnterScene()
        {
            for (int i = 0; i < sceneCycleSystems.Count; i++)
            {
                sceneCycleSystems[i].OnEnterScene();
            }
        }

        /// <summary>
        /// 退出场景时执行
        /// </summary>
        public void LeaveScene()
        {
            for (int i = 0; i < sceneCycleSystems.Count; i++)
            {
                sceneCycleSystems[i].OnLeaveScene();
            }
        }
    }
}