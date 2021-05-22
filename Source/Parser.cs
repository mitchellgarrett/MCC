using System;
using System.Collections.Generic;

namespace MCC {
    
    public static class Parser {

        public static AbstractSyntaxTree Parse(Token[] stream) {
            Queue<Token> tokens = new Queue<Token>(stream);

            AST.Function main = ParseFunction(tokens);

            AST.Program program = new AST.Program(main);
            AbstractSyntaxTree ast = new AbstractSyntaxTree(program);

            return ast;
        }

        public static AST.Function ParseFunction(Queue<Token> tokens) {
            Token token = tokens.Dequeue();
            if (!Match(token, Token.TokenType.Keyword, Syntax.GetKeyword(Syntax.Keyword.Integer))) { Fail(token); }

            token = tokens.Dequeue();
            if (token.Type != Token.TokenType.Identifier) { Fail(token); }
            string identifier = token.Value;

            token = tokens.Dequeue();
            if (token.Type != Token.TokenType.OpenParenthesis) { Fail(token); }

            token = tokens.Dequeue();
            if (token.Type != Token.TokenType.CloseParenthesis) { Fail(token); }

            token = tokens.Dequeue();
            if (token.Type != Token.TokenType.OpenBrace) { Fail(token); }

            AST.Function function = new AST.Function(identifier, ParseStatement(tokens));

            token = tokens.Dequeue();
            if (token.Type != Token.TokenType.CloseBrace) { Fail(token); }

            return function;
        }

        public static AST.Statement ParseStatement(Queue<Token> tokens) {
            Token token = tokens.Dequeue();
            if(!Match(token, Token.TokenType.Keyword, Syntax.GetKeyword(Syntax.Keyword.Return))) { Fail(token); }

            AST.Expression expression = ParseExpression(tokens);
            AST.Statement statement = new AST.Return(expression);

            token = tokens.Dequeue();
            if (token.Type != Token.TokenType.Semicolon) { Fail(token); }

            return statement;
        }

        static AST.Expression ParseExpression(Queue<Token> tokens) {
            Token token = tokens.Dequeue();
            switch (token.Type) {
                case Token.TokenType.IntegerLiteral:
                case Token.TokenType.CharacterLiteral:
                case Token.TokenType.StringLiteral:
                    return new AST.Constant(token.Value);

                case Token.TokenType.UnaryOperator:
                    return new AST.UnaryOperator(token.Value[0], ParseExpression(tokens));

                //case Token.TokenType.BinaryOperator:
                    //return new AbstractSyntaxTree.BinaryOperator(token.Value[0], ParseExpression(tokens));
            }

            Fail(token);
            return null;
        }

        static bool Match(Token token, Token.TokenType type, string value) {
            return token.Type == type && token.Value == value;
        }
        
        static void Fail(Token token) {
            Console.WriteLine("Invalid token: " + token);
        }
    }
}