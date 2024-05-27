namespace Xanatom.Handler;

public interface IHandler<T> where T : class
{
    public IHandler<T>? Next { get; set; }
    public void Handle(T data);
}