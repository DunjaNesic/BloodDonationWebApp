﻿using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.QuestionRepo
{
    public interface IQuestionRepository : IRepository<Question>
    {
    }
}
