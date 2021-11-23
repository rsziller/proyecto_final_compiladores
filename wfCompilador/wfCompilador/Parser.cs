using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace wfCompilador
{
    class Parser
    {
        Scanner _scanner;
        Token _token;
        bool aceptar;
        string mensaje;

        public bool gramatica_aceptada ()
        {
            return aceptar;
        }

        public string mensaje_error()
        {
            return this.mensaje;
        }

        private void A()
        {
            switch (_token.Tag)
            {

                case TokenType.NTerminal:
                    //Match(_token.Tag);
                    result();
                    Match(TokenType.Colon);
                    B();
                    break;
                default:
                    break;
                    //throw new Exception("Error de sintaxis");
            }
        }
        private void result()
        {
            switch (_token.Tag)
            {

                case TokenType.NTerminal:
                    Match(TokenType.NTerminal);
                    break;
                default:
                    //break;
                    aceptar = false;
                    this.mensaje = "Parser Error";
                    // MessageBox.Show("Error de sintaxis", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                    // throw new Exception("Error de sintaxis");
            }
        }

        private void Z()
        {
            switch (_token.Tag)
            {

                case TokenType.NTerminal:
                case TokenType.Terminal:
                    Match(_token.Tag);
                    //rule();
                    break;
                default:
                    break;

            }
        }

        private void rule()
        {
            switch (_token.Tag)
            {

                case TokenType.NTerminal:
                case TokenType.Terminal:
                    Match(_token.Tag);
                    
                    Z();
                    rule();
                    break;
                default:
                    break;
                    //throw new Exception("Error de sintaxis");

            }
        }

        private void C()
        {
            switch (_token.Tag)
            {

                case TokenType.Pipe:
                    Match(TokenType.Pipe);
                    rule();
                    C();
                    break;
                default:
                    break;

            }
        }

        private void B()
        {
            switch (_token.Tag)
            {

                case TokenType.NTerminal:
                case TokenType.Terminal:
                    Match(_token.Tag);
                    rule();    
                    C();
                    Match(TokenType.SColon);
                    A();
                    break;
                default:
                    //break;
                    // throw new Exception("Error de sintaxis");
                    aceptar = false;
                    this.mensaje = "Parser Error";
                    // MessageBox.Show("Error de sintaxis", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }



        private void Match(TokenType tag)
        {
            if (_token.Tag == tag)
            {
                _token = _scanner.GetToken();

            }
            else
            {
                // throw new Exception("Error de sintaxis");
                aceptar = false;
                this.mensaje = "Parser Error";
                // MessageBox.Show("Error de sintaxis", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Parse(string regexp)
        {
            this.aceptar = true;
            this.mensaje = "";

            _scanner = new Scanner(regexp + (char)TokenType.EOF);
            _token = _scanner.GetToken();
            switch (_token.Tag)
            {

                case TokenType.NTerminal:
                    A();
                    break;
                default:
                    break;
            }

            Match(TokenType.EOF);
        }

    }
}
