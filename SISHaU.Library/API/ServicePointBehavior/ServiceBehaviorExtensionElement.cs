using System;
using System.ServiceModel.Configuration;

namespace SISHaU.Library.API.ServicePointBehavior
{
    public class ServiceBehaviorExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType => typeof(ServiceBehavior);

        protected override object CreateBehavior()
        {
            return new ServiceBehavior();
        }
    }
}
