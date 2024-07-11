using System;
using System.Collections.Generic;

namespace InternetMessage.Encoding;

public class QuotedPrintableDecoder : Decoder
{
    public override string[] TransferEncodingNames { get; } = new[] { "quoted-printable" };
    public override string[] StringEncodingNames { get; } = new[] { "q" };

    public override byte[] Decode(string encodedString)
    {
        var bytes = new List<byte>();
        for (int i = 0; i < encodedString.Length; i++)
        {
            var c = encodedString[i];
            if (c == '=')
            {
                if (i == encodedString.Length - 1)
                    throw new FormatException("Encoded string ends with an = sign");
                var c1 = encodedString[i + 1];
                if (IsCrLf(c1))
                {
                    if (i == encodedString.Length - 2)
                    {
                        i++;
                    }
                    else
                    {
                        var c2 = encodedString[i + 2];
                        if (!IsCrLf(c2))
                            i++;
                        else
                            i += 2;
                    }
                }
                else if (IsHex(c1))
                {
                    if (i == encodedString.Length - 2)
                        throw new FormatException($"Incomplete hexadecimal sequence at end");
                    var c2 = encodedString[i + 2];
                    if (!IsHex(c2))
                        throw new FormatException($"Invalid character '{c2}' after = sign at {i + 2}");
                    bytes.Add(Convert.ToByte(new string(new[] { c1, c2 }), 16));
                    i += 2;
                }
                else
                    throw new FormatException($"Invalid character '{c1}' after = sign at {i + 1}");
            }
            else
            {
                bytes.Add((byte)c);
            }
        }
        return bytes.ToArray();
    }

    private static bool IsHex(char c) => c >= '0' && c <= '9' || c >= 'A' && c <= 'F';
    private static bool IsCrLf(char c) => c == '\n' || c == '\r';
}