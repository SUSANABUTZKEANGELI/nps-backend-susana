using nps_backend_susana.Model.Extensions;
using System.ComponentModel;

namespace nps_backend_susana.Model.Enums
{
    public enum Category
    {
        [Description("CRASH")]
        [EnumTranslate("e0001d6c-905e-42a0-8f2c-89184a6225da")]
        CRASH = 1,

        [Description("CONNECTIVITY")]
        [EnumTranslate("25301326-f806-42e7-9fd9-4ea1e0ddf396")]
        CONNECTIVITY = 2,

        [Description("SLOWNESS")]
        [EnumTranslate("ab7e4d23-ce17-4049-9856-9f1cea110a7e")]
        SLOWNESS = 3,

        [Description("INTERFACE")]
        [EnumTranslate("438109f9-c8bf-43b1-94a0-a186b758b1e1")]
        INTERFACE = 4,

        [Description("BUGS")]
        [EnumTranslate("883fdf80-70a2-4e36-bf0a-a291c1174cba")]
        BUGS = 5,

        [Description("OTHER")]
        [EnumTranslate("8656aec6-9f0f-41e1-a94c-49e2d49a5492")]
        OTHER = 6,
    }
}
