using BloodDonationApp.DataTransferObject.Questionnaires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Donors
{
    public class GetDonorQuestionnaireDTO
    {
        public string JMBG { get; set; } = string.Empty;
        public string DonorFullName { get; set; } = string.Empty;
        public string BloodType { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime? LastDonationDate { get; set; }
        public GetQuestionnaireDTO? Questionnaire { get; set; }
    }
}
