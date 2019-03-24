﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Transports.WCF.Service
{
    [ServiceContract]
    public interface ITransportsService
    {
        [OperationContract(IsOneWay = true)]
        void CreateDriver(string driver);
    }
}
