using BloodDonationApp.BusinessLogic.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.BusinessLogic.Services.Implementation
{
    public class DataShaper<T> : IDataShaper<T> where T : class
    {
        public PropertyInfo[] Properties { get; set; }
        public DataShaper()
        {
            Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
        public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString)
        {
            var requiredProperties = GetRequiredProperties(fieldsString);

            return FetchData(entities, requiredProperties);
        }
        public ExpandoObject ShapeData(T entity, string fieldsString)
        {
            var requiredProperties = GetRequiredProperties(fieldsString);

            return FetchEntityData(entity, requiredProperties);
        }
        private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString)
        {
            var requiredProperties = new List<PropertyInfo>();

            if (!string.IsNullOrWhiteSpace(fieldsString))
            {
                var fields = fieldsString.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var field in fields)
                {
                    var property = Properties.FirstOrDefault(property_info => 
                    property_info.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));

                    if (property == null) continue;

                    requiredProperties.Add(property);
                }
            }
            else
            { 
                requiredProperties = Properties.ToList();
            }
            return requiredProperties;
        }
        private IEnumerable<ExpandoObject> FetchData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedData = new List<ExpandoObject>();

            foreach (var entity in entities)
            {
                var shapedObj = FetchEntityData(entity, requiredProperties);
                shapedData.Add(shapedObj);
            }
            return shapedData;
        }
        private ExpandoObject FetchEntityData(T entity, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedObject = new ExpandoObject();

            foreach (var property in requiredProperties) 
            {
                var objPropertyValue = property.GetValue(entity);
                shapedObject.TryAdd(property.Name, objPropertyValue);
            }
            return shapedObject;
        }


       
    }
}
