using System.IO.Compression;

namespace CheckZipExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Crie um objeto de FileInfo para o caminho especificado
            // Create an object of FileInfo for specified path            
            FileInfo fileInfo = new(args[0]);

            // Verifica se o arquivo passado para teste existe
            // Checks if the file passed for testing exists
            if (fileInfo.Exists && fileInfo.Extension == ".zip")
            {
                // Abrir o arquivo para leitura
                // Open a file for Read
                FileStream fileStream = fileInfo.Open(FileMode.Open, FileAccess.Read);

                // Cria aum objeto ZipArchive que representa um pacote de arquivos compactados no formato de arquivo zip
                // Creates a ZipArchive object that represents a package of files compressed in zip file format.
                using ZipArchive zipArchive = new(fileStream, ZipArchiveMode.Read);


                // Coleção de elementos não ordenados e não permite elementos duplicados
                // Collection of unordered elements and does not allow duplicate elements
                HashSet<string> extensions = [];


                // Faz a iteração entre todos os arquivos do ZIP
                // Iterates through all files in ZIP
                foreach ( var zipArchiveEntry in zipArchive.Entries)
                {
                    // Pega o nome do arquivo, extrai sua extensão convertendo em minúsculo e adiciona na lista de extensões
                    // Get the file name, extract its extension converting it to lower case and add it to the extension list
                    string extension = Path.GetExtension(zipArchiveEntry.Name);
                    if(!string.IsNullOrEmpty(extension))
                    {
                        extensions.Add(extension.ToLower());
                    }
                }

                // Cria uma lista de extensões não permitidas
                // Creates a list of not allowed extensions
                List<string> extesnionsNotAllowed = [".exe", ".bin", ".obj"];

                // Verifica se a lista de extensões do ZIP contém alguma extensão que não é permitida
                // Checks whether the ZIP extension list contains any extensions that are not allowed
                if (extensions.Overlaps(extesnionsNotAllowed))
                {
                    Console.WriteLine("Arquivo ZIP contém extensões que não são permitidas / ZIP file contains extensions that are not allowed");
                } 
                else
                {
                    Console.WriteLine("Arquivo ZIP contém somente extensões permitidas / ZIP file contains only allowed extensions");
                }
            } 
            else 
            {
                Console.WriteLine("Arquivo não Encontrado ou não é um ZIP / File not found or not a ZIP file");
            }

        }
    }
}
