namespace nps_backend_susana.Model.Extensions;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
sealed class EnumTranslateAttribute : Attribute
{
    public string Translation { get; }
    
    public EnumTranslateAttribute(string translation)
    {
        Translation = translation;
    }
}