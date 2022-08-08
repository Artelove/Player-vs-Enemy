public interface IActivable
{
    abstract void SetActivator(IActivator activator); 
    abstract void RemoveActivator(IActivator activator);
}