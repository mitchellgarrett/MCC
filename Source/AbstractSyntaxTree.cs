using System;

namespace MCC {

    public class AbstractSyntaxTree {

        public Program Root;

        public AbstractSyntaxTree(Program root) {
            this.Root = root;
        }

        public override string ToString() {
            return Root.ToString();
        }

        public interface ASTNode { }

        public struct Program : ASTNode {
            public Function Main;

            public Program(Function main) {
                this.Main = main;
            }

            public override string ToString() {
                return Main.ToString();
            }
        }

        public struct Function : ASTNode {
            public string Identifier;
            public Statement Body;

            public Function(string identifier, Statement body) {
                this.Identifier = identifier;
                this.Body = body;
            }

            public override string ToString() {
                return Identifier + ":\n" + Body.ToString();
            }
        }

        /*** STATEMENTS ***/

        public interface Statement : ASTNode { }

        public struct Return : Statement {
            public Expression Expression;

            public Return(Expression expression) {
                this.Expression = expression;
            }

            public override string ToString() {
                return "return<" + Expression.ToString() + ">";
            }
        }

        public struct Assign : Statement {
            public Variable Variable;
            public Expression Expression;

            public Assign(Variable variable, Expression expression) {
                this.Variable = variable;
                this.Expression = expression;
            }

            public override string ToString() {
                return Variable.Identifier + " = " + Expression.ToString();
            }
        }

        /*** EXPRESSIONS ***/

        public interface Expression : ASTNode { }

        public struct Constant : Expression {
            public string Value;

            public Constant(string value) {
                this.Value = value;
            }

            public override string ToString() {
                return Value;
            }
        }

        public struct Variable : Expression {
            public string Identifier;
            public string Value;

            public Variable(string identifier, string value) {
                this.Identifier = identifier;
                this.Value = value;
            }

            public override string ToString() {
                return Identifier + "<" + Value + ">";
            }
        }
    }
}
