#define __APPLE__

namespace MCC {

    public static class Assembly {

        /*** FORMAT ***/

        // Use 'main' instead '_main' if not OS X
#if __APPLE__
        public const string format_function = "\t.globl _{0}\n_{0}:\n";
#else
        public const string format_function = "\t.globl {0}\n{0}:\n";
#endif

        // Statements
        public const string format_return = "\tret\n";

        // Expressions
        public const string format_constant = "\tmovl ${1}, {0}\n"; // accumulator, value

        // Unary Operators
        public const string format_negate = "\tneg {0}\n"; // accumulator
        public const string format_complement = "\tnot {0}\n"; // accumulator
        public const string format_not = "\tcmpl $0, {0}\n\tmovl $0, {0}\n\tsete %al\n"; // accumulator

        // Binary Operators
        public const string format_addition = "\taddl {0}, {1}\n"; // counter, accumulator
        public const string format_subtraction = "\tsubl {0}, {1}\n"; // counter, accumulator
        public const string format_multiplication = "\timul {0}, {1}\n"; // counter, accumulator
        public const string format_division = "\tidivl {0}, {1}\n"; // counter, accumulator
        public const string format_modulus = "\taddl {0}, {1}\n"; // counter, accumulator



        /*** REGISTERS ***/

        // Accumulator
        public const string register_A_64 = "%rax";
        public const string register_A_32 = "%eax";
        public const string register_A_16 = "%ax";
        public const string register_AH_8 = "%ah";
        public const string register_AL_8 = "%al";

        // Base
        public const string register_B_64 = "%rbx";
        public const string register_B_32 = "%ebx";
        public const string register_B_16 = "%bx";
        public const string register_BH_8 = "%bh";
        public const string register_BL_8 = "%bl";

        // Counter
        public const string register_C_64 = "%rcx";
        public const string register_C_32 = "%ecx";
        public const string register_C_16 = "%cx";
        public const string register_CH_8 = "%ch";
        public const string register_CL_8 = "%cl";

        // Data
        public const string register_D_64 = "%rdx";
        public const string register_D_32 = "%edx";
        public const string register_D_16 = "%dx";
        public const string register_DH_8 = "%dh";
        public const string register_DL_8 = "%dl";

        // Stack Pointer
        public const string register_SP_64 = "%rsp";
        public const string register_SP_32 = "%esp";
        public const string register_SP_16 = "%sp";
        public const string register_SP_8 = "%spl";

        // Stack Base Pointer
        public const string register_BP_64 = "%rbp";
        public const string register_BP_32 = "%ebp";
        public const string register_BP_16 = "%bp";
        public const string register_BP_8 = "%bpl";

        // Source
        public const string register_S_64 = "%rsi";
        public const string register_S_32 = "%esi";
        public const string register_S_16 = "%si";
        public const string register_S_8 = "%sil";

        // Destination
        public const string register_RD_64 = "%rdi";
        public const string register_RD_32 = "%edi";
        public const string register_RD_16 = "%di";
        public const string register_RD_8 = "%dil";
      
    }
}
