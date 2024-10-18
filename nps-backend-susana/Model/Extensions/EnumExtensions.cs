namespace nps_backend_susana.Model.Extensions
{
    public static class EnumExtensions
    {
        public static string GetTranslation(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            if (field == null)
            {
                return string.Empty;
            }

            var attribute = (EnumTranslateAttribute)Attribute.GetCustomAttribute(field, typeof(EnumTranslateAttribute));

            return attribute?.Translation ?? value.ToString();
        }
    }
}
