# Mitchell C Compiler (mcc) 2020

import sys, os, re

# expected form of a C program
source_re = r"int main\s*\(\s*\)\s*{\s*return\s+(?P<ret>[0-9]+)\s*;\s*}" 

# if not OS X, use 'main' instead of '_main'

assembly_format = """    
    .globl _main
_main:
    movl    ${}, %eax
    ret
"""

source_file = sys.argv[1]
assembly_file = os.path.splitext(source_file)[0] + ".s"

with open(source_file, 'r') as infile, open(assembly_file, 'w') as outfile:
    source = infile.read().strip()
    match = re.match(source_re, source)
    
    # extract named 'ret' group, containing the return value
    retval = match.group('ret')
    outfile.write(assembly_format.format(retval))