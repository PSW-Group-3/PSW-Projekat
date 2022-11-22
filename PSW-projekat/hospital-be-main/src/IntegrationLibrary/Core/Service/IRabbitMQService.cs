﻿using IntegrationLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Service
{
    public interface IRabbitMQService
    {
        void Send();
        List<Model.News> Recive(List<BloodBank> bloodBanks);
    }
}
