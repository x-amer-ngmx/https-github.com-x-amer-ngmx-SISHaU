using System;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using SISHaU.DataAccess.Model;

namespace SISHaU.Tests
{
    [TestClass]
    public class DatabaseTest
    {
        [TestMethod]
        public void TestDatabase()
        {
            var sessionFactory = ServiceLocator.Current.GetInstance<ISessionFactory>();
            using (var session = sessionFactory.OpenSession())
            {
                var loggingElement = new FileServiceLogEntity
                {
                    Id = Guid.NewGuid()
                };
                session.Save(loggingElement);
            }
        }
    }
}
