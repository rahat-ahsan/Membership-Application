using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memberships.Extension
{
   
        public static class ReflectionExtensions
        {
            public static string GetPropertyValue<T>(this T item, string propertyName)
            {
                // Reflecting over the item and pulling out the property value
                return item.GetType().GetProperty(propertyName).GetValue(item, null).ToString();
            }
        }
    }
