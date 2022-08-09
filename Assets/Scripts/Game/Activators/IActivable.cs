using System;

public interface IActivable
{
    void SetActivator(IActivator activator);
    void RemoveActivator(IActivator activator);
}