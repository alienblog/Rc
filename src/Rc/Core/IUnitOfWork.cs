using System;
using Rc.Models;

namespace Rc.Core
{
    public interface IUnitOfWork : IDisposable
    {
        RcContext Context { get; }
    }
}