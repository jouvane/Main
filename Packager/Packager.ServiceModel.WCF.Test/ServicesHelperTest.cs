using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Packager.ServiceModel.WCF.Spec.Test;
using System.ServiceModel;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.Collections.Generic;
using Packager.ServiceModel.WCF.Spec.Test.DataContracts;
using System.ServiceModel.Web;

namespace Packager.ServiceModel.WCF.Test
{
    public class WCFServicesHelper : ServicesHelper
    {
        private static WCFServicesHelper _current;

        public static string EndpointConfig
        {
            get
            {
                return ConfigurationManager.AppSettings["Endpoint-Config"];

            }

        }

        public const string FullName = "Geovane Alves Simões";
        public const string Phone = "31987423236";
        public const string Company = "NetBlade";
        public const string Identity = "geovane.simoes";

        public static WCFServicesHelper Current
        {
            get
            {
                return WCFServicesHelper._current ?? (WCFServicesHelper._current = new WCFServicesHelper());
            }
        }

        public ChannelFactory<IService1> ChannelFactoryIService1
        {
            get
            {
                return this.CreateService<IService1>(WCFServicesHelper.EndpointConfig);
            }
        }

        public void GetService1(Action<IService1> action, bool sendNamedParameters = true)
        {
            Dictionary<string, string> namedParameters = null;
            if (sendNamedParameters)
            {
                namedParameters = new Dictionary<string, string>();
                namedParameters.Add("FullName", WCFServicesHelper.FullName);
                namedParameters.Add("Phone", WCFServicesHelper.Phone);
                namedParameters.Add("Company", WCFServicesHelper.Company);
                namedParameters.Add("Identity", WCFServicesHelper.Identity);
            }

            this.GetServices<IService1>(action, WCFServicesHelper.EndpointConfig, namedParameters);
        }
    }

    [TestClass]
    public class ServicesHelperTest
    {
        [TestMethod]
        public void GetServicesTest()
        {
            ClientSection client = ConfigurationManager.GetSection("system.serviceModel/client") as ClientSection;
            ChannelEndpointElementCollection endpoints = client.Endpoints;
            ChannelEndpointElement endpoint = null;

            foreach (ChannelEndpointElement item in endpoints)
            {
                if (WCFServicesHelper.EndpointConfig.Equals(item.Name))
                {
                    endpoint = item;
                    break;
                }
            }

            using (ChannelFactory<IService1> endpointServices = WCFServicesHelper.Current.ChannelFactoryIService1)
            {
                Assert.AreEqual(endpointServices.Endpoint.Address.Uri, endpoint.Address);
            }
        }

        [TestMethod]
        public void GetServicesSendNamedParametersTest()
        {
            string fullName = null;
            WCFServicesHelper.Current.GetService1(action =>
            {
                fullName = action.GetAuthenticationFullName();
            });

            Assert.AreEqual(WCFServicesHelper.FullName, fullName);
        }
    }
}
