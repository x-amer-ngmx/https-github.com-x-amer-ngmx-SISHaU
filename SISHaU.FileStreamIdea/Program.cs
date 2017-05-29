using SISHaU.Library.File;
using SISHaU.Library.File.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SISHaU.FileStreamIdea
{
    public class ConsoleSpiner
    {
        int counter;
        string[] sequence;

        public ConsoleSpiner()
        {
            counter = 0;
            //sequence = new string[] { "/", "-", "\\", "|" };
            //sequence = new string[] { ".", "o", "0", "o" };
            //sequence = new string[] { "+", "x" };
            sequence = new string[] { "V", "<", "^", ">" };
            //sequence = new string[] { ".   ", "..  ", "... ", "...." };
        }
        public void Turn()
        {
            //System.Threading.Thread.Sleep(100);
            counter++;

            if (counter >= sequence.Length) counter = 0;

            Console.Write(sequence[counter]);
            var left = Console.CursorLeft < sequence.Length ? 0 : Console.CursorLeft - sequence.Length;
            Console.SetCursorPosition(left, Console.CursorTop);

            //System.Threading.Thread.Sleep(100);
        }
    }
    class Program
    {
        static void ConsoleDraw(IEnumerable<string> lines, int x, int y)
        {
            if (x > Console.WindowWidth) return;
            if (y > Console.WindowHeight) return;

            var trimLeft = x < 0 ? -x : 0;
            int index = y;

            x = x < 0 ? 0 : x;
            y = y < 0 ? 0 : y;

            var linesToPrint =
                from line in lines
                let currentIndex = index++
                where currentIndex > 0 && currentIndex < Console.WindowHeight
                select new
                {
                    Text = new String(line.Skip(trimLeft).Take(Math.Min(Console.WindowWidth - x, line.Length - trimLeft)).ToArray()),
                    X = x,
                    Y = y++
                };

            Console.Clear();
            foreach (var line in linesToPrint)
            {
                Console.SetCursorPosition(line.X, line.Y);
                Console.Write(line.Text);
            }
        }

        static void Main(string[] args)
        {
#if DEBUG
            var files = new[] {
                @"D:\test0.zip",
                @"D:\test1.zip",
                @"D:\test2.zip",
                @"D:\test3.zip",
                @"D:\test4.zip",
                @"D:\test5.zip",
                @"D:\test6.zip",
                @"D:\test7.zip", //350mb
                //@"D:\test8.zip" //1 386 mb
            };

            var tmpPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var result = new List<UploadeResultModel>();
            Console.CursorVisible = false;

            var arr = new[]
            {
                @"      ___                       ___                     ___         ___           ___",
                @"     /\  \          ___        /\  \                   /\  \       /\__\         /\__\",
                @"    /::\  \        /\  \      /::\  \                  \:\  \     /:/  /        /:/  /",
                @"   /:/\:\  \       \:\  \    /:/\ \  \             ___ /::\__\   /:/__/        /:/__/",
                @"  /:/  \:\  \      /::\__\  _\:\~\ \  \           /\  /:/\/__/  /::\__\____   /::\  \ ___",
                @" /:/__/_\:\__\  __/:/\/__/ /\ \:\ \ \__\          \:\/:/  /    /:/\:::::\__\ /:/\:\  /\__\",
                @" \:\  /\ \/__/ /\/:/  /    \:\ \:\ \/__/           \::/  /     \/_|:|~~|~    \/__\:\/:/  /",
                @"  \:\ \:\__\   \::/__/      \:\ \:\__\              \/__/         |:|  |          \::/  /",
                @"   \:\/:/  /    \:\__\       \:\/:/  /                            |:|  |          /:/  /",
                @"    \::/  /      \/__/        \::/  /                             |:|  |         /:/  /",
                @"     \/__/                     \/__/                               \|__|         \/__/",
                @"               ___           ___           ___                       ___              ",
                @"              /\  \         /\__\         /\__\          ___        /\__\",
                @"             /::\  \       /:/  /        /:/  /         /\  \      /::|  |",
                @"            /:/\:\  \     /:/  /        /:/__/          \:\  \    /:|:|  |",
                @"           /::\~\:\  \   /:/  /  ___   /::\__\____      /::\__\  /:/|:|  |__",
                @"          /:/\:\ \:\__\ /:/__/  /\__\ /:/\:::::\__\  __/:/\/__/ /:/ |:| /\__\",
                @"          \/__\:\ \/__/ \:\  \ /:/  / \/_|:|~~|~    /\/:/  /    \/__|:|/:/  /",
                @"               \:\__\    \:\  /:/  /     |:|  |     \::/__/         |:/:/  /",
                @"                \/__/     \:\/:/  /      |:|  |      \:\__\         |::/  /",
                @"                           \::/  /       |:|  |       \/__/         /:/  /",
                @"                            \/__/         \|__|                     \/__/",
            };

            var maxLength = arr.Aggregate(0, (max, line) => Math.Max(max, line.Length));
            var x = Console.BufferWidth / 2 - maxLength / 2;
            for (int y = -arr.Length; y < Console.WindowHeight + arr.Length; y++)
            {
                ConsoleDraw(arr, x, y);
                System.Threading.Thread.Sleep(100);
            }

            foreach (var patch in files)
            {
                Console.WriteLine($"Обработка файла {{{patch}}}");
                result.Add(SplitFiles(tmpPath, patch));
            }

            Console.WriteLine();

            Console.WriteLine(new string('/',25));
            Console.WriteLine($"Сборка файлов.");
            Console.WriteLine(new string('/', 25));

            Console.WriteLine();


            foreach (var re in result)
            {
                Console.WriteLine($"Собираем файл: {{{re.FileName}}}, кол-во частей: {{{re.Parts.Count}}}");
                var spin = new ConsoleSpiner();
                using (var tmpFile = new FileStream($@"{tmpPath}\{re.FileName}", FileMode.Create, FileAccess.Write))
                {
                    foreach (var pat in re.Parts)
                    {
                        var buff = File.ReadAllBytes(pat.Patch);
                        spin.Turn();
                        tmpFile.Write(buff, 0, buff.Length);

                        if (pat.Patch.IndexOf(".tmpart") > 0) File.Delete(pat.Patch);
                    }
                }
            }
            Console.CursorVisible = true;
            Console.WriteLine("Программа выполнена.");
            Console.ReadKey();
#endif
        }

        /// <summary>
        /// Операция расщипления файла. Получает поток файла и определяет больше ли он заданного предела или нет, и возвращает путь либо к самому файлу либо к его нарезанным частям...
        /// </summary>
        /// <param name="tmpPath">Можно вынести в константу, так как это локальное хранилище временных частей файла.</param>
        /// <param name="patch">Путь к файлу</param>
        /// <returns>Объект определяющий размер и массив частей.</returns>
        private static UploadeResultModel SplitFiles(string tmpPath, string patch)
        {
            var resultX = new UploadeResultModel();
            IList<ByteDetectorModel> result = null;
            var fName = Path.GetFileNameWithoutExtension(patch);

            resultX.FileName = Path.GetFileName(patch);

            //Используем поток файла не загружая оперативу, ненужными байтами
            using (var file = new FileStream(patch, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Console.WriteLine(new string('=', 20));

                //Определяем кол-во частей
                var parts = (int)(file.Length / ConstantModel.MaxPartSize) + 1;
                Console.WriteLine($"= Размер файла {{{file.Length}}}, кол-во частей {{{parts}}}");

                //Применение рефакторинг-кунгфу....
                result = parts == 1 ? new ByteDetectorModel[] {
                        new ByteDetectorModel{
                            From = 0,
                            To = file.Length,
                            Part = 1,
                            Patch = patch,
                            Md5Hash = file.FileMd5()
                        }
                    } : SingleFiles(parts, file, fName, tmpPath);
                
                //Определяем и сохраняем хеш-по-госту файла
                resultX.FileGuid = file.FileGost();
                file.Dispose();
            }

            resultX.Parts = result;

            Console.WriteLine(new string('=',20));

            return resultX;
        }


        private static IList<ByteDetectorModel> SingleFiles(int parts, Stream file, string fName, string tmpPath)
        {
            var result = new List<ByteDetectorModel>();
            var part = 1;
            long partTo = 0;
            Console.WriteLine(new string('*',15));
            Console.WriteLine($"Расщипление файла.");
            var spin = new ConsoleSpiner();
            while (part <= parts)
            {
                partTo = part == 1 ? 0 : partTo + ConstantModel.MaxPartSize;

                var from = part != parts ? partTo + ConstantModel.MaxPartSize : file.Length;
                var buffSize = part != parts ? (int)(ConstantModel.MaxPartSize) : (int)(file.Length - partTo);

                //выделение буферной памяти для создания части
                var buffer = new byte[buffSize];
                //запись части в буфер и возврат её реального размера
                var partSize = file.Read(buffer, 0, buffSize);

                //путь к временно-созданной части
                var splitPatch = $@"{tmpPath}\{file.Length}_{fName}.{part}.tmpart";

                Console.WriteLine($" Часть {{{splitPatch}}}, размер {{{partSize}}}");
                spin.Turn();

                //Создание части, если часть уже существует то она будет перезаписанна
                using (var tmpFile = new FileStream(splitPatch, FileMode.Create, FileAccess.Write))
                {
                    tmpFile.Write(buffer, 0, partSize);
                }

                //Формируем коллекцию частей(в языке C# несуществует простых массивов)
                result.Add(
                    new ByteDetectorModel
                    {
                        Part = part,
                        From = partTo,
                        To = from - 1,
                        Patch = splitPatch,
                        Md5Hash = buffer.FileMd5()
                    });
                part++;
            }
            Console.WriteLine(new string('*', 15));

            return result;
        }

    }
}
