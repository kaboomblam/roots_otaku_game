using System;
using UnityEngine;

namespace OtakuGameJam.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DisablePropertyAttribute : PropertyAttribute
    {

    }
}