using System.ComponentModel;

namespace UnqMeterAPI.Enums
{
    public enum TipoPregunta
    {
        [Description("Indefinido")]
        INDEFINIDO = 0,
        [Description("Multiple choice")]
        MULTIPLE_CHOICE = 1,
        [Description("Work Cloud")]
        WORK_CLOUD = 2,
        [Description("Ranking")]
        RANKING = 3,
        [Description("Texto abierto")]
        TEXTO_ABIERTO = 4
    }

    public static class EnumExtensionMethods
    {
        public static string GetEnumDescription(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }
    }
}