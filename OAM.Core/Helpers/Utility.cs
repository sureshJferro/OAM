﻿using System;
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

        #region Convert to bool
        public static bool GetBool(object value)
        {
            bool result = false;
            if (value != null)
            {
                bool.TryParse(value.ToString(), out result);
            }
            return result;
        }
        #endregion

        #region Convert to guid
        public static Guid GetGuid(object value)
        {
            Guid result = new Guid();
            if (value != null)
            {
                Guid.TryParse(value.ToString(), out result);
            }
            return result;
        }
        #endregion
    }
}
