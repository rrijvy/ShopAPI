using ShopAPI.IRepositories;
using System;

namespace ShopAPI.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        int Complete();
    }
}
