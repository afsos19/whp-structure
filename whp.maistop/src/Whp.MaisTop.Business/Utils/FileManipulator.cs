using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whp.MaisTop.Business.Utils
{
    public static class FileManipulator
    {
        public static List<string> ValidateSpreadsheet(ExcelWorksheet tab, string[] columnsToValidate)
        {
            var spreedSheetMessages = new List<string>();

            if (tab.Dimension?.Columns == columnsToValidate.Length)
            {
                for (var i = 1; i <= tab.Dimension?.Columns; i++)
                {
                    if (columnsToValidate[i - 1] != tab.Cells[1, i].Text.Trim().ToUpper())
                    {
                        spreedSheetMessages.Add($"Coluna {tab.Cells[1, i].Text} na aba { tab.Name} está na posição errada");
                    }
                }

            }
            else
                spreedSheetMessages.Add("Numero de colunas inválido!");

            return spreedSheetMessages;
        }
        public static async Task<(bool uploaded, string message, string fileName)> Uploadfile(IFormFile file, string pathFromUrl, IEnumerable<string> AcceptedTypes)
        {
            if (file != null)
            {
                if (file.Length > 0 && file.Length <= 2000000)
                {
                    var extensions = Path.GetFileName(file.FileName).Split('.').Last();

                    if (!AcceptedTypes.Contains(extensions.ToUpper()))
                    {
                        return (false, $"Foto perfil formato inválido. Formato atual: {extensions.ToUpper()} - Formatos aceitos: .JPG e .PNG", "");
                    }

                    var imageName = $"{DateTime.Now.Year}" +
                        $"{DateTime.Now.Month.ToString().PadLeft(2, '0')}" +
                        $"{DateTime.Now.Day.ToString().PadLeft(2, '0')}" +
                        $"{DateTime.Now.Hour.ToString().PadLeft(2, '0')}" +
                        $"{DateTime.Now.Minute.ToString().PadLeft(2, '0')}" +
                        $"{DateTime.Now.Second.ToString().PadLeft(2, '0')}.{extensions}";

                    var path = Path.Combine(pathFromUrl, Path.GetFileName(imageName));
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    return (true, "upload efetuado com sucesso", imageName);

                }
                else
                {
                    return (false, "Foto perfil tem limite de tamanho 2MB", "");
                }
            }
            else
            {
                return (false, "Foto não enviada", "");
            }
        }
    }
}
