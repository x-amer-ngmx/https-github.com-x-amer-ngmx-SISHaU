using System;
using System.Reflection;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;
using SISHaU.Library.API;

namespace SISHaU.Web
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
            ServiceRegistrator.RegisterServices(builder);
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(builder.Build()));*/
        }

    }
}