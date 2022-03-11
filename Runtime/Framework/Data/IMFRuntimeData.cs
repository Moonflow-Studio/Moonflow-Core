namespace Moonflow.Core
{
    /// <summary>
    /// 封装运行逻辑
    /// </summary>
    public interface IMFRuntimeData
    {
        public void OnInitial();
        public void BeforeTickUpdate();
        public void OnTickUpdate();
        public void LateTickUpdate();
        public void OnEnd();
    }
}