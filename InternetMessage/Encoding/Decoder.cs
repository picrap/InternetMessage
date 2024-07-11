
using System;
using System.Collections.Generic;
using System.Linq;

namespace InternetMessage.Encoding;

public abstract class Decoder
{
    // ByteDecoder comes first, we use Decoder[0] below
    private static readonly Decoder[] Decoders = new Decoder[] { new ByteDecoder(), new Base64Decoder(), new QuotedPrintableDecoder() };

    private static readonly IDictionary<string, Decoder> KnownTransferDecoders =
        Decoders.SelectMany(d => d.TransferEncodingNames.Select(n => new { n, d })).ToDictionary(d => d.n, d => d.d, StringComparer.InvariantCultureIgnoreCase);

    private static readonly IDictionary<string, Decoder> KnownStringDecoders =
        Decoders.SelectMany(d => d.StringEncodingNames.Select(n => new { n, d })).ToDictionary(d => d.n, d => d.d, StringComparer.InvariantCultureIgnoreCase);

    public abstract string[] TransferEncodingNames { get; }
    public abstract string[] StringEncodingNames { get; }

    public abstract byte[] Decode(string encodedString);

    public static Decoder FindTransferDecoder(string transferEncodingName)
    {
        if (transferEncodingName is null)
            return Decoders[0];
        KnownTransferDecoders.TryGetValue(transferEncodingName, out var decoder);
        return decoder ?? Decoders[0];
    }

    public static Decoder FindStringDecoder(string stringEncodingName)
    {
        KnownStringDecoders.TryGetValue(stringEncodingName, out var decoder);
        return decoder;
    }

    public static byte[] TryDecodeTransfer(string transferEncodingName, string encodedString) => FindTransferDecoder(transferEncodingName)?.Decode(encodedString);

    public static string TryDecodeString(string stringEncodingName, string encodedString, string charset)
    {
        var bytes = FindStringDecoder(stringEncodingName)?.Decode(encodedString);
        try
        {
            var encoding = System.Text.Encoding.GetEncoding(charset);
            return encoding.GetString(bytes);
        }
        catch(ArgumentException)
        {
            return null;
        }
    }
}