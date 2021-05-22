namespace MCC {
    public static class Syntax {
        
        public const char semicolon = ';';
        public const char open_brace = '{';
        public const char close_brace = '}';
        public const char open_parenthesis = '(';
        public const char close_parenthesis = ')';
        public const char single_quote = '\'';
        public const char double_quote = '\"';
        
        public const char operator_assignment = '=';
        
        public const char operator_addition = '+';
        public const char operator_subtraction = '-';
        public const char operator_multiplication = '*';
        public const char operator_division = '/';
        public const char operator_modulus = '%';

        public const char operator_negation = '!';
        public const char operator_complement = '~';

        public const string conditional_and = "&&";
        public const string conditional_or = "||";
        
        public const string identifier = @"[a-zA-Z]\w*";
        public const string integer_literal = @"[0-9]+";
        
        public static readonly string[] keywords = new string[] { "return", "char", "int", "string" };
        
        public enum Keyword { Invalid, Return, Character, Integer, String };
        
        public static string GetKeyword(Keyword k) {
            return keywords[(int)k - 1];
        }
        
        public static Keyword GetKeywordType(string s) {
            for (int i = 0; i < keywords.Length; i++) {
                if(s == keywords[i]) {
                    return (Keyword)(i + 1);
                }
            }
            return Keyword.Invalid;
        }
    }
}