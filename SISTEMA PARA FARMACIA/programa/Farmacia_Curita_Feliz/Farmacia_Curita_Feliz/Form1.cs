using Farmacia_Curita_Feliz.clase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Farmacia_Curita_Feliz
{
    public partial class Form1 : Form
    {
        ClassConeccion conex = new ClassConeccion();
        public Form1()
        {
            InitializeComponent();
            
        }

       
        // VALIDAR
        private void button1_Click(object sender, EventArgs e)
        {
           
            

            string query = "SELECT US,CONT,PUESTO FROM USUARIOS WHERE US='"+textBox1.Text+"';";
            SqlCommand chek1 = new SqlCommand(query, conex.Conectar_Open());
            chek1.ExecuteNonQuery();
            conex.Conectar_Open();
            SqlDataReader registro = chek1.ExecuteReader();
            if (registro.Read())
            {
                label4.Text = registro["US"].ToString();
                label5.Text= registro["CONT"].ToString();
               label6.Text= registro["PUESTO"].ToString();
               
            }

            

            if (textBox1.Text == label4.Text && textBox2.Text == label5.Text && comboBox1.Text == label6.Text)
            {
                switch (comboBox1.Text)
                {
                    case "Cajero":
                   
                        Cajas cajas = new Cajas();
                        cajas.Text = textBox1.Text;
                        cajas.label7.Text= textBox1.Text;  
                       cajas.Show();
                        this.Hide();    
                        


                        break;
                    case "Administracion":
                        Adm adm = new Adm();
                        adm.Text = this.textBox1.Text;
                        adm.Show();
                        this.Hide();
                        break;
                    default:
                        MessageBox.Show("Usurio o Contraseña\n Incorrecots");
                        break;
                }

            }
            else
            {
                MessageBox.Show("Usurio o Contraseña\n Incorrecots");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
         
        }
    }
}
