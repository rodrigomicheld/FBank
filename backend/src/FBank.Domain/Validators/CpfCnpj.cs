using FBank.Domain.Enums;

namespace FBank.Domain.Validators
{
    public static class CpfCnpj
    {
        public static PersonType ValidTypeDocument(string document)
        {
            if (IsCpf(document))
                return PersonType.Person;
            else if (IsCnpj(document))
                return PersonType.Company;

            return PersonType.None;
        }

        private static bool IsCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf) || cpf.Length != 11)
            {
                return false;
            }

            int[] digits = cpf.Select(c => int.Parse(c.ToString())).ToArray();

            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += digits[i] * (10 - i);
            }
            int remainder = sum % 11;
            int expectedDigit1 = (remainder < 2) ? 0 : 11 - remainder;
            if (digits[9] != expectedDigit1)
            {
                return false;
            }

            sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += digits[i] * (11 - i);
            }
            remainder = sum % 11;
            int expectedDigit2 = (remainder < 2) ? 0 : 11 - remainder;
            return digits[10] == expectedDigit2;
        }

        private static bool IsCnpj(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj) || cnpj.Length != 14)
            {
                return false;
            }

            int[] digits = cnpj.Select(c => int.Parse(c.ToString())).ToArray();

            int sum = 0;
            int weight = 5;
            for (int i = 0; i < 12; i++)
            {
                sum += digits[i] * weight;
                weight = (weight == 2) ? 9 : weight - 1;
            }
            int remainder = sum % 11;
            int expectedDigit1 = (remainder < 2) ? 0 : 11 - remainder;
            if (digits[12] != expectedDigit1)
            {
                return false;
            }

            sum = 0;
            weight = 6;
            for (int i = 0; i < 13; i++)
            {
                sum += digits[i] * weight;
                weight = (weight == 2) ? 9 : weight - 1;
            }
            remainder = sum % 11;
            int expectedDigit2 = (remainder < 2) ? 0 : 11 - remainder;
            return digits[13] == expectedDigit2;
        }
    }
}
