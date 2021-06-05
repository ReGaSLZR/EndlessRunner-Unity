namespace ReGaSLZR.EndlessRunner.Utils
{

    public static class StringUtil
    {

        public static bool HasEntry(string[] array, string entry)
        {
            if (array == null || array.Length == 0
                || entry == null)
            {
                return false;
            }

            foreach (var arrEntry in array)
            {
                if (arrEntry.Equals(entry))
                {
                    return true;
                }
            }

            return false;
        }

    }

}