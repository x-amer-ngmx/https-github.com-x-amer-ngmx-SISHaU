using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using AutoMapper;
using Integration.Base;
using SISHaU.Library.API.Interfaces;
using SISHaU.Library.Util;

namespace SISHaU.Library.API
{
    public class GisIntegrationService<T, TU> : GisBinder, IGisIntegrationService<T, TU>
        where T : class
        where TU : class
    {
        private readonly object _abstractProxy;
        private readonly object _abstractProxyAsync;
        private readonly Dictionary<string, MethodInfo> _methods;
        private readonly Dictionary<string, MethodInfo> _methodsAsync;

        public GisIntegrationService(MapperConfiguration mapConfig = null)
        {
            /*В регистраторе определён только асинхронный сервис*/
            if (!IsSimpleService())
            {
                _abstractProxy = GetProxy<T>();
                _methods = typeof(T).GetMethods()
                    .Where(t => t.ReturnType.IsAssignableFrom(typeof(ResultHeader)))
                    .ToDictionary(t => t.GetParameters().ElementAt(1).ParameterType.Name, t => t);
            }

            _abstractProxyAsync = GetProxy<TU>();

            _methodsAsync = typeof(TU).GetMethods()
                .Where(t => t.ReturnType.IsAssignableFrom(typeof(ResultHeader)))
                .ToDictionary(t => t.GetParameters().ElementAt(1).ParameterType.Name, t => t);

            InitMapper(mapConfig);
        }

        /*В регистраторе определён только асинхронный сервис*/
        /*Происходит проверка, заглушка ли это или это полноценный сервис*/
        private static bool IsSimpleService()
        {
            return typeof(T) == typeof(StubService);
        }

        /// <summary>
        /// Завершает работу внешнего асинхронного запроса - передаёт управление наверх
        /// </summary>
        /// <returns></returns>
        public TS ProcessAsyncMessageState<TS>(getStateRequest request) where TS : class
        {
            var method = _methodsAsync[request.GetType().Name];
            var attrs = new[] { GetHeader(method), request, null };

            try
            {
                method.Invoke(_abstractProxyAsync, attrs);
            }
            catch (TargetInvocationException tiExc)
            {
                return ProcessError<TS>(tiExc);
            }

            return null == attrs.Last() ? null : UtilMapper.Map<TS>(ClearEntityFields(attrs.Last()));
        }

        public virtual TS ProcessRequest<TS>(object request) where TS : class
        {
            return IsSimpleService() ? ProcessMethodAsync<TS>(request) : ProcessMethod<TS>(request);
        }

        public TS BeginProcessRequest<TS>(object request) where TS : class
        {
            return ProcessMethodAsync<TS>(request, false);
        }

        private TS ProcessMethod<TS>(object request) where TS : class
        {
            var method = _methods[request.GetType().Name];

            var attrs = new[] { GetHeader(method), request, null };

            try
            {
                method.Invoke(_abstractProxy, attrs);
            }
            catch (Exception exc)
            {
                return ProcessError<TS>(exc);
            }

            var retInstance = attrs.Last();

            SetPropValue(retInstance, "Signature", null);
            SetPropValue(retInstance, "Id", TransportGuid);

            return retInstance as TS;
        }

        private TS ProcessMethodAsync<TS>(object request, bool async2Sync = true) where TS : class
        {
            var method = _methodsAsync[request.GetType().Name];

            var retInstance = new AckRequest();
            var attrs = new[] { GetHeader(method), request, retInstance };

            try
            {
                method.Invoke(_abstractProxyAsync, attrs);
            }
            catch (Exception exc)
            {
                return ProcessError<TS>(exc);
            }

            var messageGuid = GetMessageGuid(attrs.Last());

            if (!async2Sync)
                return new getStateRequest { MessageGUID = messageGuid } as TS;

            var result = ProcessAsyncMessage(messageGuid);

            return result == null ? null : UtilMapper.Map<TS>(result);
        }

        private object ProcessAsyncMessage(string messageId)
        {
            object stateResult;
            var request = new getStateRequest { MessageGUID = messageId };
            var attrs = new[] { null, request, (object)null };
            var method = _methodsAsync[request.GetType().Name];

            var sleepTimerOut = 500;
            var tryes = 0;

            while (true)
            {
                attrs[0] = GetHeader(method);

                method.Invoke(_abstractProxyAsync, attrs);

                stateResult = attrs.Last();

                if (GetPropValue<sbyte>(stateResult, "RequestState") == 3 || tryes > 5) break;

                Thread.Sleep(sleepTimerOut); sleepTimerOut += 500; tryes++;
            }

            /*
             * Хотел написать сообщение об окончании количества попыток получения ответа
             * if (GetPropValue<sbyte>(stateResult, "RequestState") != 3 && tryes > 5)
            {
                ProcessError<object>(new Exception(""));
            }*/

            return ClearEntityFields(stateResult);
        }

        public TS ProcessError<TS>(Exception exc)
        {
            var ret = Activator.CreateInstance<TS>();
            var error = CollectInnerExceptions(exc);
            SetPropValue(ret, "ErrorMessage", error);
            DispatchError(error);
            return ret;
        }

        private string GetMessageGuid(object retInstance)
        {
            return (retInstance as AckRequest)?.Ack.MessageGUID;
        }

        private object ClearEntityFields(object retInstance)
        {
            if (null == retInstance) return null;
            SetPropValue(retInstance, "Signature", null);
            SetPropValue(retInstance, "Id", TransportGuid);

            return retInstance;
        }

        private static ErrorMessageType CollectInnerExceptions(Exception exc)
        {
            var exceptionAggregator = new ExceptionAggregator();

            var excAgg = exc;
            while (true)
            {
                exceptionAggregator.AddException(excAgg);
                if (null == excAgg.InnerException) break;
                excAgg = excAgg.InnerException;
            }

            return exceptionAggregator.GetAggregatedException();
        }
    }
}
