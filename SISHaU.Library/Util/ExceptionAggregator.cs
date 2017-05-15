using System;
using System.Text;
using Integration.Base;

namespace SISHaU.Library.Util
{
    public class ExceptionAggregator
    {
        public ExceptionAggregator()
        {
            Message = new StringBuilder();
            StackTrace = new StringBuilder();
        }

        private StringBuilder Message { get; }

        private StringBuilder StackTrace { get; }

        public void AddException(Exception exc)
        {
            Message.AppendLine(exc.Message);
            StackTrace.AppendLine(exc.StackTrace);
        }

        public ErrorMessageType GetAggregatedException()
        {
            return new ErrorMessageType
            {
                Description = Message.ToString(),
                StackTrace = StackTrace.ToString()
            };
        }
    }
}
