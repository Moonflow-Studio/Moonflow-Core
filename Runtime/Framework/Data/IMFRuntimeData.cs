namespace Moonflow.Core
{
    public interface IMFRuntimeData
    {
        public void OnInitial();
        public void BeforeTickUpdate();
        public void OnTickUpdate();
        public void LateTickUpdate();
        public void OnEnd();
    }
}