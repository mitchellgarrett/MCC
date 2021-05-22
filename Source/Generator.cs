using System.IO;

namespace MCC {

    public static class Generator {

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

        static void WriteFunction(StreamWriter writer, AST.Function function) {
            writer.Write(Assembly.format_function, function.Identifier);
            WriteStatement(writer, function.Body);
        }

        static void WriteReturn(StreamWriter writer, AST.Return ret) {
            WriteExpression(writer, ret.Expression);
            writer.Write(Assembly.format_return);
        }

        static void WriteStatement(StreamWriter writer, AST.Statement statement) {
            if (statement is AST.Return) {
                WriteReturn(writer, (AST.Return)statement);
            }
        }

        static void WriteExpression(StreamWriter writer, AST.Expression expression) {
            if (expression is AST.Constant constant) {
                writer.Write(Assembly.format_constant, Assembly.register_A_32, constant.Value);
            } else if(expression is AST.UnaryOperator unary) {
                WriteUnaryOperator(writer, unary);
            }
        }

        static void WriteUnaryOperator(StreamWriter writer, AST.UnaryOperator unary) {
            WriteExpression(writer, unary.Expression);

            switch (unary.Operator) {
                case Syntax.operator_subtraction:
                    writer.Write(Assembly.format_negate, Assembly.register_A_32);
                    break;

                case Syntax.operator_negation:
                    writer.Write(Assembly.format_not, Assembly.register_A_32);
                    break;

                case Syntax.operator_complement:
                    writer.Write(Assembly.format_complement, Assembly.register_A_32);
                    break;
            }
        }

        // FIXME
        static void WriteBinaryOperator(StreamWriter writer, AST.BinaryOperator binary) {
            WriteExpression(writer, binary.LeftExpression);
            WriteExpression(writer, binary.RightExpression);

            // Write operator
        }

    }
}
