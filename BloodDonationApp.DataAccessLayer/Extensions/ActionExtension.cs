using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;  
using System.Threading.Tasks;
using System.Reflection;

namespace BloodDonationApp.DataAccessLayer.Extensions
{
    public static class ActionExtension
    {
        public static IQueryable<TransfusionAction> Filter(this IQueryable<TransfusionAction> actions, DateTime? minDate, DateTime? maxDate) =>
            actions.Where(a => (a.ActionDate >= minDate && a.ActionDate <= maxDate));

        public static IQueryable<TransfusionAction> Search(this IQueryable<TransfusionAction> actions, string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return actions;

            var lowercase = search.Trim().ToLower();
            return actions.Where(a => a.ActionName.ToLower().Contains(lowercase));
        }      
    }
}
