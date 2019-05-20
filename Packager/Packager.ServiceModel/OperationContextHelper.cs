using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace Packager.ServiceModel
{
    internal class OperationContextHelper
    {
        private const string DEFAULT_NS = "9CB09E7B-2128-44A4-8478-10AAE13BB893";

        private static OperationContextHelper _current;

        private OperationContextHelper()
        {
        }

        internal static OperationContextHelper Current
        {
            get
            {
                return OperationContextHelper._current ?? (OperationContextHelper._current = new OperationContextHelper());
            }
        }

        internal OperationContextScope CreateOperationContextScope(Dictionary<string, string> namedParameters, IContextChannel service, string ns = null)
        {
            if (namedParameters != null)
            {
                ns = ns ?? OperationContextHelper.DEFAULT_NS;
                OperationContextScope scope = new OperationContextScope(service);

                foreach (KeyValuePair<string, string> parameter in namedParameters)
                {
                    if (OperationContext.Current.OutgoingMessageHeaders.Any(a => parameter.Key.Equals(a.Name) && ns.Equals(a.Namespace)))
                    {
                        OperationContext.Current.OutgoingMessageHeaders.RemoveAll(parameter.Key, ns);
                    }

                    MessageHeader message = MessageHeader.CreateHeader(parameter.Key, ns, parameter.Value);
                    OperationContext.Current.OutgoingMessageHeaders.Add(message);
                }

                return scope;
            }
            else
            {
                return null;
            }
        }

        internal Dictionary<string, string> GetHeaderMessage(string ns = null)
        {
            if (OperationContext.Current != null && OperationContext.Current.IncomingMessageHeaders.Count > 0)
            {
                ns = ns ?? OperationContextHelper.DEFAULT_NS;
                Dictionary<string, string> namedParameters = new Dictionary<string, string>();
                foreach (MessageHeaderInfo messageHeader in OperationContext.Current.IncomingMessageHeaders)
                {
                    if (ns.Equals(messageHeader.Namespace))
                    {
                        string value = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>(messageHeader.Name, messageHeader.Namespace);
                        if (value != null)
                        {
                            namedParameters.Add(messageHeader.Name, value);
                        }
                    }
                }

                return namedParameters.Count > 0 ? namedParameters : null;
            }
            else
            {
                return null;
            }
        }
    }
}
