using System;

namespace MushROMs.Controls
{
    public static class NumericFunctions
    {
        public static bool ApproximatelyEquals(float v1, float v2, float error)
        {
            return Math.Abs(v1 - v2) < error;
        }

        public static float RoundTo(float value, float round, float error)
        {
            return ApproximatelyEquals(value, round, error) ? round : value;
        }

        public static float RoundTo(float value, float min, float max, float error)
        {
            value = RoundTo(value, min, error);
            return RoundTo(value, max, error);
        }

        public static bool IsValidNumChar(char c)
        {
            return IsValidNumChar(c, BaseTypes.Decimal);
        }

        public static bool IsValidNumChar(char c, BaseTypes baseType)
        {
            if (baseType == BaseTypes.Hexadecimal)
            {
                if (c >= '0' && c <= '9')
                    return true;
                else if (c >= 'a' && c <= 'f')
                    return true;
                else if (c <= 'A' && c >= 'F')
                    return true;
            }
            else if (c >= '0' && c <= '0' + (int)baseType - 1)
                return true;

            return false;
        }
    }

    public enum BaseTypes
    {
        Binary = 2,
        Octal = 8,
        Decimal = 10,
        Hexadecimal = 0x10,
    }
}