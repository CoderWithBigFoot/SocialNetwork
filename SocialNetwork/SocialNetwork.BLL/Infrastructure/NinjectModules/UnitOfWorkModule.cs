using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.DAL.Interfaces;
using SocialNetwork.DAL.Repositories;
using Ninject.Modules;
namespace SocialNetwork.BLL.Infrastructure.NinjectModules
{
   public class UnitOfWorkModule : NinjectModule
    {
        private string connectionString;
        public UnitOfWorkModule(string connectionString) {
            this.connectionString = connectionString;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
