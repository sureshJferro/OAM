using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OAM.Core.Helpers
{
    public class Utility
    {
        #region Get Enum Display Name by Enum Value
        public static string GetEnumDisplayName(Enum value)
        {

            // Get the field information for the enum value
            FieldInfo field = value.GetType().GetField(value.ToString());

            // Get the Display attribute, if it exists
            DisplayAttribute attribute = field.GetCustomAttribute<DisplayAttribute>();

            // If the Display attribute exists, return the name; otherwise, use the enum value
            return attribute?.Name ?? value.ToString();
        }

        #endregion
    }
}
