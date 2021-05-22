namespace MCC {

    public class AbstractSyntaxTree {

        public AST.Program Root;

        public AbstractSyntaxTree(AST.Program root) {
            this.Root = root;
        }

        public override string ToString() {
            return Root.ToString();
        }
    }

    public static class AST {

        public abstract class ASTNode { }

        public class Program : ASTNode {
            public Function Main;

            public Program(Function main) {
                this.Main = main;
            }

            public override string ToString() {
                return Main.ToString();
            }
        }

        public class Function : ASTNode {
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

        public abstract class Statement : ASTNode { }

        public class Return : Statement {
            public Expression Expression;

            public Return(Expression expression) {
                this.Expression = expression;
            }

            public override string ToString() {
                return "return<" + Expression.ToString() + ">";
            }
        }

        public class Assign : Statement {
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

        public abstract class Expression : ASTNode { }

        public class Constant : Expression {
            public string Value;

            public Constant(string value) {
                this.Value = value;
            }

            public override string ToString() {
                return Value;
            }
        }

        public class Variable : Expression {
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

        public class UnaryOperator : Expression {
            public char Operator;
            public Expression Expression;

            public UnaryOperator(char op, Expression exp) {
                this.Operator = op;
                this.Expression = exp;
            }

            public override string ToString() {
                return Operator + Expression.ToString();
            }
        }

        public class BinaryOperator : Expression {
            public char Operator;
            public Expression LeftExpression;
            public Expression RightExpression;

            public BinaryOperator(char op, Expression lhs, Expression rhs) {
                this.Operator = op;
                this.LeftExpression = lhs;
                this.RightExpression = rhs;
            }

            public override string ToString() {
                return LeftExpression.ToString() + Operator + RightExpression.ToString();
            }
        }
    }
}
