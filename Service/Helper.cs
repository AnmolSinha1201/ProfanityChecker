namespace Service
{
    public static class Helpers
    {
        public static string Prepare(this string Word)
        => Word.Replace("'", "''");
    }
}