using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace wfCompilador
{
    public class Scanner
    {
        private string _regexp = "";
        private int _index = 0;
        private int _state = 0;
        public Scanner(string regexp)
        {
            _regexp = regexp + (char)TokenType.EOF;
            _index = 0;
            _state = 0;
        }
        public Token GetToken()
        {
            Token result = new Token() { Value = ((char)0).ToString() };
            bool tokenFound = false;
            string cadena = null;
            while (!tokenFound)
            {
                char peek = _regexp[_index];
                switch (_state)
                {
                    case 0:

                        while (char.IsWhiteSpace(peek))
                        {
                            _index++;
                            peek = _regexp[_index];

                        }

                        switch (peek)
                        {
                            case (char)TokenType.SQuote:
                                cadena = cadena + peek;
                                _state = 4;
                                break;
                            case (char)TokenType.Underscore:
                                cadena = cadena + peek;
                                _state = 3;
                                break;
                            case (char)TokenType.Plus:
                            case (char)TokenType.Minus:
                            case (char)TokenType.Multiply:
                            case (char)TokenType.Divide:
                            case (char)TokenType.LParen:
                            case (char)TokenType.RParen:
                            case (char)TokenType.Excla:
                            case (char)TokenType.DQuote:
                            case (char)TokenType.Hashtag:
                            case (char)TokenType.Porcentage:
                            case (char)TokenType.And:
                            case (char)TokenType.Coma:
                            case (char)TokenType.Dot:
                            case (char)TokenType.Colon:
                            case (char)TokenType.SColon:
                            case (char)TokenType.LThan:
                            case (char)TokenType.Equal:
                            case (char)TokenType.GThan:
                            case (char)TokenType.RQuestion:
                            case (char)TokenType.LSBracket:
                            case (char)TokenType.RSBracket:
                            case (char)TokenType.Exponent:                       
                            case (char)TokenType.LCBracket:
                            case (char)TokenType.Pipe:
                            case (char)TokenType.RCBracket:
                            case (char)TokenType.Tilde:
                            case (char)TokenType.EOF:
                            case (char)TokenType.Empty:
                            case (char)TokenType.Null:
                            case (char)TokenType.Symbol:
                            case (char)TokenType.Number:
                                tokenFound = true;
                                result.Tag = (TokenType)peek;
                                result.Value = peek.ToString();
                                break;
                            default:
                                if (char.IsDigit(peek))
                                {
                                    cadena = cadena + peek;
                                    _state = 2;


                                }
                                else if (char.IsLetter(peek))
                                {
                                    cadena = cadena + peek;
                                    _state = 3;
                                }
                                else
                                {
                                    MessageBox.Show("Lex Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    // throw new Exception("Lex Error");
                                }
                                break;
                        }
                        break;
                    case 1:
                        switch (peek)
                        {
                            case (char)TokenType.LParen:
                            case (char)TokenType.RParen:
                            case (char)TokenType.Plus:
                            case (char)TokenType.Multiply:
                            case (char)TokenType.Minus:
                            case (char)TokenType.Divide:
                            case '\\':
                            case ' ':
                                tokenFound = true;
                                result.Tag = TokenType.Symbol;
                                result.Value = peek.ToString();
                                break;
                            case 'E':
                                tokenFound = true;
                                result.Tag = TokenType.Null;
                                break;
                            case '0':
                                tokenFound = true;
                                result.Tag = TokenType.Empty;
                                break;
                            default:
                                MessageBox.Show("Lex Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                               // throw new Exception("Lex Error");
                        }
                        break;
                    case 2:

                        if (char.IsDigit(peek))
                        {
                            cadena = cadena + peek;
                            //Console.WriteLine(cadena);
                            _state = 2;

                        }
                        else
                        {


                            tokenFound = true;
                            result.Tag = TokenType.Number;
                            result.Value = cadena;
                            _state = 0;
                            _index--;
                        }
                        break;
                    case 3:

                        if (char.IsLetter(peek) || peek.Equals('_')|| char.IsDigit(peek))
                        {
                            cadena = cadena + peek;
                            //Console.WriteLine(cadena);
                            _state = 3;

                        }
                        else
                        {
                            tokenFound = true;
                            result.Tag = TokenType.NTerminal;
                            result.Value = cadena;
                            _state = 0;
                            _index--;
                        }
                        break;
                    case 4:

                        if (peek.Equals('\''))
                        {
                            cadena = cadena + peek;
                            tokenFound = true;
                            result.Tag = TokenType.Terminal;
                            result.Value = cadena;
                            _state = 0;
                            //_index--;

                        }
                        else
                        {
                            cadena = cadena + peek;
                            //Console.WriteLine(cadena);
                            _state = 4;
                        }
                        break;
                    default:
                        break;
                }
                _index++;
            }//while token found
            _state = 0;
            return result;
        }//get token

    }
}
