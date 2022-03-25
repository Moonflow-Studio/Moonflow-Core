namespace MoonflowCore.Runtime.Framework.Data
{
    /// <summary>
    /// 封装场景切换周期系统
    /// </summary>
    public interface IMFSceneCycle
    {
        /// <summary>
        /// 进入场景时
        /// </summary>
        public void OnEnterScene();
        /// <summary>
        /// 退出场景时
        /// </summary>
        public void OnLeaveScene();
    }
}