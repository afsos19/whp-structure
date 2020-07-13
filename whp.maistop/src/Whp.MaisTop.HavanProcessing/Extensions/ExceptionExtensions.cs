using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Whp.MaisTop.HavanProcessing.Extensions
{
    static class ExceptionExtensions
    {

        public static string ToLogString(this Exception exception, string environmentStackTrace)
        {
            List<string> environmentStackTraceLines = ExceptionExtensions.GetUserStackTraceLines(environmentStackTrace);
            environmentStackTraceLines.RemoveAt(0);

            return $" {GetDescriptionException(exception.GetType().Name)} {environmentStackTraceLines.First().Split("//").Last().TrimStart()}";
        }


        private static string GetDescriptionException(string typeException)
        {
            switch (typeException)
            {
                case "XmlException":
                    return "XML no formato inválido";
                case "NullReferenceException":
                    return "Um referencia no objeto ou instancia esta nula";
                case "ArgumentException":
                    return "Um parametro foi passado para um metodo invalido";
                case "ArgumentNullException":
                    return "Foi passado um parametro nulo para o metodo";
                case "InvalidOperationException":
                    return "Uma chamada de método é inválida no estado atual de um objeto";
                case "ArgumentOutOfRangeException":
                    return "Um argumento está fora do intervalo de valores válidos";
                case "DirectoryNotFoundException":
                    return "Diretório não encontrado";
                case "DivideByZeroException":
                    return "O denominador em uma operação de divisão inteira ou decimal é zero";
                case "DriveNotFoundException":
                    return "A unidade não esta disponivel ou não existe";
                case "FileNotFoundException":
                    return "Arquivo não encontrado";
                case "FormatException":
                    return "Um valor não está em um formato apropriado para ser convertido de uma string por um método de conversão como o Parse";
                case "IndexOutOfRangeException":
                    return "Um índice está fora dos limites de uma matriz ou coleção";
                case "KeyNotFoundException":
                    return "A chave especificada para acessar um membro em uma coleção não pode ser encontrada";
                case "NotImplementedException":
                    return "Um método ou operação não está implementado";
                case "NotSupportedException":
                    return "Um método ou operação não é suportado";
                case "ObjectDisposedException":
                    return "Uma operação é executada em um objeto que foi descartado";
                case "OverflowException":
                    return "Uma operação aritmética, de conversão ou de conversão resulta em um estouro";
                case "PathTooLongException":
                    return "Um caminho ou nome de arquivo excede o comprimento máximo definido pelo sistema";
                case "PlatformNotSupportedException":
                    return "A operação não é suportada na plataforma atual";
                case "RankException":
                    return "Uma matriz com o número errado de dimensões é passada para um método";
                case "TimeoutException":
                    return "O intervalo de tempo atribuído a uma operação expirou";
                case "UriFormatException":
                    return "Um URI (Uniform Resource Identifier) inválido é usado";
                default:
                    return "Ocorreu um erro grave";
            }
        }

        private static List<string> GetStackTraceLines(string stackTrace)
        {
            return stackTrace.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
        }


        private static List<string> GetUserStackTraceLines(string fullStackTrace)
        {
            List<string> outputList = new List<string>();
            Regex regex = new Regex(@"([^\)]*\)) in (.*):line (\d)*$");

            List<string> stackTraceLines = ExceptionExtensions.GetStackTraceLines(fullStackTrace);
            foreach (string stackTraceLine in stackTraceLines)
            {
                if (!regex.IsMatch(stackTraceLine))
                {
                    continue;
                }

                outputList.Add(stackTraceLine);
            }

            return outputList;
        }
    }
}
