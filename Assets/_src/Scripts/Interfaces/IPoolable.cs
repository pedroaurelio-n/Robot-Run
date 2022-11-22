namespace PedroAurelio.HermitCrab
{
    public interface IPoolable
    {
        public void ReleaseFromPool(bool triggerEffects);
    }
}
