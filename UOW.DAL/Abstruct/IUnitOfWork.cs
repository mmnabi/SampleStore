using System;
using System.Threading.Tasks;

namespace UOW.DAL.Abstruct
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        int Complete();
        Task<int> CompleteAsync();
    }
}