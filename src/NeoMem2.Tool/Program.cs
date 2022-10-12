using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

using NeoMem2.Core;

using NeoMem2.Core.Stores;
using System.Xml;

namespace NeoMemImporter
{
    class Program
    {
        //private const string InputPath = @"\\Me-nts1\users\BRADSHAWK\My Documents\Notes1.csv";
        private const string InputPath = @"D:\wc\NeoMem2\Notes.csv";
        private const string ExportFolder = @"D:\Temp\NM2";

        private NeoMemFile m_File;
        
        static void Main(string[] args)
        {
            new Program().Loop();
        }

        public void Loop()
        {
            bool exit;
            do
            {
                try
                {
                    exit = Menu();
                }
                catch (Exception ex)
                {
                    exit = false;
                    Console.WriteLine(ex);
                }
            }
            while (!exit);

            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }

        private bool Menu()
        {
            Console.WriteLine("");
            Console.WriteLine("[Menu]");
            Console.WriteLine("L: load file");
            Console.WriteLine("E: export loaded file");
            Console.WriteLine("X: exit");
            Console.WriteLine("Choose: ");
            string choice = Console.ReadLine();

            bool exit = false;
            switch (choice.ToUpper())
            {
                case "L":
                    Load();
                    break;
                case "E":
                    Export();
                    break;
                default:
                    exit = true;
                    break;
            }

            return exit;
        }

        private void Export()
        {
            Console.WriteLine("Export Folder [{0}]: ", ExportFolder);
            string folderName = Console.ReadLine();
            if (string.IsNullOrEmpty(folderName))
            {
                folderName = ExportFolder;
            }
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }
            new NeoMemFlatFileWriter(folderName).Export(m_File);
        }

        private void Load()
        {
            Console.WriteLine("Import File [{0}]: ", InputPath);
            string filename = Console.ReadLine();
            if (string.IsNullOrEmpty(filename))
            {
                filename = InputPath;
            }
            NeoMem1CsvReader reader = new NeoMem1CsvReader(filename);
            m_File = reader.Read();
            //foreach (string columnName in columnNames)
            //{
            //    Console.WriteLine("{0}", columnName);
            //}
            Console.WriteLine("{0} columns found", m_File.ColumnNames.Count);

            //foreach (Note note in m_Notes)
            //{
            //    Console.WriteLine("{0}, {1}", note.Id, note.Text);
            //}
            Console.WriteLine("{0} notes found", m_File.AllNotes.GetNotes().Count());

            //foreach (Note note in file.RootNotes)
            //{
            //    Console.WriteLine("{0}, {1}", note.Id, note.Name);
            //}
            Console.WriteLine("{0} root notes found", m_File.RootNotes.Count);
        }
    }
}
