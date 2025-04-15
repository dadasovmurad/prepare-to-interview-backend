using System;
using System.ComponentModel;
using System.Reflection;

public static class EnumHelper
{
    public static bool TryParseFromDescription<TEnum>(string description, out TEnum result) where TEnum : struct
    {
        foreach (var field in typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var attr = field.GetCustomAttribute<DescriptionAttribute>();
            if (attr != null && attr.Description.Equals(description, StringComparison.OrdinalIgnoreCase))
            {
                var value = field.GetValue(null);
                if (value is TEnum enumValue)
                {
                    result = enumValue;
                    return true;
                }
            }
        }

        result = default;
        return false;
    }
    public static bool TryParseEnumOrDescription<TEnum>(string value, out TEnum result) where TEnum : struct, Enum
    {
        return Enum.TryParse(value, true, out result) || TryParseFromDescription(value, out result);
    }

}