﻿using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.CommandLineOptions
{
    public class CommandLineOptions
    {
        [Option('d', Required = true, HelpText = "Spesefiy a problem that will be solved by typing \'d\'" )]
        public int Day { get; set; }

        [Option("path", Required = false, HelpText = "Set path to input, Defualt: path to input file included in build")]
        public string Path { get; set; }

        [Option('a', Required = false, HelpText = "1 for first question, 2 for second question, 3 for both")]
        public int Answer { get; set; }
    }
}
