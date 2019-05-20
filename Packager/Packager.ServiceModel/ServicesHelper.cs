using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Packager.ServiceModel
{
    public class ServicesHelper
    {
        public virtual void GetServices<T>(Action<T> service, string endpointConfigurationName, Dictionary<string, string> namedParameters = null, string ns = null)
        {
            this.GetServices<T>(service: service, endpointConfigurationName: endpointConfigurationName, binding: null, uri: null, namedParameters: namedParameters, ns: ns);
        }

        public virtual void GetServices<T>(Action<T> service, Binding binding, string uri, Dictionary<string, string> namedParameters = null, string ns = null)
        {
            this.GetServices<T>(service: service, endpointConfigurationName: null, binding: binding, uri: uri, namedParameters: namedParameters, ns: ns);
        }

        protected void CloseService<T>(ChannelFactory<T> serviceFactory)
        {
            switch (serviceFactory.State)
            {
                case CommunicationState.Created:
                case CommunicationState.Faulted:
                    serviceFactory.Abort();
                    break;
                case CommunicationState.Opened:
                case CommunicationState.Opening:
                    try
                    {
                        serviceFactory.Close();
                    }
                    catch
                    {
                        serviceFactory.Abort();
                    }
                    break;
            }
        }

        protected virtual ChannelFactory<T> CreateService<T>(string endpointConfigurationName = null, Binding binding = null, EndpointAddress endpointAddress = null)
        {
            ChannelFactory<T> serviceFactory = null;
            if (binding != null && endpointAddress != null)
            {
                serviceFactory = new ChannelFactory<T>(binding, endpointAddress);
            }
            else if (!string.IsNullOrEmpty(endpointConfigurationName))
            {
                serviceFactory = new ChannelFactory<T>(endpointConfigurationName);
            }
            else
            {
                throw new ArgumentException("endpointConfigurationName or (binding and endpointAddress) is requerid.");
            }

            return serviceFactory;
        }

        protected virtual void GetServices<T>(Action<T> service, string endpointConfigurationName = null, Binding binding = null, string uri = null, Dictionary<string, string> namedParameters = null, string ns = null)
        {
            using (ChannelFactory<T> serviceFactory = this.CreateService<T>(endpointConfigurationName, binding, string.IsNullOrEmpty(uri) ? null : new EndpointAddress(uri)))
            {
                try
                {
                    T channel = serviceFactory.CreateChannel();
                    this.GetServices<T>(service: service, channel: channel, namedParameters: namedParameters, ns: ns);
                }
                finally
                {
                    this.CloseService(serviceFactory);
                }
            }
        }

        protected virtual void GetServices<T>(Action<T> service, T channel, Dictionary<string, string> namedParameters = null, string ns = null)
        {
            IContextChannel contextChannelService = channel as IContextChannel;
            if (contextChannelService != null && namedParameters != null)
            {
                using (OperationContextScope scope = OperationContextHelper.Current.CreateOperationContextScope(namedParameters, contextChannelService, ns))
                {
                    service(channel);
                }
            }
            else
            {
                service(channel);
            }
        }
    }
}
