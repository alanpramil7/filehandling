class FileHandling
{
    static void Main(string[] args)
    {
        string sourcePath = @"/home/alan/Alan/amadis/FileHandling/source";
        string targetPath = @"/home/alan/Alan/amadis/FileHandling/target";
        string logFilepath = @"/home/alan/Alan/amadis/FileHandling/logfile.csv";

        if (!File.Exists(logFilepath))
        {
            File.WriteAllText(logFilepath, "Filename, Datetime, Source, Target, Status\n");
        }

        string[] files = Directory.GetFiles(sourcePath);

        foreach (string file in files)
        {
            string filename = Path.GetFileNameWithoutExtension(file);
            string dirname = filename.Split('_')[0];
            string targetDirPath = Path.Combine(targetPath, dirname);
            try
            {
                Directory.CreateDirectory(targetDirPath);
                string newFilePath = Path.Combine(targetDirPath, Path.GetFileName(file));
                File.Move(file, newFilePath);

                GenerateLog(
                    logFilepath,
                    Path.GetFileName(file),
                    Convert.ToString(DateTime.Now),
                    sourcePath,
                    targetDirPath,
                    "Sucess"
                );
                Console.WriteLine("Files moved sucessfully");
            }
            catch (Exception e)
            {
                GenerateLog(
                    logFilepath,
                    Path.GetFileName(file),
                    Convert.ToString(DateTime.Now),
                    sourcePath,
                    targetDirPath,
                    $"Failed :{e.Message}"
                );
                Console.WriteLine("Failed to move files. See logs for more detail");
            }
        }
    }

    static void GenerateLog(
        string csvPath,
        string filename,
        string date,
        string source,
        string target,
        string status
    )
    {
        string log = $"{filename},{date},{source},{target},{status}\n";
        File.AppendAllText(csvPath, log);
    }
}
