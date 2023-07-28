using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Farmacia_Curita_Feliz.clase;
using System.Collections;
using System.IO;

namespace Farmacia_Curita_Feliz
{
    
    public partial class Adm : Form
    {
        public Adm()
        {
            InitializeComponent();


        }

        ClassConeccion conex = new ClassConeccion();
        public void cmd()
        {// nombre
            string query1 = "SELECT NOMBRE_PRODUC, COUNT(NOMBRE_PRODUC)AS CUAN FROM PRODUCTO GROUP BY NOMBRE_PRODUC HAVING COUNT (NOMBRE_PRODUC)>0;";
            SqlCommand Comando_consulta1 = new SqlCommand(query1, conex.Conectar_Open());
            SqlDataAdapter dato1 = new SqlDataAdapter(Comando_consulta1);
            DataTable table1 = new DataTable();
            dato1.Fill(table1);
            DataRow fila1 = table1.NewRow();
            fila1["NOMBRE_PRODUC"] = "MARCAS";
            table1.Rows.InsertAt(fila1, 0);
            comboBox12.DisplayMember = "NOMBRE_PRODUC";
            comboBox12.DataSource = table1;

            string query2 = "SELECT MARCA_PRODUC,COUNT(MARCA_PRODUC)AS MARC FROM PRODUCTO GROUP BY MARCA_PRODUC HAVING COUNT (MARCA_PRODUC)>0 ;";
            SqlCommand Comando_consulta2 = new SqlCommand(query2, conex.Conectar_Open());
            SqlDataAdapter dato2 = new SqlDataAdapter(Comando_consulta2);
            DataTable table2= new DataTable();
            dato2.Fill(table2);
            DataRow fila2= table2.NewRow();
            fila2["MARCA_PRODUC"] = "MARCAS";
            table2.Rows.InsertAt(fila2, 0);
            comboBox10.DisplayMember = "MARCA_PRODUC";
            comboBox10.DataSource =table2;

        }
       

        
        
        public void validardia()
        {
            label25.Visible = true;
            label26.Visible = true;
            label27.Visible = true;

            string query1 = "SELECT US,ACTIVO FROM USUARIOS WHERE ID_PERSONA='2';";
            string query2 = "SELECT US,ACTIVO FROM USUARIOS WHERE ID_PERSONA='3';";
            string query3 = "SELECT US,ACTIVO FROM USUARIOS WHERE ID_PERSONA='4';";
            // primer query
            SqlCommand chek1 = new SqlCommand(query1, conex.Conectar_Open());
            chek1.ExecuteNonQuery();
            conex.Conectar_Open();
            SqlDataReader registro = chek1.ExecuteReader();
            if (registro.Read())
            {
                textBox8.Text = registro["US"].ToString();
                label25.Text = registro["ACTIVO"].ToString();
                if (label25.Text == "False")
                {
                    checkBox2.Enabled = true;
                    label25.BackColor = Color.Red;
                }
                else
                {
                    checkBox2.Enabled = false;
                    checkBox7.Enabled = true;
                    label25.BackColor = Color.Lime;
                }
            }
            // segundoquery
            SqlCommand chek2 = new SqlCommand(query2, conex.Conectar_Open());
            chek2.ExecuteNonQuery();
            conex.Conectar_Open();
            SqlDataReader registro2 = chek2.ExecuteReader();
            if (registro2.Read())
            {
                textBox9.Text = registro2["US"].ToString();
                label26.Text = registro2["ACTIVO"].ToString();
                if (label26.Text == "False")
                {
                    checkBox3.Enabled = true;
                    label26.BackColor = Color.Red;
                }
                else
                {
                    checkBox3.Enabled = false;
                    checkBox6.Enabled = true;
                    label26.BackColor = Color.Lime;
                }
            } // Segundo query
            SqlCommand chek3 = new SqlCommand(query3, conex.Conectar_Open());
            chek3.ExecuteNonQuery();
            conex.Conectar_Open();
            SqlDataReader registro3 = chek3.ExecuteReader();
            if (registro3.Read())
            {
                textBox10.Text = registro3["US"].ToString();
                label27.Text = registro3["ACTIVO"].ToString();
                if (label27.Text == "False")
                {
                    checkBox4.Enabled = true;
                    label27.BackColor = Color.Red;
                }
                else
                {
                    checkBox4.Enabled = false;
                    checkBox5.Enabled = true;
                    label27.BackColor = Color.Lime;
                }
            }

        }
        public void Consulta_usuarios()
        {
            string query = "SELECT  NOMBRE, APELLIDO FROM USUARIOS";
            SqlCommand Comando_consulta = new SqlCommand(query,conex.Conectar_Open());
            SqlDataAdapter dato = new SqlDataAdapter(Comando_consulta);
            DataTable table = new DataTable();
            dato.Fill(table);
            dataGridView1.DataSource = table;
           

        }


      
        // boton para guardar usuario
        private void button4_Click(object sender, EventArgs e)
        {
            
            string insertinto = "INSERT INTO USUARIOS (PUESTO, NOMBRE, APELLIDO, EDAD,SEXO, ROL1 ,ROL2 ,ROL3 ,ROL4 ,ROL5 ,US,CONT) VALUES('"+comboBox1.Text+"','"+textBox1.Text+ "','"+textBox2.Text+"','"+textBox3.Text+"', '"+comboBox2.Text+"','"+comboBox3.Text+ "','"+comboBox4.Text+ "','"+comboBox5.Text+ "','"+comboBox6.Text+ "','"+comboBox7.Text+"','"+textBox4.Text+"','"+textBox5.Text+"');";
            SqlCommand comando = new SqlCommand(insertinto,conex.Conectar_Open());
            comando.ExecuteNonQuery();
            conex.Conectar_Open();
            MessageBox.Show("usuario ingresado correcatamente");
            // limpiar campos
            textBox1.Clear();textBox1.Clear();
            textBox2.Clear();textBox2.Clear();
            textBox3.Clear();textBox3.Clear();
            textBox4.Clear();textBox4.Clear();
            textBox5.Clear();textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
           
            Consulta_usuarios();
            // Consulta_usuarios();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel_crear.Visible = true;panel_Cuadre.Visible = false;panel_inventario.Visible = false;
            button1.BackColor = Color.White; button2.BackColor = Color.DarkGray;button3.BackColor = Color.DarkGray;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Consulta_usuarios();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel_crear.Visible = false; panel_Cuadre.Visible = true; panel_inventario.Visible = false;
            button1.BackColor = Color.DarkGray;button2.BackColor=Color.White;button3.BackColor=Color.DarkGray;
            panel_Cuadre.Visible = true;
        }

       

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            validardia();

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            string insertinto = "UPDATE USUARIOS SET ACTIVO='True'  WHERE ID_PERSONA=2;";
            SqlCommand comando = new SqlCommand(insertinto, conex.Conectar_Open());
            comando.ExecuteNonQuery();
            conex.Conectar_Open();
            MessageBox.Show("CAJA 1 ACTIVADO");
            validardia();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            string insertinto = "UPDATE USUARIOS SET ACTIVO='True'  WHERE ID_PERSONA=3;";
            SqlCommand comando = new SqlCommand(insertinto, conex.Conectar_Open());
            comando.ExecuteNonQuery();
            conex.Conectar_Open();
            MessageBox.Show("CAJA 2 ACTIVADO");
            validardia();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            string insertinto = "UPDATE USUARIOS SET ACTIVO='True'  WHERE ID_PERSONA=4;";
            SqlCommand comando = new SqlCommand(insertinto, conex.Conectar_Open());
            comando.ExecuteNonQuery();
            conex.Conectar_Open();
            MessageBox.Show("CAJA 3 ACTIVADO");
            validardia();
        }
        // FINALIZAR USUARIOS
        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            string insertinto = "UPDATE USUARIOS SET ACTIVO='False'  WHERE ID_PERSONA=2;";
            SqlCommand comando = new SqlCommand(insertinto, conex.Conectar_Open());
            comando.ExecuteNonQuery();
            conex.Conectar_Open();
            MessageBox.Show("CAJA 1 DESACTIVADO");
            validardia();
            checkBox7.Enabled = false;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            string insertinto = "UPDATE USUARIOS SET ACTIVO='False'  WHERE ID_PERSONA=3;";
            SqlCommand comando = new SqlCommand(insertinto, conex.Conectar_Open());
            comando.ExecuteNonQuery();
            conex.Conectar_Open();
            MessageBox.Show("CAJA 2 DESACTIVADO");
            validardia();
            checkBox6.Enabled = false;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            string insertinto = "UPDATE USUARIOS SET ACTIVO='False'  WHERE ID_PERSONA=4;";
            SqlCommand comando = new SqlCommand(insertinto, conex.Conectar_Open());
            comando.ExecuteNonQuery();
            conex.Conectar_Open();
            MessageBox.Show("CAJA 3 DESACTIVADO");
            validardia();
            checkBox5.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmd();
            panel_crear.Visible = false; panel_Cuadre.Visible = false; panel_inventario.Visible = true;
            button1.BackColor = Color.DarkGray; button2.BackColor = Color.DarkGray; button3.BackColor = Color.White;
        }

        private void panel_inventario_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog fo = new OpenFileDialog();   
            DialogResult s = fo.ShowDialog();
            if (s == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile( fo.FileName );   
            }
        }

        public void buscarproductos()
        {
            string query = "SELECT NOMBRE_PRODUC,CANTIDAD_PRODUCTO,PRECIO FROM PRODUCTO WHERE NOMBRE_PRODUC='" + comboBox12.Text + "'and MARCA_PRODUC='" + comboBox10.Text + "' AND TIPO_ESTDO_MATERIA='" + comboBox11.Text + "';";
            SqlCommand chek1 = new SqlCommand(query, conex.Conectar_Open());
            chek1.ExecuteNonQuery();
            conex.Conectar_Open();
            SqlDataReader registro = chek1.ExecuteReader();
            if (registro.Read())
            {
                textBox24.Text = registro["NOMBRE_PRODUC"].ToString();
                textBox25.Text = registro["CANTIDAD_PRODUCTO"].ToString();
                textBox26.Text = registro["PRECIO"].ToString();
            }
            else
            {
                MessageBox.Show("PRODUCTO NO ENCONTRADO");
            }
        }
        private void button11_Click(object sender, EventArgs e)
        {
            
            float cant,pre, sub;
            cant = float.Parse( textBox21.Text );
            pre= float.Parse( textBox22.Text );
            sub = cant * pre;
            // convertir a byts la imagen para alamacenarla en la base de datos
            System.IO.MemoryStream ms = new System.IO.MemoryStream();   
            pictureBox1.Image.Save(ms,System.Drawing.Imaging.ImageFormat.Jpeg);  

            string insertinto = "INSERT INTO PRODUCTO(NOMBRE_PRODUC,MARCA_PRODUC,DESCRIPCION_PRODUCTO,IMAGEN,TIPO_ESTDO_MATERIA,CANTIDAD_PRODUCTO,PRECIO,PRECIO_TOTAL)" +
                "VALUES('"+textBox18.Text+"','"+textBox19.Text+"','"+textBox20.Text+"','"+ms.GetBuffer()+"','"+comboBox9.Text+"','"+textBox21.Text+"','"+textBox22.Text+"','"+sub+"');";
            SqlCommand comando = new SqlCommand(insertinto, conex.Conectar_Open());
            comando.ExecuteNonQuery();
            conex.Conectar_Open();
            MessageBox.Show("PRODUCTO GUARDAO \n CON EXITO");

            // limpiear campos
            textBox18.Clear();comboBox9.Text = " ";
            textBox19.Clear();pictureBox1.Image = pictureBox2.Image;
            textBox20.Clear();
            textBox21.Clear();
            textBox22.Clear();
            cmd();
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            textBox26.Enabled = true;
        }

       
        // busca para agregar
        private void button8_Click(object sender, EventArgs e)
        {
            buscarproductos();
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
              
            float cant_anterior, cant_actual, Totalcanti,precioante, preciotoal;
            cant_anterior=float.Parse(textBox25.Text);
            if (textBox27.Text=="")
            {
                cant_actual = 0;
            }
            else
            {
                cant_actual = float.Parse(textBox27.Text);
            }
            
            Totalcanti= cant_actual+cant_anterior;// nueva cantidad
            precioante=float.Parse(textBox26.Text);

            preciotoal = precioante * Totalcanti;// nuevo total precio

            // acualizar
            string insertinto = "UPDATE PRODUCTO SET CANTIDAD_PRODUCTO='"+Totalcanti+"', PRECIO='"+textBox26.Text+"',PRECIO_TOTAL='"+preciotoal+ "'  WHERE NOMBRE_PRODUC='" + comboBox12.Text + "'and MARCA_PRODUC='" + comboBox10.Text+ "' AND TIPO_ESTDO_MATERIA='" + comboBox11.Text + "';";
            SqlCommand comando = new SqlCommand(insertinto, conex.Conectar_Open());
            comando.ExecuteNonQuery();
            conex.Conectar_Open();
            buscarproductos();
            textBox27.Clear();
            checkBox9.Checked = false;
            textBox26.Enabled = false;
            MessageBox.Show("Se acualizo exitozamente");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string query = "SELECT NOMBRE_PRODUC,MARCA_PRODUC,TIPO_ESTDO_MATERIA, CANTIDAD_PRODUCTO,PRECIO,PRECIO_TOTAL FROM PRODUCTO";
            SqlCommand Comando_consulta = new SqlCommand(query, conex.Conectar_Open());
            SqlDataAdapter dato = new SqlDataAdapter(Comando_consulta);
            DataTable table = new DataTable();
            dato.Fill(table);
            dataGridView2.DataSource = table;
        }

        private void panel_Cuadre_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            switch (comboBox8.Text)
            {

                case "Caja 1":
                    string query = "SELECT  SUM(SUB_TOTAL)as SALDO ,COUNT(NUMERO_FACTURA)as NVENTAS,SUM(CANTIDAD_PRODUCTO)as PRODUCTOS FROM VENTAS WHERE US='"+textBox8.Text+"';";
                    SqlCommand chek1 = new SqlCommand(query, conex.Conectar_Open());
                    chek1.ExecuteNonQuery();
                    conex.Conectar_Open();
                    SqlDataReader registro = chek1.ExecuteReader();
                    if (registro.Read())
                    {
                        textBox11.Text = textBox8.Text;
                        textBox14.Text = registro["SALDO"].ToString();
                       textBox13.Text = registro["NVENTAS"].ToString();
                        textBox12.Text = registro["PRODUCTOS"].ToString();
                    }

                    break;
                case "Caja 2":
                    string query1 = "SELECT  SUM(SUB_TOTAL)as SALDO ,COUNT(NUMERO_FACTURA)as NVENTAS,SUM(CANTIDAD_PRODUCTO)as PRODUCTOS FROM VENTAS WHERE US='" + textBox9.Text + "';";
                    SqlCommand chek12 = new SqlCommand(query1, conex.Conectar_Open());
                    chek12.ExecuteNonQuery();
                    conex.Conectar_Open();
                    SqlDataReader registro1 = chek12.ExecuteReader();
                    if (registro1.Read())
                    {
                        textBox11.Text = textBox9.Text;
                        textBox14.Text = registro1["SALDO"].ToString();
                        textBox13.Text = registro1["NVENTAS"].ToString();
                        textBox12.Text = registro1["PRODUCTOS"].ToString();
                    }
                    break;

                case"Caja 3":
                    string query3 = "SELECT  SUM(SUB_TOTAL)as SALDO ,COUNT(NUMERO_FACTURA)as NVENTAS,SUM(CANTIDAD_PRODUCTO)as PRODUCTOS FROM VENTAS WHERE US='" + textBox10.Text + "';";
                    SqlCommand chek13 = new SqlCommand(query3, conex.Conectar_Open());
                    chek13.ExecuteNonQuery();
                    conex.Conectar_Open();
                    SqlDataReader registro3 = chek13.ExecuteReader();
                    if (registro3.Read())
                    {
                        textBox11.Text = textBox10.Text;
                        textBox14.Text = registro3["SALDO"].ToString();
                        textBox13.Text = registro3["NVENTAS"].ToString();
                        textBox12.Text = registro3["PRODUCTOS"].ToString();
                    }

                    break;
                default:
                    MessageBox.Show("DEBE VALIDAR DIA");
                    break;
                    
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string query3 = "SELECT  SUM(SUB_TOTAL)as SALDO ,COUNT(NUMERO_FACTURA)as NVENTAS,SUM(CANTIDAD_PRODUCTO)as PRODUCTOS FROM VENTAS;";
            SqlCommand chek13 = new SqlCommand(query3, conex.Conectar_Open());
            chek13.ExecuteNonQuery();
            conex.Conectar_Open();
            SqlDataReader registro3 = chek13.ExecuteReader();
            if (registro3.Read())
            {
               
                textBox15.Text = registro3["SALDO"].ToString();
                textBox16.Text = registro3["NVENTAS"].ToString();
                textBox17.Text = registro3["PRODUCTOS"].ToString();
            }

        }
    }
}
