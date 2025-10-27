using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace FinancasPessoais.Infra.IoC
{
    public class FileService : IFileService
    {
        private readonly string _basePath;

        public FileService(IConfiguration configuration)
        {
            _basePath = configuration.GetSection("FileStorage:BasePath").Value;
        }

        public string SaveFile(string description, IFormFile file, DateTime dueDate)
        {
            string yearFolder = Path.Combine(_basePath, dueDate.Year.ToString());
            string monthFolder = Path.Combine(yearFolder, dueDate.ToString("MMMM"));
            string descriptiveFileName;

            if (!Directory.Exists(monthFolder))
                Directory.CreateDirectory(monthFolder);

            descriptiveFileName = file.FileName.Contains(".pdf") ?
                $"{file.FileName.Replace(".pdf", String.Empty)}_{dueDate.ToString("MMMM").ToLower()}.pdf" :
                $"{file.FileName}_{dueDate.ToString("MMMM").ToLower()}.pdf";

            string fullPath = Path.Combine(monthFolder, descriptiveFileName);

            if (File.Exists(fullPath))
                return descriptiveFileName;
            
            // Salvar o arquivo no caminho determinado
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return fullPath;
        }
    }

    public interface IFileService 
    {
        string SaveFile(string description, IFormFile file, DateTime dueDate);
    }
}
