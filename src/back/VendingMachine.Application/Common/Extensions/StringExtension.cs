using Pluralize.NET.Core;


namespace VendingMachine.Application.Common.Extensions
{
    public static class StringExtension
    {

        public static string ToPlural(this string nameParam)
        {
            return new Pluralizer().Pluralize(nameParam);
        }

    }
}
