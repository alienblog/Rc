using System;
using Rc.Models;
using Rc.Core.Impl;

namespace Rc.Core
{
    public interface IUnitOfWork : ISingletonDependency, IDisposable
    {
        RcContext Context { get; }
    }
}