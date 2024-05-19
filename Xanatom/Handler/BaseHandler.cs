using Xanatom.Model;

namespace Xanatom.Handler;

public abstract class BaseHandler<T> : IHandler<T> where T : class
{
    public IHandler<T>? Next { get; set; }
    public abstract void Handle(T data);
}

public class BankCardNumberAuthenticationHandler : BaseHandler<XanatomHandlerData>
{
    public override void Handle(XanatomHandlerData data)
    {
        throw new NotImplementedException();
    }
}

public class BankCardOwnerNameAuthenticationHandler : BaseHandler<XanatomHandlerData>
{
    public override void Handle(XanatomHandlerData data)
    {
        throw new NotImplementedException();
    }
}

public class BankCardExpireDateAuthenticationHandler : BaseHandler<XanatomHandlerData>
{
    public override void Handle(XanatomHandlerData data)
    {
        throw new NotImplementedException();
    }
}