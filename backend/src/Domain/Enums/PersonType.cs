using System.ComponentModel;

namespace Domain.Enums
{
    public enum PersonType
    {
        [Description("Nenhum")]
        None = 0,

        [Description("Fisica")]
        Person = 1,

        [Description("Juridica")]
        Company = 2
    }
}
