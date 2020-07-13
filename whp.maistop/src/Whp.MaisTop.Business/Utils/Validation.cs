using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Whp.MaisTop.Business.Utils
{
    public static class Validation
    {
        public static bool CheckValidDateFromBr(string date)
        {
            try
            {
                DateTime dt = new DateTime(int.Parse(date.Split('/')[2]), int.Parse(date.Split('/')[1]), int.Parse(date.Split('/')[0]));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsOffice(string office)
        {
            switch (office.ToUpper().Trim())
            {
                case "VENDEDOR":
                    return true;

                case "GERENTE":
                    return true;

                case "GERENTEREGIONAL":
                    return true;

                case "GESTORDAINFORMACAO":
                    return true;

            }

            return false;
        }

        public static bool ValidaCPF(string CPF)
        {
            try
            {
                if (CPF.Trim().Length == 0)
                    return false;

                int[] mt1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] mt2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string TempCPF;
                string Digito;
                int soma;
                int resto;

                CPF = CPF.Trim();
                CPF = CPF.Replace(".", "").Replace("-", "");

                if (CPF.Length != 11)
                    return false;

                if (CPF == "11111111111" || CPF == "22222222222" || CPF == "33333333333" || CPF == "44444444444" || CPF == "55555555555" || CPF == "66666666666" || CPF == "77777777777" || CPF == "88888888888" || CPF == "99999999999")
                    return false;

                TempCPF = CPF.Substring(0, 9);
                soma = 0;
                for (int i = 0; i < 9; i++)
                    soma += int.Parse(TempCPF[i].ToString()) * mt1[i];

                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                Digito = resto.ToString();
                TempCPF = TempCPF + Digito;
                soma = 0;

                for (int i = 0; i < 10; i++)
                    soma += int.Parse(TempCPF[i].ToString()) * mt2[i];

                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                Digito = Digito + resto.ToString(CultureInfo.InvariantCulture);

                return CPF.EndsWith(Digito);
            }
            catch
            {

                return false;
            }

        }

        public static string RemoverAcentos(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }

        public static bool IsNumeric(string prstValor)
        {
            var aimStDatachars = prstValor.ToCharArray();

            try
            {
                if (aimStDatachars.Any(t => !char.IsDigit(t)))
                    return false;
            }
            catch
            {
                return false;
            }



            return true;
        }

        public static bool ValidaCnpj(string value)
        {
            if (value != null)
            {
                string CNPJ = value.ToString().Replace(".", "").Replace("/", "").Replace("-", "");

                if (CNPJ == "00000000000000")
                {
                    return false;
                }

                int[] digitos, soma, resultado;
                int nrDig;
                string ftmt;
                bool[] CNPJOk;

                ftmt = "6543298765432";
                digitos = new int[14];
                soma = new int[2];
                soma[0] = 0;
                soma[1] = 0;
                resultado = new int[2];
                resultado[0] = 0;
                resultado[1] = 0;
                CNPJOk = new bool[2];
                CNPJOk[0] = false;
                CNPJOk[1] = false;

                try
                {
                    for (nrDig = 0; nrDig < 14; nrDig++)
                    {
                        digitos[nrDig] = int.Parse(CNPJ.Substring(nrDig, 1));

                        if (nrDig <= 11)
                            soma[0] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig + 1, 1)));

                        if (nrDig <= 12)
                            soma[1] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig, 1)));
                    }

                    for (nrDig = 0; nrDig < 2; nrDig++)
                    {
                        resultado[nrDig] = (soma[nrDig] % 11);

                        if ((resultado[nrDig] == 0) || (resultado[nrDig] == 1))
                            CNPJOk[nrDig] = (digitos[12 + nrDig] == 0);
                        else
                            CNPJOk[nrDig] = (digitos[12 + nrDig] == (11 - resultado[nrDig]));
                    }

                    return (CNPJOk[0] && CNPJOk[1]);
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
    }
}
