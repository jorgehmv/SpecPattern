namespace SpecPattern.Helpers
{
    internal static class HashCodeProvider
    {
        public static int GetHashCode(params object[] args)
        {
            int result = 23;
            foreach (object o in args)
            {
                result = result*37 + o.GetHashCode();
            }

            return result;
        }
    }
}