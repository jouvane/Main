using Packager.ServiceModel.WCF.Spec.Test.DataContracts;
using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Packager.ServiceModel.WCF.Spec.Test
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetAuthenticationFullName();

        [OperationContract]
        string GetData(int value);
    }
}