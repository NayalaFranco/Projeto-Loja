﻿using System.ComponentModel.DataAnnotations;

namespace Loja.Application.Validations
{
    /// <summary>
    /// Valida um CPF
    /// </summary>
    internal class CpfValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value,
            ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                // Success porque quem deve acusar erro de nulo é o atributo Required
                return ValidationResult.Success;
            }

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            string cpf = (string)value;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return new ValidationResult("CPF Inválido!");
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();

            if (cpf.EndsWith(digito))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("CPF Inválido!");
            }
        }
    }
}
