using System;
using UnityEngine;

namespace OtakuGameJam.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DisablePropertyControlAttribute : PropertyAttribute
    {
        public string ControlBooleanPropertyName { get; set; }
        public object ControlBooleanPropertyValue { get; set; }


        public DisablePropertyControlAttribute(string controlBooleanPropertyName, object controlBooleanPropertyValue)
        {
            ControlBooleanPropertyName = controlBooleanPropertyName;
            ControlBooleanPropertyValue = controlBooleanPropertyValue;
        }

        public DisablePropertyControlAttribute()
        {
            ControlBooleanPropertyName = null;
            ControlBooleanPropertyValue = null;
        }

    }
}