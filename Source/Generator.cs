using System.IO;

namespace MCC {

    public static class Generator {

        // Use 'main' instead '_main' if not OS X
        const string assembly_format = "\t.globl _main\n_main:\n\tmovl\t${}, %eax\n\tret\n";

        const string function_format = "\t.globl _{0}\n_{0}:\n";

        const string return_format = "\tmovl ${0}, %eax\n\tret\n";

        public static void Write(string filename, Token[] tokens, AbstractSyntaxTree ast) {
            StreamWriter writer = new StreamWriter(filename, false);

            //WriteProgram(writer);
            WriteFunction(writer, ast.Root.Main);
            writer.WriteLine();

            for (int i = 0; i < tokens.Length; i++) {
                writer.Write(tokens[i] + "\n");
            }

            writer.WriteLine();
            writer.WriteLine(ast.ToString());

            writer.Close();
        }

        static void WriteProgram(StreamWriter writer) {
            writer.Write(assembly_format);
        }

        static void WriteFunction(StreamWriter writer, AbstractSyntaxTree.Function function) {
            writer.Write(function_format, function.Identifier);
            WriteStatement(writer, function.Body);
        }

        static void WriteStatement(StreamWriter writer, AbstractSyntaxTree.Statement statement) {
            if (statement is AbstractSyntaxTree.Return) {
                WriteReturn(writer, (AbstractSyntaxTree.Return)statement);
            }
        }

        static void WriteReturn(StreamWriter writer, AbstractSyntaxTree.Return ret) {
            writer.Write(return_format, WriteExpression(writer, ret.Expression));
        }

        static string WriteExpression(StreamWriter writer, AbstractSyntaxTree.Expression expression) {
            if (expression is AbstractSyntaxTree.Constant) {
                return ((AbstractSyntaxTree.Constant)expression).Value;
            }
            return "";
        }

    }
}
