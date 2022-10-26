﻿using IntegrationLibrary.Core.Model.MailRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Service
{
    public interface IEmailService
    {
        void SendEmailAsync(MailRequest mailRequest);

    }
}
