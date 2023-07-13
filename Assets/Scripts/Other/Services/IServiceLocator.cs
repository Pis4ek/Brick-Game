namespace Services
{
    public interface IServiceLocator
    {
        public void Add<T>(T service) where T : IService;
        public void Remove<T>() where T : IService;
        public T GetService<T>() where T : IService;
        public bool TryGetService<T>(out T service) where T : IService;
        public bool HasSuchService<T>() where T : IService;
    }
}
