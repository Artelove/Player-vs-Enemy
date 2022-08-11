namespace Save
{
    public interface ISaveSystem<T>
    {
        public void Save(T data);
        public T Load();
    }
}