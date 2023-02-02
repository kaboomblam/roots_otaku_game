using System;
using UnityEngine;

namespace OtakuGameJam.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DisablePropertyAttribute : PropertyAttribute
    {
        public string ControlBooleanPropertyName { get; set; }
        public object ControlBooleanPropertyValue { get; set; }


        public DisablePropertyAttribute(string controlBooleanPropertyName, object controlBooleanPropertyValue)
        {
            ControlBooleanPropertyName = controlBooleanPropertyName;
            ControlBooleanPropertyValue = controlBooleanPropertyValue;
        }

        public DisablePropertyAttribute()
        {
            ControlBooleanPropertyName = null;
            ControlBooleanPropertyValue = null;
        }

    }
}