	.globl _main
_main:
	movl $1, %eax
	neg %eax
	ret

Keyword, "int"
Identifier, "main"
OpenParenthesis, "("
CloseParenthesis, ")"
OpenBrace, "{"
Keyword, "return"
UnaryOperator, "-"
IntegerLiteral, "1"
Semicolon, ";"
CloseBrace, "}"

main:
return<-1>
