

namespace AgendaApp.Helpers;
//obtener un archivo
public class FileAccessHelper
{
    public static string GetPathFile(string File) => System.IO.Path.Combine(FileSystem.AppDataDirectory, File);

    public static string GetAppDirectory()
        => FileSystem.AppDataDirectory;
}
