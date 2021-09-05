using System;
using System.IO;


namespace SortFileGUI
{
    class SortAPI
    {
        public static string BaseDirectory; // начальная директория где будут сортироватся файлы

        public static void MoveForSort(in byte Iterator, in FileInfo file, bool enable_log = false)
        {
            MoveFileForSort(((EFormats)Iterator).ToString(), in file);
            if (enable_log)
                Console.WriteLine($"File\t{file.Name}\tmove to\t({BaseDirectory}/{ ((EFormats)Iterator).ToString() })\t");
        }

        public static void MoveFileForSort(in string subDirectory, in FileInfo file)
        {
            if (!Directory.Exists(BaseDirectory + "/" + subDirectory)) // проверка есть ли такая дериктория уже
                Directory.CreateDirectory(BaseDirectory + "/" + subDirectory); // если нет создаем
            if (!File.Exists($"{BaseDirectory}/{subDirectory}/{file.Name}"))
                file.MoveTo($"{BaseDirectory}/{subDirectory}/{file.Name}"); // переносим файл в дерикторию subDirectory
            else
                file.MoveTo($"{BaseDirectory}/{subDirectory}/sorting-{file.Name}");

        }

        /// <summary>
        /// Данный метод сортирует файлы, в директории BaseDirectory
        /// </summary>
        /// <param name="enable_log"></param>
        public static void ReadAllSorts(Boolean enable_log = false)
        {
            string[] files = Directory.GetFiles(BaseDirectory); // получаем все файлы в папке

            if (files.Length <= 0)
            {
                Console.WriteLine("All files sorted in directory (" + BaseDirectory + ") !!\n\n\n");
                return;
            }
            string[] imagesFormats = {".psd",
                                      ".jpg",
                                      ".tiff",
                                      ".bmp",
                                      ".jpeg",
                                      ".jp2",
                                      ".j2k",
                                      ".jpf",
                                      ".jpm",
                                      ".jpg2",
                                      ".j2c",
                                      ".jpc",
                                      ".jxr",
                                      ".hdp",
                                      ".wdp",
                                      ".gif",
                                      ".eps",
                                      ".png",
                                      ".pict",
                                      ".pcx",
                                      ".ico",
                                      ".cdr",
                                      ".ai",
                                      ".raw",
                                      ".svg",
                                      ".webp",
                                      ".avif" };
            string[] documentsFormats =
            {
                ".asp",
                ".cdd",
                ".doc",
                ".docm",
                ".docx",
                ".dot",
                ".dotm",
                ".dotx",
                ".epub",
                ".fb2",
                ".gpx",
                ".ibooks",
                ".indd",
                ".kdc",
                ".key",
                ".kml",
                ".mdb",
                ".mdf",
                ".mobi",
                ".mso",
                ".ods",
                ".odt",
                ".one",
                ".oxps",
                ".pages",
                ".pdf",
                ".pkg",
                ".pl",
                ".pot",
                ".potm",
                ".potx",
                ".pps",
                ".ppsm",
                ".ppsx",
                ".ppt",
                ".pptm",
                ".pptx",
                ".ps",
                ".pub",
                ".rtf",
                ".sdf",
                ".sgml",
                ".sldm",
                ".snb",
                ".wpd",
                ".wps",
                ".xar",
                ".xlr",
                ".xls",
                ".xlsb",
                ".xlsm",
                ".xlsx",
                ".xlt",
                ".xltm",
                ".xltx",
                ".xps"
            };
            string[] executeFormats =
            {
                ".apk",
                ".bat",
                ".bin",
                ".cgi",
                ".cmd",
                ".com",
                ".exe",
                ".jse",
                ".gadget",
                ".gtp",
                ".gta",
                ".jar",
                ".msi",
                ".msu",
                ".paf.exe",
                ".pif",
                ".ps1",
                ".pwz",
                ".scr",
                ".thm",
                ".vb",
                ".vbe",
                ".vbs",
                ".wsf",
                ".py",
                ".dll"
            };
            string[] archivesFormats =
            {
                ".7z",
                ".ace",
                ".arj",
                ".cab",
                ".cbr",
                ".deb",
                ".gz",
                ".gzip",
                ".pak",
                ".ppt",
                ".rar",
                ".rpm",
                ".sh",
                ".sib",
                ".sis",
                ".sisx",
                ".sit",
                ".sitx",
                ".spl",
                ".tar",
                ".tar-gz",
                ".tgz",
                ".xar",
                ".zip",
                ".zipx"
            };
            string[] AudioFormats =
            {
                ".aac",
                ".ac3",
                ".aif",
                ".aiff",
                ".amr",
                ".aob",
                ".ape",
                ".asf",
                ".aud",
                ".awb",
                ".bwg",
                ".cdr",
                ".flac",
                ".gpx",
                ".ics",
                ".iff",
                ".m",
                ".m3u",
                ".m3u8",
                ".m4a",
                ".m4b",
                ".m4p",
                ".m4r",
                ".mid",
                ".midi",
                ".mod",
                ".mp3",
                ".mpa",
                ".mpp",
                ".msc",
                ".msv",
                ".mts",
                ".nkc",
                ".ogg",
                ".ps",
                ".ra",
                ".ram",
                ".sdf",
                ".sib",
                ".sln",
                ".spl",
                ".srt",
                ".temp",
                ".vb",
                ".wav",
                ".wave",
                ".wm",
                ".wma",
                ".wpd",
                ".xsb",
                ".xwb"
            };
            string[] videoFormats =
            {
                ".3g2",
                ".3gp",
                ".3gp2",
                ".3gpp",
                ".3gpp2",
                ".asf",
                ".asx",
                ".avi",
                ".dat",
                ".drv",
                ".f4v",
                ".flv",
                ".gtp",
                ".h264",
                ".m4v",
                ".mkv",
                ".mod",
                ".moov",
                ".mov",
                ".mp4",
                ".mpeg",
                ".mts",
                ".rm",
                ".rmvb",
                ".spl",
                ".srt",
                ".stl",
                ".swf",
                ".ts",
                ".vcd",
                ".vid",
                ".vob",
                ".webm",
                ".wm",
                ".wmv",
                ".yuv"
            };
            string[] textsFormats =
            {
                ".apt",
                ".err",
                ".log",
                ".pwi",
                ".sub",
                ".ttf",
                ".tex",
                ".text",
                ".txt",
                ".json"
            };

            string[][] Formats = {
                imagesFormats,
                documentsFormats,
                executeFormats,
                archivesFormats,
                AudioFormats,
                videoFormats,
                textsFormats
            };


            foreach (var item in files) // проходим по всем файлам И берём их расширение если оно соответстует требованиям то переносим в спец назначеную директорию
            {
                FileInfo file = new FileInfo(item);
                bool fileWrite = false;

                for (byte i = 0; i < Formats.Length; i++)
                {
                    for (byte j = 0; j < Formats[i].Length; j++)
                    {
                        if (file.Extension == Formats[i][j]) // проверка расширения файлы и полученых с зубчатова массива данных
                        {

                            MoveForSort(in i, in file, enable_log);
                            fileWrite = true;
                        }
                    }
                }
                if (!fileWrite) MoveFileForSort("Unidentified file", in file);

            }
            foreach (var directores in Directory.GetDirectories(BaseDirectory))
            {
                if (Directory.GetFiles(directores).Length <= 0 && Directory.GetDirectories(directores).Length <= 0)
                    Directory.Delete(directores);
            }
            return;
        }
    }

    enum EFormats
    {
        Images = 0,
        Documents,
        ExecutesFiles,
        Archives,
        Audios,
        Videos,
        TextsFiles
    }
}
