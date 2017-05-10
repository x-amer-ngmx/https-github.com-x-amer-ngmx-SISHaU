using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using FluentNHibernate.Mapping;

namespace SISHaU.DataAccess.Definition
{
    public class MapAction<T> : ClassMap<T>
    {
        private string TableName { get; set; }
        private string GetIdName => $"{TableName}_id";

        public MapAction(string schemaName, string tableName, Expression<Func<T, object>> memberExpression = null) : base()
        {
            GenerateSchemaAction();

            if (!string.IsNullOrEmpty(schemaName))
            {
                Schema(schemaName);
            }

            TableName = tableName;
            Table(TableName);
            //Пропускаем все не GUID идентификаторы
            if (null == memberExpression) return;
            GenerateGuidIdentity(Id(memberExpression/*, GetIdName*/));
        }

        internal void SetThisColumnKey<TU>(OneToManyPart<TU> column)
        {
            column.KeyColumn(GetIdName);
        }

        internal void GenerateGuidIdentity(IdentityPart identityPart)
        {
            /*1) Или CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
                 и генерировать uuid так - select * from uuid_generate_v4()*/
            /*2) Или CREATE EXTENSION pgcrypto; и вызов - select gen_random_uuid()*/
            identityPart.GeneratedBy.Guid()/*
                .Length(36)
                .GeneratedBy
                .Guid()
                .Not
                .Nullable()
                .Default("public.gen_random_uuid()")*/;
        }
        private void ResolveAction(string action)
        {
            var rxThisAction = new Regex($"-{(typeof(T)).Name}", RegexOptions.IgnoreCase);
            var rxAllSpreadAction = new Regex(@"-\*");

            var thisAction = rxThisAction.IsMatch(action) || rxAllSpreadAction.IsMatch(action);

            //По шаблону always true
            if (!thisAction)
            {
                throw new SyntaxErrorException($"список правил должен включать хотя-бы '*' если нет конкретного определения для этого класса ({(typeof(T)).Name})");
            }

            var ac = "";
            foreach (var a in action.Split(';'))
            {

                if (rxAllSpreadAction.IsMatch(a))
                {
                    ac = a.Split('-').First();
                }

                if (false == (rxThisAction.IsMatch(a)))
                {
                    continue;
                }

                ac = a.Split('-').First();
                break;
            }

            SchemaAction.Custom(ac);
        }

        public void GenerateSchemaAction()
        {
            var action = ConfigurationManager.AppSettings["generate_tables_action"] ?? "none-" + typeof(T).Name.ToLower();
            bool acts;
            if (null != ConfigurationManager.AppSettings["generate_tables"] && (bool.TryParse(ConfigurationManager.AppSettings["generate_tables"], out acts) && acts))
                ResolveAction(action);
        }
    }
}
