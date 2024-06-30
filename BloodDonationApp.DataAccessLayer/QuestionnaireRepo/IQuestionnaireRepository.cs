﻿using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using Common.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.QuestionnaireRepo
{
    public interface IQuestionnaireRepository
    {
        Task<IEnumerable<Questionnaire>> GetAllForDonorAsync(string JMBG, QuestionnaireParameters questionnaireParameters, bool trackChanges);
        Task CreateQuestionnaireForDonor(string JMBG, Questionnaire questionnaire);
    }
}
