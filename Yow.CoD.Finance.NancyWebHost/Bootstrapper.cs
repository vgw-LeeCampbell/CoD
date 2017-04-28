﻿using System;
using System.Collections.Generic;
using System.Windows.Input;
using Nancy;
using Nancy.TinyIoc;
using Yow.CoD.Finance.Domain.Contracts;
using Yow.CoD.Finance.Domain.Model;
using Yow.CoD.Finance.Domain.Services;
using Yow.CoD.Finance.NancyWebHost.Adapters;

namespace Yow.CoD.Finance.NancyWebHost
{
    public sealed class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            //Numerous ways to deal with registration and decoration.
            //  But they can all be localized to this file.
            //  Nothing else should really be aware of the IoC/DI strategy.
            var loanRepo = new InMemoryRepository<Loan>();
            container.Register<IRepository<Loan>>(loanRepo);
            container.Register<IHandler<CreateLoanCommand>>((c, _) => new LoggingHandler<CreateLoanCommand>(c.Resolve<CreateLoanCommandHandler>()));
        }
    }
}