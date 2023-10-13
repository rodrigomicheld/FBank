using System.ComponentModel;

namespace FBank.Domain.Enums
{
    public enum PersonType
    {
        [Description("Fisica")]
        Person = 1,
        [Description("Juridica")]
        Company = 2
    }
}
