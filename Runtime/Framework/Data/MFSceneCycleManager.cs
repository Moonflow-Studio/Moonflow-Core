using System.Collections.Generic;

namespace MoonflowCore.Runtime.Framework.Data
{
    public class MFSceneCycleManager: MFSingleton<MFSceneCycleManager>
    {
        public List<IMFSceneCycle> sceneCycleSystems { get;}

        public MFSceneCycleManager()
        {
            sceneCycleSystems = new List<IMFSceneCycle>();
        }
        public void AddSystem(IMFSceneCycle sceneCycleSystem)
        {
            sceneCycleSystems.Add(sceneCycleSystem);
        }

        public void EnterScene()
        {
            for (int i = 0; i < sceneCycleSystems.Count; i++)
            {
                sceneCycleSystems[i].OnEnterScene();
            }
        }

        public void LeaveScene()
        {
            for (int i = 0; i < sceneCycleSystems.Count; i++)
            {
                sceneCycleSystems[i].OnLeaveScene();
            }
        }
    }
}