using System;
using System.Collections.Generic;
using System.Text;

namespace wfCompilador
{
    public enum TokenType
    {
        Plus = '+',
        Minus = '-',
        Multiply = '*',
        Divide = '/',
        LParen = '(',
        RParen = ')',
        Excla = '!',
        DQuote = '"',
        Hashtag = '#',
        Porcentage = '%',
        And = '&',
        Coma = ',',
        Dot = '.',
        Colon = ':',
        SColon = ';',
        LThan = '<',
        Equal = '=',
        GThan = '>',
        RQuestion = '?',
        LSBracket = '[',
        RSBracket = ']',
        Exponent = '^',
        Underscore = '_',
        LCBracket = '{',
        Pipe = '|',
        RCBracket = '}',
        Tilde = '~',
        SQuote = '\'',
        EOF = (char)0,
        Empty = (char)1,
        Null = (char)2,
        Symbol = (char)3,
        Number = (char)4,
        NTerminal = (char)5,
        Terminal = (char)6

    }
}
