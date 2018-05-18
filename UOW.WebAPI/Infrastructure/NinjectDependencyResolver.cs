using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using UOW.BLL.Abstruct;
using UOW.BLL.Concrete;

namespace UOW.WebAPI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IProductService>().To<ProductService>();
        }
    }
}