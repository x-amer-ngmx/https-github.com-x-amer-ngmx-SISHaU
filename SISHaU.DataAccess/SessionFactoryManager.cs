using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using SISHaU.DataAccess.Definition;

namespace SISHaU.DataAccess
{
    public class SessionFactoryManager
    {
        public ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(new FirebirdConfiguration().Raw("hbm2ddl.keywords", "auto-quote")
                    .ConnectionString(cs => cs.FromConnectionStringWithKey("ConnectionString")))
                .Mappings(mps => mps.FluentMappings.AddFromAssemblyOf<EntityDto>())
                .ExposeConfiguration(ex =>
                {
                    new SchemaUpdate(ex).Execute(true, true);
                }).ExposeConfiguration(SchemaMetadataUpdater.QuoteTableAndColumns)
                .BuildConfiguration().BuildSessionFactory();
        }
    }
}
