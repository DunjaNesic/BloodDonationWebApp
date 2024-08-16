using BloodDonationApp.Domain.DomainModel;
using Common.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace BloodDonationApp.DataAccessLayer.Extensions
{
    public static class DonorExtension
    {
        public static IQueryable<Donor> Filter(this IQueryable<Donor> donors, DateTime nextDonationDate, bool? isActive, BloodType? bloodType, Sex? sex, int placeID)
        {
            donors = donors.Where(d =>
                (d.LastDonationDate == null || d.LastDonationDate <= nextDonationDate.AddMonths(-4))
            );

            if (isActive.HasValue) donors = donors.Where(d => d.IsActive == isActive.Value);
            if (bloodType.HasValue) donors = donors.Where(d => d.BloodType == bloodType);
            if (sex.HasValue) donors = donors.Where(d => d.Sex == sex);
            if (placeID > 0) donors = donors.Where(d => d.Place.PlaceID == placeID);

            return donors;
        }
        public static IQueryable<Donor> Search(this IQueryable<Donor> donors, string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return donors;

            var lowercase = search.Trim().ToLower();
            return donors.Where(a => a.DonorFullName.ToLower().Contains(lowercase));
        }

        public static IQueryable<Donor> Sort(this IQueryable<Donor> donors, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return donors.OrderBy(d => d.BloodType);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Donor).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(property =>
                    property.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction},");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            if (string.IsNullOrWhiteSpace(orderQuery))
                return donors.OrderBy(d => d.BloodType);

            return donors.OrderBy(orderQuery);
        }


    }
}
