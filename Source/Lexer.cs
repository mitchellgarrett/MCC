using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MCC {
    
    public static class Lexer {
        
        static Token[] common_tokens = new Token[] { 
            new Token(Token.TokenType.Semicolon, Syntax.semicolon), 
            new Token(Token.TokenType.OpenBrace, Syntax.open_brace), 
            new Token(Token.TokenType.CloseBrace, Syntax.close_brace),
            new Token(Token.TokenType.OpenParenthesis, Syntax.open_parenthesis),
            new Token(Token.TokenType.CloseParenthesis, Syntax.close_parenthesis)
        };
        
        public static Token GetCommonToken(Token.TokenType token) {
            return common_tokens[(int)(token - Token.TokenType.Semicolon)];
        }
        
        public static Token[] Tokenize(string src) {
            List<Token> tokens = new List<Token>();
            
            Token token;
            string current_word = string.Empty;
            for (int i = 0; i < src.Length; i++) {
                char c = src[i];
                if(char.IsWhiteSpace(c)) {
                    if(!string.IsNullOrEmpty(current_word)) {
                        tokens.Add(BuildToken(current_word));
                        current_word = string.Empty;
                    }
                    continue;
                }
                
                token = BuildToken(c);
                if(token.IsValid()) {
                    Token current_token = BuildToken(current_word);
                    if(current_token.IsValid()) {
                        tokens.Add(current_token);
                    }
                    
                    tokens.Add(token);
                    
                    current_word = string.Empty;
                    continue;
                }
                
                current_word += c;
            }
            
            if(!string.IsNullOrEmpty(current_word)) {
                tokens.Add(BuildToken(current_word));
                current_word = string.Empty;
            }
            
            return tokens.ToArray();
        }
        
        static Token BuildToken(char src) {
            switch (src) {

                // Punctuation
                case Syntax.semicolon:
                    return GetCommonToken(Token.TokenType.Semicolon);
                case Syntax.open_brace:
                    return GetCommonToken(Token.TokenType.OpenBrace);
                case Syntax.close_brace:
                    return GetCommonToken(Token.TokenType.CloseBrace);
                case Syntax.open_parenthesis:                        
                    return GetCommonToken(Token.TokenType.OpenParenthesis);
                case Syntax.close_parenthesis:
                    return GetCommonToken(Token.TokenType.CloseParenthesis);

                // Unary Operators
                case Syntax.operator_negation:
                    return new Token(Token.TokenType.UnaryOperator, Syntax.operator_negation);
                case Syntax.operator_subtraction:
                    return new Token(Token.TokenType.UnaryOperator, Syntax.operator_subtraction);
                case Syntax.operator_complement:
                    return new Token(Token.TokenType.UnaryOperator, Syntax.operator_complement);

                // Binay Operators
                case Syntax.operator_addition:
                    return new Token(Token.TokenType.BinaryOperator, Syntax.operator_addition);
                case Syntax.operator_multiplication:
                    return new Token(Token.TokenType.BinaryOperator, Syntax.operator_multiplication);
                case Syntax.operator_division:
                    return new Token(Token.TokenType.BinaryOperator, Syntax.operator_division);
                case Syntax.operator_modulus:
                    return new Token(Token.TokenType.BinaryOperator, Syntax.operator_modulus);
            }
            
            return Token.Invalid;
        }
        
        static Token BuildToken(string src) {
            Token token;
            token.Value = src;
            
            Syntax.Keyword type;
            if((type = Syntax.GetKeywordType(src)) != Syntax.Keyword.Invalid) {
                token.Type = Token.TokenType.Keyword;
                return token;
            }
            
            if(Regex.IsMatch(src, Syntax.integer_literal)) {
                token.Type = Token.TokenType.IntegerLiteral;
                return token;
            }
            
            if(Regex.IsMatch(src, Syntax.identifier)) {
                token.Type = Token.TokenType.Identifier;
                return token;
            }
            
            token.Type = Token.TokenType.Invalid;
            return token;
        }
    }

    public struct Token {
        public enum TokenType { Invalid, Semicolon, OpenBrace, CloseBrace, OpenParenthesis, CloseParenthesis, Keyword, Identifier, CharacterLiteral, IntegerLiteral, StringLiteral, UnaryOperator, BinaryOperator };
            
        public TokenType Type;
        public string Value;
        public Token(TokenType type, string value) {
            this.Type = type;
            this.Value = value;
        }
            
        public Token(TokenType type, char value) {
            this.Type = type;
            this.Value = value.ToString();
        }
            
        public bool IsValid() {
            return Type != TokenType.Invalid && !string.IsNullOrEmpty(Value);
        }
            
        public static Token Invalid {
            get { return new Token(TokenType.Invalid, string.Empty); }
        }
            
        public override string ToString() {
            return Type.ToString() + ", \"" + Value + "\"";
        }
    }
}