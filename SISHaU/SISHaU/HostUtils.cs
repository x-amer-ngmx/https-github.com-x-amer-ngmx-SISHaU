using System;
using System.Reflection;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using SISHaU.DataAccess;
using SISHaU.Library.File;

namespace SISHaU
{
    public static class HostUtils
    {
        public static T InitHost<T>()
        {
            var typeT = typeof(T);
            var host = (T)Activator.CreateInstance(typeT);
            host.GetType().GetMethod("Init").Invoke(host, BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, null, null);

            InitializeServices();
            return host;
        }

        public static void InitializeServices()
        {/*
            var builder = new ContainerBuilder();

            var nhSessionFactory = new SessionFactoryManager().CreateSessionFactory();
            builder.Register(c => nhSessionFactory).As<ISessionFactory>().SingleInstance();
            builder.Register(c => c.Resolve<ISessionFactory>().GetHashCode());
            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession());

            var fileExchangeBuilder = new Builder();
            builder.RegisterInstance(fileExchangeBuilder).As<IBuilder>().SingleInstance();

            var cfg = builder.Build();*/

            //ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(cfg));
        }
    }
}