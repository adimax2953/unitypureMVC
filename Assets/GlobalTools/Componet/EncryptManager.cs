using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Security.Cryptography;

public class EncryptManager
{
    public static string convertSha1(string text)
    {

        SHA1 sha1 = new SHA1CryptoServiceProvider();
        string resultSha1 = Convert.ToBase64String(sha1.ComputeHash(Encoding.Default.GetBytes(text)));

        return resultSha1;
    }
    public static string addNext(string text)
    {
        string result;

        result = string.Concat(getLength(text));

        return result;
    }
    private static int getLength(string text)
    {
        int length = text.Length >= 32 ? 32 : text.Length;
        return length -1;
    }
    public static string eatSpaceContent(string text)
    {

        char[] all_whitespaces = new char[] {
    // SpaceSeparator category
    '\u0020', '\u1680', '\u180E', '\u2000', '\u2001', '\u2002', '\u2003', 
    '\u2004', '\u2005', '\u2006', '\u2007', '\u2008', '\u2009', '\u200A', 
    '\u202F', '\u205F', '\u3000',
    // LineSeparator category
    '\u2028',
    // ParagraphSeparator category
    '\u2029',
    // Latin1 characters
    '\u0009', '\u000A', '\u000B', '\u000C', '\u000D', '\u0085', '\u00A0',
    // ZERO WIDTH SPACE (U+200B) & ZERO WIDTH NO-BREAK SPACE (U+FEFF)
    '\u200B', '\uFEFF','\n'
    };
        var result = text.Trim(all_whitespaces);
        return result;
    }
}