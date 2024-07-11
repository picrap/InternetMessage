using System;

namespace InternetMessage.Utility;

[Flags]
public enum CharacterType
{
    Control = 0x0001,
    Wsp = 0x0002,
    Atext = 0x0004,
    Dquote = 0x0008,
    Specials = 0x0010,
    Alpha = 0x0020,
    Digit = 0x0040,
}

public static class Character
{
    private readonly static CharacterType[] Types =
    {
        CharacterType.Control, // 0
        CharacterType.Control, // 1
        CharacterType.Control, // 2
        CharacterType.Control, // 3
        CharacterType.Control, // 4
        CharacterType.Control, // 5
        CharacterType.Control, // 6
        CharacterType.Control, // 7
        CharacterType.Control, // 8
        CharacterType.Control | CharacterType.Wsp, // 9
        CharacterType.Control, // 10
        CharacterType.Control, // 11
        CharacterType.Control, // 12
        CharacterType.Control, // 13
        CharacterType.Control, // 14
        CharacterType.Control, // 15
        CharacterType.Control, // 16
        CharacterType.Control, // 17
        CharacterType.Control, // 18
        CharacterType.Control, // 19
        CharacterType.Control, // 20
        CharacterType.Control, // 21
        CharacterType.Control, // 22
        CharacterType.Control, // 23
        CharacterType.Control, // 24
        CharacterType.Control, // 25
        CharacterType.Control, // 26
        CharacterType.Control, // 27
        CharacterType.Control, // 28
        CharacterType.Control, // 29
        CharacterType.Control, // 30
        CharacterType.Control, // 31
        CharacterType.Wsp, // 32 space
        CharacterType.Atext, // 33 !
        CharacterType.Dquote, // 34 "
        CharacterType.Atext, // 35 #
        CharacterType.Atext, // 36 $
        CharacterType.Atext, // 37 %
        CharacterType.Atext, // 38 &
        CharacterType.Atext, // 39 '
        CharacterType.Specials, // 40 (
        CharacterType.Specials, // 41 )
        CharacterType.Atext, // 42 *
        CharacterType.Atext, // 43 +
        CharacterType.Specials, // 44 ,
        CharacterType.Atext, // 45 -
        CharacterType.Specials, // 46 .
        CharacterType.Atext, // 47 /
        CharacterType.Atext | CharacterType.Digit, // 48 0
        CharacterType.Atext | CharacterType.Digit, // 49 1
        CharacterType.Atext | CharacterType.Digit, // 50 2
        CharacterType.Atext | CharacterType.Digit, // 51 3
        CharacterType.Atext | CharacterType.Digit, // 52 4
        CharacterType.Atext | CharacterType.Digit, // 53 5
        CharacterType.Atext | CharacterType.Digit, // 54 6
        CharacterType.Atext | CharacterType.Digit, // 55 7
        CharacterType.Atext | CharacterType.Digit, // 56 8
        CharacterType.Atext | CharacterType.Digit, // 57 9
        CharacterType.Specials, // 58 :
        CharacterType.Specials, // 59 ;
        CharacterType.Specials, // 60 <
        CharacterType.Atext, // 61 =
        CharacterType.Specials, // 62 >
        CharacterType.Atext, // 63 ?
        CharacterType.Specials, // 64 @
        CharacterType.Atext | CharacterType.Alpha, // 65 A
        CharacterType.Atext | CharacterType.Alpha, // 66 B
        CharacterType.Atext | CharacterType.Alpha, // 67 C
        CharacterType.Atext | CharacterType.Alpha, // 68 D
        CharacterType.Atext | CharacterType.Alpha, // 69 E
        CharacterType.Atext | CharacterType.Alpha, // 70 F
        CharacterType.Atext | CharacterType.Alpha, // 71 G
        CharacterType.Atext | CharacterType.Alpha, // 72 H
        CharacterType.Atext | CharacterType.Alpha, // 73 I
        CharacterType.Atext | CharacterType.Alpha, // 74 J
        CharacterType.Atext | CharacterType.Alpha, // 75 K
        CharacterType.Atext | CharacterType.Alpha, // 76 L
        CharacterType.Atext | CharacterType.Alpha, // 77 M
        CharacterType.Atext | CharacterType.Alpha, // 78 N
        CharacterType.Atext | CharacterType.Alpha, // 79 O
        CharacterType.Atext | CharacterType.Alpha, // 80 P
        CharacterType.Atext | CharacterType.Alpha, // 81 Q
        CharacterType.Atext | CharacterType.Alpha, // 82 R
        CharacterType.Atext | CharacterType.Alpha, // 83 S
        CharacterType.Atext | CharacterType.Alpha, // 84 T
        CharacterType.Atext | CharacterType.Alpha, // 85 U
        CharacterType.Atext | CharacterType.Alpha, // 86 V
        CharacterType.Atext | CharacterType.Alpha, // 87 W
        CharacterType.Atext | CharacterType.Alpha, // 88 X
        CharacterType.Atext | CharacterType.Alpha, // 89 Y
        CharacterType.Atext | CharacterType.Alpha, // 90 Z
        CharacterType.Specials, // 91 [
        CharacterType.Specials, // 92 \
        CharacterType.Specials, // 93 ]
        CharacterType.Atext, // 94 ^
        CharacterType.Atext, // 95 _
        CharacterType.Atext, // 96 `
        CharacterType.Atext | CharacterType.Alpha, // 97 a
        CharacterType.Atext | CharacterType.Alpha, // 98 b
        CharacterType.Atext | CharacterType.Alpha, // 99 c
        CharacterType.Atext | CharacterType.Alpha, // 100 d
        CharacterType.Atext | CharacterType.Alpha, // 101 e
        CharacterType.Atext | CharacterType.Alpha, // 102 f
        CharacterType.Atext | CharacterType.Alpha, // 103 g
        CharacterType.Atext | CharacterType.Alpha, // 104 h
        CharacterType.Atext | CharacterType.Alpha, // 105 i
        CharacterType.Atext | CharacterType.Alpha, // 106 j
        CharacterType.Atext | CharacterType.Alpha, // 107 k
        CharacterType.Atext | CharacterType.Alpha, // 108 l
        CharacterType.Atext | CharacterType.Alpha, // 109 m
        CharacterType.Atext | CharacterType.Alpha, // 110 n
        CharacterType.Atext | CharacterType.Alpha, // 111 o
        CharacterType.Atext | CharacterType.Alpha, // 112 p
        CharacterType.Atext | CharacterType.Alpha, // 113 q
        CharacterType.Atext | CharacterType.Alpha, // 114 r
        CharacterType.Atext | CharacterType.Alpha, // 115 s
        CharacterType.Atext | CharacterType.Alpha, // 116 t
        CharacterType.Atext | CharacterType.Alpha, // 117 u
        CharacterType.Atext | CharacterType.Alpha, // 118 v
        CharacterType.Atext | CharacterType.Alpha, // 119 w
        CharacterType.Atext | CharacterType.Alpha, // 120 x
        CharacterType.Atext | CharacterType.Alpha, // 121 y
        CharacterType.Atext | CharacterType.Alpha, // 122 z
        CharacterType.Atext, // 123 {
        CharacterType.Atext, // 124 |
        CharacterType.Atext, // 125 }
        CharacterType.Atext, // 126 ~
        CharacterType.Wsp // 127 not sure of it, not exactly clear
    };

    public static bool Is(this char c, CharacterType type) => (int)(Types[c] & type) != 0;
    public static bool Is(this char? c, CharacterType type) => c.HasValue && c.Value.Is(type);
}