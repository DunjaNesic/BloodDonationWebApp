using BloodDonationApp.Domain.DomainModel;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BloodDonationApp.DataAccessLayer.Extensions
{
    public static class VolunteerExtension
    {
        public static IQueryable<Volunteer> Filter(this IQueryable<Volunteer> volunteers, DateTime? dateFreeFrom, DateTime? dateFreeToo, DateTime? dateOfBirth, Sex? sex, int redCrossID)
        {
            volunteers = volunteers.Where(v => (v.DateFreeFrom <= dateFreeFrom && v.DateFreeTo >= dateFreeToo && v.DateOfBirth <= dateOfBirth));

            if (sex.HasValue) volunteers = volunteers.Where(d => d.Sex == sex);
            if (redCrossID > 0) volunteers = volunteers.Where(d => d.RedCross.RedCrossID == redCrossID);

            return volunteers;
        }
        public static IQueryable<Volunteer> Search(this IQueryable<Volunteer> volunteers, string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return volunteers;

            var lowercase = search.Trim().ToLower();
            return volunteers.Where(v => v.VolunteerFullName.ToLower().Contains(lowercase));
        }
        public static IQueryable<Volunteer> Sort(this IQueryable<Volunteer> volunteers, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return volunteers.OrderBy(v => v.RedCross);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Volunteer).GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
                return volunteers.OrderBy(v => v.RedCross);

            return volunteers.OrderBy(orderQuery);
        }

    }
}
