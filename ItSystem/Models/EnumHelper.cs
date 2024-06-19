namespace ItSystem.Models
{
    public static class EnumHelper
    {
        public static List<T> GetStatuses<T>() where T : struct, Enum
        {
            return Enum.GetValues<T>().Cast<T>().ToList();
        } 
    }
}
