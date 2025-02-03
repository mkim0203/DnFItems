using Common.Models;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MySetItem
{
    public class Program
    {
        

        static void Main(string[] args)
        {
            Main mainFm = new Main();
            mainFm.ShowDialog();

            //if(args.Length == 2)
            //{
            //    string userId = args[0];
            //    string serverName = args[1];

            //    Program pg = new Program();
            //    await pg.RunAsync(userId, serverName);
            //}
        }

    }
}
