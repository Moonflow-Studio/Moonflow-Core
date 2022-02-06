namespace Moonflow.Core
{
    public interface IMFRuntimeData
    {
        public void OnInitial();
        public void OnTickUpdate();
        public void OnEnd();
    }
}