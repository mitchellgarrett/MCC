using System;
using System.Collections.Generic;

namespace MCC {
    
    public static class Parser {

        public static AbstractSyntaxTree Parse(Token[] stream) {
            Queue<Token> tokens = new Queue<Token>(stream);

            AbstractSyntaxTree.Function main = ParseFunction(tokens);

            AbstractSyntaxTree.Program program = new AbstractSyntaxTree.Program(main);
            AbstractSyntaxTree ast = new AbstractSyntaxTree(program);

            return ast;
        }

        public static AbstractSyntaxTree.Function ParseFunction(Queue<Token> tokens) {
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

            AbstractSyntaxTree.Function function = new AbstractSyntaxTree.Function(identifier, ParseStatement(tokens));

            token = tokens.Dequeue();
            if (token.Type != Token.TokenType.CloseBrace) { Fail(token); }

            return function;
        }

        public static AbstractSyntaxTree.Statement ParseStatement(Queue<Token> tokens) {
            Token token = tokens.Dequeue();
            if(!Match(token, Token.TokenType.Keyword, Syntax.GetKeyword(Syntax.Keyword.Return))) { Fail(token); }

            AbstractSyntaxTree.Expression expression = ParseExpression(tokens);
            AbstractSyntaxTree.Statement statement = new AbstractSyntaxTree.Return(expression);

            token = tokens.Dequeue();
            if (token.Type != Token.TokenType.Semicolon) { Fail(token); }

            return statement;
        }

        static AbstractSyntaxTree.Expression ParseExpression(Queue<Token> tokens) {
            Token token = tokens.Dequeue();
            if (token.Type != Token.TokenType.IntegerLiteral) { Fail(token); }

            AbstractSyntaxTree.Constant expression = new AbstractSyntaxTree.Constant(token.Value);

            return expression;
        }

        static bool Match(Token token, Token.TokenType type, string value) {
            return token.Type == type && token.Value == value;
        }
        
        static void Fail(Token token) {
            Console.WriteLine("Invalid token: " + token);
        }
    }
}