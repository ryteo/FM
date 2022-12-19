using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FileManager
{
    public class Commands
    {
        string _path = "";
        string _toPath = "";
        QueryParse path = new QueryParse();
        private List<string> _treeList = new List<string>();
        public void Command(string commandString)
        {
            switch (commandString)
            {
                case "!help":
                    help();
                    break;
                case "cd":
                    CD();
                    break;
                case "ls":
                    List();
                    break;
                case "list":
                    List();
                    break;
                case "delFile":
                    delFile();
                    break;
                case "delFolder":
                    delFolder();
                    break;
                case "copyFile":
                    copyFile();
                    break;
                case "copyFFF":
                    copyFilesFromFolderToFolder();
                    break;
                case "tree":
                    Tree();
                    break;
                case "fi":
                    Console.WriteLine(fi());
                    break;
                case "di":
                    Console.WriteLine(di());
                    break;
                case "!clear":
                    Console.Clear();
                    break;
                default: Console.WriteLine("Неверный формат команды!");
                    break;
            }
        }
        private void help()
        {
            Console.WriteLine("-------------------------------------------------- Основные команды ---------------------------------------------------");
            Console.WriteLine("cd \t\t\t\t - перемещение по файловой системе\t");
            Console.WriteLine("ls или list \t\t\t - вывод списка файлов и папок в текущей директории постраничный");
            Console.WriteLine("delFile \t\t\t - удаление указанного файла");
            Console.WriteLine("delFolder \t\t\t - удаление указанной папки");
            Console.WriteLine("copyFile \t\t\t - копирование файла с заменой");
            Console.WriteLine("copyFFF \t\t\t - копирование файлов из папки в другую папку");
            Console.WriteLine("tree \t\t\t\t - просмотр файловой системы из текущей директори постраничный");
            Console.WriteLine("fi \t\t\t\t - вывод информации о файле");
            Console.WriteLine("di \t\t\t\t - вывод информации о папке");
            Console.WriteLine("!clear \t\t\t\t - очистка консоли");
            Console.WriteLine("!help \t\t\t\t - вызов описания команд");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
        }
        private void CD()
        {
            try
            {
                Console.Write("Введите путь: ");
                _path = path.ToShortPath(Console.ReadLine());
                Directory.SetCurrentDirectory(_path);
                var message = path._ToShortPath(Directory.GetCurrentDirectory()); 
                Console.WriteLine(message);
                string dllPath = Assembly.GetExecutingAssembly().Location;
                string txtPath = new FileInfo(dllPath).DirectoryName + "\backup.txt";
                File.WriteAllText(txtPath, _path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void Tree()
        {
            Console.Write("Введите путь: ");
            _path = path.ToShortPath(Console.ReadLine());
            try
            {
                List<string> directories = Directory.GetDirectories(_path).ToList();
                List<string> files = Directory.GetFiles(_path).ToList();
                foreach (string name in directories)
                {
                    //Console.WriteLine(name);
                    _treeList.Add(name);
                }
                foreach (string name in files)
                {
                    //Console.WriteLine(name);
                    _treeList.Add(name);
                }
                Console.WriteLine();
                foreach (string item in _treeList)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void copyFilesFromFolderToFolder()
        {
            try
            {
                Console.Write("Введите первый путь: ");
                _path = path.ToShortPath(Console.ReadLine());
                Console.Write("Введите второй путь: ");
                _toPath = path.ToShortPath(Console.ReadLine());
                Directory.CreateDirectory(_toPath);
                if (Directory.Exists(_path))
                {
                    string[] files = Directory.GetFiles(_path);
                    foreach (string file in files)
                    {
                        string fileName = Path.GetFileName(file);
                        string destFile = Path.Combine(_toPath, fileName);
                        File.Copy(file, destFile, true);
                    }
                    Console.WriteLine("Успешно");
                }
                else
                {
                    Console.WriteLine("По указанному пути не существует папки!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void List()
        {
            try
            {
                Console.Write("Введите путь: ");
                _path = path.ToShortPath(Console.ReadLine());
                DirectoryInfo dirList = new DirectoryInfo(_path);
                DirectoryInfo[] foldersList = dirList.GetDirectories();
                FileInfo[] filesList = dirList.GetFiles();

                var Files = new DirectoryInfo(_path);
                Console.WriteLine();
                Console.WriteLine("__________Файлы:_________");
                Console.WriteLine();
                foreach (FileInfo file in filesList)
                {
                    Console.WriteLine(Path.GetFileNameWithoutExtension(file.FullName));
                }
                Console.WriteLine();
                Console.WriteLine("__________Папки:_________");
                foreach (DirectoryInfo folder in foldersList)
                {
                    Console.WriteLine(Path.GetFileNameWithoutExtension(folder.FullName));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void delFile()
        {
            Console.Write("Введите путь: ");
            _path = path.ToShortPath(Console.ReadLine());
            try
            {
                if (File.Exists(_path))
                {
                    File.Delete(_path);
                    Console.WriteLine("Файл успешно удален");
                }
                else
                {
                    Console.WriteLine("Такого файла нет!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void delFolder()
        {
            Console.Write("Введите путь: ");
            _path = path.ToShortPath(Console.ReadLine());
            try
            {
                if (Directory.Exists(_path))
                {
                    foreach (string file in Directory.GetFiles(_path))
                    {
                        File.Delete(file);
                    }
                    foreach (string directory in Directory.GetDirectories(_path))
                    {
                        delFolder();
                    }
                    Directory.Delete(_path);
                    Console.WriteLine("Папка удалена");
                }
                else
                {
                    Console.WriteLine("Такой папки не существует");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void copyFile()
        {
            Console.Write("Введите первый путь: ");
            _path = path.ToShortPath(Console.ReadLine());
            Console.Write("Введите второй путь: ");
            _toPath = path.ToShortPath(Console.ReadLine());
            try
            {
                if (File.Exists(_path))
                {
                    FileInfo file = new FileInfo(_path);
                    File.Copy(Path.Combine(_path), Path.Combine(_toPath), true);
                    Console.WriteLine("Успешно!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private string fi()
        {
            try
            {
                Console.Write("Введите путь: ");
                _path = path.ToShortPath(Console.ReadLine());
                FileInfo fileInfo = new FileInfo(_path);
                string str = $"Информация о файле:\nИмя: {fileInfo.Name} | Расширение: {fileInfo.Extension} | Размер файла: {fileInfo.Length} байт | " +
                    $"Создан: {fileInfo.CreationTime} | \nИзменен: {fileInfo.LastWriteTime} | Атрибуты: {fileInfo.Attributes}" +
                    $"\nПолный путь: {fileInfo.FullName}";
                return str;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        private string di()
        {
            try
            {
                Console.Write("Введите путь: ");
                _path = path.ToShortPath(Console.ReadLine());
                DirectoryInfo dirInfo = new DirectoryInfo(_path);
                string str = $"Информация о папке:\nИмя: {dirInfo.Name} | Атрибуты: {dirInfo.Attributes} | " +
                    $"\nСоздана: {dirInfo.CreationTime} | Изенена: {dirInfo.CreationTime}";
                return str;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
