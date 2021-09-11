namespace InternetMessage
{
    public static class Character
    {
        public static bool IsSp(this char c) => c == ' ';
        public static bool IsHtab(this char c) => c == '\t';
        public static bool IsWsp(this char c) => IsSp(c) || IsHtab(c);
    }
}