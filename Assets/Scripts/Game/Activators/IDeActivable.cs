public interface IDeActivable
{
    abstract void SetDeActivator(IDeActivator deActivator);
    abstract void RemoveDeActivator(IDeActivator deActivator);
}