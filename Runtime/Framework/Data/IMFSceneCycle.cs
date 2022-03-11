namespace MoonflowCore.Runtime.Framework.Data
{
    /// <summary>
    /// 封装场景切换周期系统
    /// </summary>
    public interface IMFSceneCycle
    {
        public void OnEnterScene();
        public void OnLeaveScene();
    }
}