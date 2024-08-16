using BloodDonationApp.DataTransferObject;
using BloodDonationApp.DataTransferObject.Donors;
using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Mappers
{
    public class DonorMapper : IMapperCustom<GetDonorDTO, Donor>
    {
        private readonly QuestionnaireMapper _questionnaireMapper;

        public DonorMapper(QuestionnaireMapper questionnaireMapper)
        {
            _questionnaireMapper = questionnaireMapper;
        }

        private static readonly Dictionary<BloodType, string> BloodTypeMappings = new()
        {
            { BloodType.APozitivna, "A+" },
            { BloodType.ANegativna, "A-" },
            { BloodType.BPozitivna, "B+" },
            { BloodType.BNegativna, "B-" },
            { BloodType.ABPozitivna, "AB+" },
            { BloodType.ABNegativna, "AB-" },
            { BloodType.OPozitivna, "O+" },
            { BloodType.ONegativna, "O-" }
        };
        public GetDonorDTO ToDto(Donor donor) => new()
        {
            JMBG = donor.JMBG,
            DonorFullName = donor.DonorFullName,
            PlaceName = donor.Place?.PlaceName ?? "Nepoznat grad",
            BloodType = BloodTypeMappings[donor.BloodType],
            IsActive = donor.IsActive,
            LastDonationDate = donor.LastDonationDate,
            CallsToDonate = donor.CallsToDonate?.Select(call => new CallsToDonorDTO{
                AcceptedTheCall = call.AcceptedTheCall,
                ShowedUp = call.ShowedUp
            }).ToList()
        };

        public GetDonorQuestionnaireDTO ToDto2(Donor donor, Questionnaire questionnaire) => new()
        {
            JMBG = donor.JMBG,
            DonorFullName = donor.DonorFullName,
            BloodType = BloodTypeMappings[donor.BloodType],
            IsActive = donor.IsActive,
            LastDonationDate = donor.LastDonationDate,  
            Questionnaire = _questionnaireMapper.ToDto(questionnaire)
        };


        public Donor FromDto(GetDonorDTO source)
        {
            throw new NotImplementedException();
        }
    }
}
