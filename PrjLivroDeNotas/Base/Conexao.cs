using System;
using System.Collections.Generic;
using System.Text;

namespace PrjLivroDeNotas.Base
{
    public class Conexao
    {
        public string GetConnectionSBEadSesi()
        {
            string Servidor = @"https://sandbox-eadsesi.fiep.digital//webservice//rest//server.php";

            String Token = @"?wstoken=";

            return Servidor + Token;
        }


        public string GetConnectionSBNEAD()
        {
            string Servidor = @"https://sandbox-nead.fiep.digital//webservice//rest//server.php";

            String Token = @"?wstoken=";

            return Servidor + Token;
        }

        public string GetConnectionSBFiepDigital()
        {
            string Servidor = @"https://sandbox-moodle.fiep.digital//webservice//rest//server.php/";

            String Token = @"?wstoken=8394b9b6995dd1c15ebdd663e695e0b3";

            return Servidor + Token;
        }
        //

        public string GetConnectionSBDEV()
        {
            string Servidor = @"https://sandbox.ava.dev.fiep.digital//webservice//rest//server.php/";

            String Token = @"?wstoken=d1b0c6025f3b875e6cd2dcc921a8b8e8";

            return Servidor + Token;
        }

        public string GetConnectionPRODUCAO()
        {
            string Servidor = @"https://ava.fiep.digital///webservice//rest//server.php/";

            String Token = @"?wstoken=2a57e43b96934e53e671a4aad8c98664";

            return Servidor + Token;
        }

        public string GetConnectionSandBoxDiario()
        {
            string Servidor = @"https://sandbox-ava.diario.fiep.digital//webservice//rest//server.php/";

            String Token = @"?wstoken=8d1191f2d2f46217169b172e018528d4";

            return Servidor + Token;
        }

        public string GetConnectionSandBox4()
        {
            string Servidor = @"https://sandbox-ava.moodle4dev.fiep.digital//webservice//rest//server.php/";

            String Token = @"?wstoken=9c54eb7e3e375d978c3c029f3e802fa7";

            return Servidor + Token;
        }
    }
}
