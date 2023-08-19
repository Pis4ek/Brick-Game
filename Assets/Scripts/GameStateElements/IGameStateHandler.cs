using System;

public interface IGameStateHandler
{
    public event Action OnValueChangedEvent;

    public Type StateHandlerType { get; }
}
