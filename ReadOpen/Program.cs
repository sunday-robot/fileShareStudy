using System;
using System.Collections.Generic;
using System.IO;

namespace ReadOpen
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileAccess = FileAccess.Read;
            var fileShare = FileShare.Read;
            Console.WriteLine($"FileAccess.{fileAccess}, FileShare.{fileShare}");

            var ofsList = new List<FileStream>();
            foreach (var arg in args)
            {
                try
                {
                    var ofs = new FileStream(arg, FileMode.Open, fileAccess, fileShare);
                    ofsList.Add(ofs);
                    Console.WriteLine($"{arg}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            Console.ReadLine();
            // 下の処理を省略すると、Releaseビルドでは最適化の結果として、オープンした次の瞬間にクローズするらしい.
            foreach (var ofs in ofsList)
            {
                ofs.Close();
            }
        }
    }
}
