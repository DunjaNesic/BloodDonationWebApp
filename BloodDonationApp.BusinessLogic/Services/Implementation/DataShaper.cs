using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.Domain.CustomModel;
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
        public IEnumerable<ShapedCustomExpando> ShapeData(IEnumerable<T> entities, string fieldsString)
        {
            var requiredProperties = GetRequiredProperties(fieldsString);

            return FetchData(entities, requiredProperties);
        }
        public ShapedCustomExpando ShapeData(T entity, string fieldsString)
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
        private IEnumerable<ShapedCustomExpando> FetchData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedData = new List<ShapedCustomExpando>();

            foreach (var entity in entities)
            {
                var shapedObj = FetchEntityData(entity, requiredProperties);
                shapedData.Add(shapedObj);
            }
            return shapedData;
        }
        private ShapedCustomExpando FetchEntityData(T entity, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedObject = new ShapedCustomExpando();

            foreach (var property in requiredProperties) 
            {
                var objPropertyValue = property.GetValue(entity);
                shapedObject.CustomExpando.Add(property.Name, objPropertyValue);
            }

            var idProperty = entity.GetType().GetProperties().FirstOrDefault(p => p.Name.EndsWith("ID"));

            if (idProperty != null)
            {
                var idPropertyValue = idProperty.GetValue(entity);
                shapedObject.Id = Convert.ToInt32(idPropertyValue);
            }
            else
            {
                throw new InvalidOperationException("ID property not found in the shaped object.");
            }

            return shapedObject;
        }


       
    }
}
