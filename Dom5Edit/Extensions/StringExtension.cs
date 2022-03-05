﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class StringExtension
{
    public static bool IsNumeric(this string s, bool floatingPt = true)
    {
        s = s.Trim();
        if (floatingPt)
        {
            return double.TryParse(s, out _);
        }
        else
        {
            return int.TryParse(s, out _);
        }
    }

    public static bool EqualsIgnoreCase(this string s, string rhs)
    {
        if (s == null)
        {
            return rhs == null;
        }
        if (rhs == null) return false;
        return s.ToLower().Equals(rhs.ToLower());
    }

    /*
     * Takes a string and extracts the target from the string. Returns the original string, minus the target.
     * Example: 
     *  string s = "apple | orange";
     *  string ret = s.Extract("|");
     *  ret now equals "apple  orange" (note two spaces, it only extracted the line)
     */
    public static bool Extract(this string s, string target, out string result)
    {
        if (s == null)
        {
            result = s;
            return false;
        }
        int index = s.IndexOf(target);
        if (index == -1)
        {
            result = s;
            return false;
        }
        result = s.Remove(index, target.Length);
        return true;
    }

    /*
     * Splits a string, but returns a list that can be foreach'd over more optimally, no array shenanigans.
     */
    public static IEnumerable<string> Split(this string line, string delim, char escapeChar)
    {
        int index = -1;
        string replace = escapeChar + delim;
        // Parse into delimited elements
        while ((index = line.IndexOf(delim, index + 1)) != -1)
        {
            if (index > 0 && line[index - 1] == escapeChar)
            { // Was escaped
                continue;
            }
            yield return line.Substring(0, index).Replace(replace, delim);
            line = line.Substring(index + 1);
            index = -1;
        }
        yield return line.Replace(replace, delim);
    }

    public static string ToUnixEndings(this string str)
    {
        return str.Replace("\r\n", "\n");
    }

    public static string ToUpperOrEmpty(this string str)
    {
        if (str == null) return string.Empty;
        return str.ToUpper();
    }

    public static bool TrySubstringFromStart(this string src, string item, out string result)
    {
        int index = src.IndexOf(item);
        if (index == -1)
        {
            result = src;
            return false;
        }
        index += item.Length;
        if (index == src.Length)
        {
            result = string.Empty;
            return true;
        }
        result = src.Substring(index);
        return true;
    }

    public static string SubstringFromStart(this string src, string item)
    {
        TrySubstringFromStart(src, item, out string result);
        return result;
    }

    public static bool TrySubstringFromEnd(this string src, string item, out string result)
    {
        int index = src.LastIndexOf(item);
        if (index == -1)
        {
            result = src;
            return false;
        }
        if (index == 0)
        {
            result = string.Empty;
            return true;
        }
        result = src.Substring(0, index);
        return true;
    }

    public static string SubstringFromEnd(this string src, string item)
    {
        string result;
        TrySubstringFromEnd(src, item, out result);
        return result;
    }

    public static bool TryTrimStart(this string src, string item, out string result)
    {
        if (!src.StartsWith(item))
        {
            result = src;
            return false;
        }
        if (src.Length == item.Length)
        {
            result = string.Empty;
            return true;
        }
        result = src.Substring(item.Length);
        return true;
    }

    public static string TrimStart(this string src, string item)
    {
        TryTrimStart(src, item, out string result);
        return result;
    }

    public static bool TryTrimEnd(this string src, string item, out string result)
    {
        if (!src.EndsWith(item))
        {
            result = src;
            return false;
        }
        if (src.Length == item.Length)
        {
            result = string.Empty;
            return true;
        }
        result = src.Substring(0, src.Length - item.Length);
        return true;
    }

    public static string TrimEnd(this string src, string item)
    {
        TryTrimEnd(src, item, out string result);
        return result;
    }

    public static byte[] ToBytes(this string str)
    {
        return Encoding.ASCII.GetBytes(str);
    }

    public static bool ToEnum<T>(this string str, out T e) where T : struct, IComparable, IConvertible
    {
        try
        {
            e = (T)Enum.Parse(typeof(T), str, true);
            return true;
        }
        catch (Exception)
        {
            e = default;
            return false;
        }
    }
}