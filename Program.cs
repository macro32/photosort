using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace photosort
{
    class Program
    {
        private static Regex r = new Regex(":");
        private static string destinationRoot;

        public static DateTime GetDateTakenFromImage(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    using (Image myImage = Image.FromStream(fs, false, false))
                    {
                        PropertyItem propItem = myImage.GetPropertyItem(36867);
                        string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);

                        DateTime result;
                        if (DateTime.TryParse(dateTaken, out result))
                        {

                            return result;
                        }

                        return DateTime.MinValue;
                    }
                }
                catch
                {
                    Console.WriteLine("File processig failed for {0}", path);
                    return DateTime.MinValue;
                }
            }
        }

        public static void ProcessDirectory(string targetDirectory)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectory,"*.jpg");
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }

        public static void ProcessFile(string path)
        {
            Console.WriteLine("Processed file '{0}'.", path);
            var dt = GetDateTakenFromImage(path);
            Console.WriteLine("Date taken: {0}", dt);

            var destination = string.Format(@"{0}\{1,4}\{2,2}\{3,2}\{4}",
                destinationRoot,
                dt.Year.ToString("D4"),
                dt.Month.ToString("D2"),
                dt.Day.ToString("D2"),
                Path.GetFileName(path));
            var fullPath = Path.GetDirectoryName(destination);

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            if(File.Exists(destination))
            {
                File.Delete(destination);
            }

            File.Move(path, destination);
            
        }

        static void Main(string[] args)
        {
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                if (options.Verbose) Console.WriteLine("Filename: {0}", options.InputFile);
                Console.WriteLine("Date taken: {0}", GetDateTakenFromImage(options.InputFile).ToLongDateString());
                destinationRoot = options.Destination;

                if (Directory.Exists(options.Source))
                {
                    ProcessDirectory(options.Source);
                }
            }
            Console.ReadLine();
        }

    }
}
