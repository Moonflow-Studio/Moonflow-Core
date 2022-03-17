namespace Moonflow.Core
{
    /// <summary>
    /// 封装运行逻辑
    /// </summary>
    public interface IMFRuntimeData
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public void OnInitial();
        
        /// <summary>
        /// 每帧更新前
        /// </summary>
        public void BeforeTickUpdate();
        
        /// <summary>
        /// 每帧更新时
        /// </summary>
        public void OnTickUpdate();
        
        /// <summary>
        /// 每帧更新后
        /// </summary>
        public void LateTickUpdate();
        
        /// <summary>
        /// 结束运行时
        /// </summary>
        public void OnEnd();
    }
}