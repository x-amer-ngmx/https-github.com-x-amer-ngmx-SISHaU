using System.Collections.Generic;
using FluentAssertions;
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

            var loggingElements = new List<FileServiceLogEntity>
            {
                { new FileServiceLogEntity("Новыми данными ширится таблица...") },
                { new FileServiceLogEntity("Новыми данными полнится таблица!") },
                { new FileServiceLogEntity("Новые данные примкнули к нам!") }
            };

            using (var session = sessionFactory.OpenSession())
            {
                //Сохраняю элементы
                foreach (var loggingElement in loggingElements)
                {
                    session.Save(loggingElement);
                }
                
                session.Flush();
                session.Close();
            }

            loggingElements.Clear();

            using (var session = sessionFactory.OpenSession())
            {
                loggingElements.AddRange(session.QueryOver<FileServiceLogEntity>().List());
                session.Close();
            }

            loggingElements.Should().NotBeEmpty();
        }
    }
}
