namespace PAMobile.Utils;

internal class FileHelper
{
    internal static async Task<string> SaveFileAsync(byte[] fileData, string extension)
    {

        var fileName = "uploaded_file_" + DateTime.UtcNow.ToString("ddMMM_hhmmss") + extension;

        var fileFullPath = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
        //var fileFullPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), fileName);

        await File.WriteAllBytesAsync(fileFullPath, fileData);

        //string mainDir = FileSystem.Current.AppDataDirectory;
        return fileFullPath;

    }
}
