using System;
using System.Linq;
using System.IO;

namespace MCC {

    static class Compiler {
        
        static void Main(string[] args) {

            string inputfilename = "return_int.c";

            if (args.Count() > 0) {
                inputfilename = args[0];
            }
            
            if(!File.Exists(inputfilename)) {
                Console.WriteLine("File \"" + inputfilename + "\" is not available.");
                return;
            }
            
            string src = File.ReadAllText(inputfilename);
            Token[] tokens = Lexer.Tokenize(src);

            AbstractSyntaxTree ast = Parser.Parse(tokens);

            string outfilename = inputfilename.Replace(".c", ".asm");
            Generator.Write(outfilename, tokens, ast);
        }
    }
}