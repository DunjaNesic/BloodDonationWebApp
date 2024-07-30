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
            ActionID = action.ActionID,
            ActionName = action.ActionName,
            ActionDate = action.ActionDate, 
            ExactLocation = action.ExactLocation,
            ActionTimeFromTo = action?.ActionTimeFromTo ?? "Nepoznato je vreme odrzavanja akcije",
            PlaceName = action?.Place.PlaceName ?? "Nepoznat je grad u kom se odrzava akcija"
        };
        public TransfusionAction FromDto(GetTransfusionActionDTO action)
        {
            throw new NotImplementedException();
        }      
    }
}
