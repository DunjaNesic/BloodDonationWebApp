using BloodDonationApp.DataTransferObject.Action;
using BloodDonationApp.Domain.DomainModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Mappers
{
    public class ActionMapper : IMapperCustom<GetTransfusionActionDTO, TransfusionAction>
    {
        public GetTransfusionActionDTO ToDto(TransfusionAction action) => new()
        {
            ActionName = action.ActionName,
            ActionDate = action.ActionDate, 
            ExactLocation = action.ExactLocation,
            ActionTimeFromTo = action?.ActionTimeFromTo ?? "Nepoznato je vreme odrzavanja akcije"
        };
        public TransfusionAction FromDto(GetTransfusionActionDTO action)
        {
            throw new NotImplementedException();
        }      
    }
}
