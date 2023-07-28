using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Farmacia_Curita_Feliz.clase
{
     class ClassConeccion
    {
        static string servidor = "DRH";
        static string BaseD = "FARMACIA_CURITA";

        string cadenaC = "Data Source="+servidor+";Initial Catalog="+BaseD+";Integrated Security=True";

       
        public SqlConnection Conectar_Open()
        {
            SqlConnection conection =new SqlConnection(cadenaC);



                conection.Open();
               

            return conection;
        }
       

    }
}
