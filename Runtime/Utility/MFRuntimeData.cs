namespace Moonflow
{
    public interface MFRuntimeData
    {
        public void OnInitial();
        public void OnTickUpdate();
        public void OnEnd();
    }
}