namespace PedroAurelio.RobotRun
{
    public interface IPoolable
    {
        public void ReleaseFromPool(bool triggerEffects);
    }
}
