﻿using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.BusinessLogic.Services.Implementation
{
    public class ActionService : IActionService
    {
        private readonly IUnitOfWork uow;
        public ActionService(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }
    }
}
