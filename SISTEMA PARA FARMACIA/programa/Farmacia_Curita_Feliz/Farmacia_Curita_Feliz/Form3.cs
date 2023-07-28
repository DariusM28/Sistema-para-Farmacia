using Farmacia_Curita_Feliz.clase;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Farmacia_Curita_Feliz
{
   
    public partial class Cajas : Form
    {
        
        public static class var
        {
            public static int r;
        }
        public Cajas()
        {
            InitializeComponent();
            cmd();
            roles();
        }
        public void roles()
        {
            string query = " SELECT ROL4,ROL5,ACTIVO FROM USUARIOS WHERE US='" + label7.Text + "';";
            SqlCommand chek1 = new SqlCommand(query, conex.Conectar_Open());
            chek1.ExecuteNonQuery();
            conex.Conectar_Open();
            SqlDataReader registro = chek1.ExecuteReader();
            if (registro.Read())
            {
                label10.Text = registro["ROL4"].ToString();
                label11.Text = registro["ROL5"].ToString();
                label12.Text = registro["ACTIVO"].ToString();

            }
            if (label10.Text == "False")
            {
               
                MessageBox.Show("USUARIO NO TIENE PERMISO PARA VENDER ");
            }
            if (label12.Text == "False")
            {
                button1.Enabled = false;
                button3.Enabled = false;
                MessageBox.Show("USUARIO NO INICIADO ");
            }

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
            fila1["NOMBRE_PRODUC"] = "PRODUCTOS";
            table1.Rows.InsertAt(fila1, 0);
            comboBox12.DisplayMember = "NOMBRE_PRODUC";
            comboBox12.DataSource = table1;

            string query2 = "SELECT MARCA_PRODUC,COUNT(MARCA_PRODUC)AS MARC FROM PRODUCTO GROUP BY MARCA_PRODUC HAVING COUNT (MARCA_PRODUC)>0 ;";
            SqlCommand Comando_consulta2 = new SqlCommand(query2, conex.Conectar_Open());
            SqlDataAdapter dato2 = new SqlDataAdapter(Comando_consulta2);
            DataTable table2 = new DataTable();
            dato2.Fill(table2);
            DataRow fila2 = table2.NewRow();
            fila2["MARCA_PRODUC"] = "MARCAS";
            table2.Rows.InsertAt(fila2, 0);
            comboBox10.DisplayMember = "MARCA_PRODUC";
            comboBox10.DataSource = table2;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.BackColor=Color.White;button2.BackColor = Color.DarkGray;
            panel_VENTA.Visible = true;panel_BUSCAR.Visible = false;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.DarkGray; button2.BackColor = Color.White;
            panel_VENTA.Visible = false; panel_BUSCAR.Visible = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Visible = true;
            checkBox1 .Visible = false; 
         Random s = new Random();
            var.r = s.Next(111111, 999999);
            MessageBox.Show("" +var.r);


        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Visible = false; 
            checkBox1.Visible = true; 

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string query = "SELECT CANTIDAD_PRODUCTO,PRECIO, IMAGEN,DESCRIPCION_PRODUCTO FROM PRODUCTO WHERE NOMBRE_PRODUC='" + comboBox12.Text + "'and MARCA_PRODUC='" + comboBox10.Text + "' AND TIPO_ESTDO_MATERIA='" + comboBox11.Text + "';";
            SqlCommand chek1 = new SqlCommand(query, conex.Conectar_Open());
            chek1.ExecuteNonQuery();
            conex.Conectar_Open();
            SqlDataReader registro = chek1.ExecuteReader();
            if (registro.Read())
            {
                textBox21.Text = registro["CANTIDAD_PRODUCTO"].ToString();
                textBox22.Text = registro["PRECIO"].ToString();

                byte[] s = (byte[])registro["IMAGEN"];
                System.IO.MemoryStream ms = new MemoryStream(s);
               // pic.Image = System.Drawing.Bitmap.FromStream(ms);

                textBox1.Text = registro["DESCRIPCION_PRODUCTO"].ToString();
            }
            else
            {
                MessageBox.Show("PRODUCTO NO ENCONTRADO");
            }
           

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            float presio, subtotal,cantidad;
            presio=float.Parse(textBox22.Text);
            cantidad=float.Parse(textBox2.Text);
            subtotal= presio*cantidad;

            Cajas s = new Cajas();  
            string queryInsert = "INSERT INTO VENTAS( NUMERO_FACTURA,NOMBRE_PRODUC,MARCA_PRODUC,TIPO_ESTDO_MATERIA,CANTIDAD_PRODUCTO,NIT,NOMBRE_FACTURA,US,PRECIO,SUB_TOTAL)" +
                " VALUES('"+var.r+"','"+comboBox12.Text+"','"+comboBox10.Text+"','"+comboBox11.Text+"','"+textBox2.Text+"','"+textBox6.Text+"','"+textBox3.Text +"','"+label7.Text+"','"+textBox22.Text+"','"+subtotal+"')";
            SqlCommand comando = new SqlCommand(queryInsert, conex.Conectar_Open());
            comando.ExecuteNonQuery();
            conex.Conectar_Open();
            MessageBox.Show("SE AGREGO PRODUCTO");
            // SI AGREGO A LA FACUTRA

            string query = "SELECT NOMBRE_PRODUC,MARCA_PRODUC,TIPO_ESTDO_MATERIA,CANTIDAD_PRODUCTO,PRECIO,SUB_TOTAL FROM VENTAS WHERE NUMERO_FACTURA= '"+var.r+"';";
            SqlCommand Comando_consulta = new SqlCommand(query, conex.Conectar_Open());
            SqlDataAdapter dato = new SqlDataAdapter(Comando_consulta);
            DataTable table = new DataTable();
            dato.Fill(table);
            dataGridView1.DataSource = table;
            // TOTAL 
            string query2 ="SELECT SUM(SUB_TOTAL) AS TOTAL FROM VENTAS WHERE NUMERO_FACTURA = '"+var.r+"';";
            SqlCommand chek1 = new SqlCommand(query2, conex.Conectar_Open());
            chek1.ExecuteNonQuery();
            conex.Conectar_Open();
            SqlDataReader registro = chek1.ExecuteReader();
            if (registro.Read())
            {
                textBox5.Text = registro["TOTAL"].ToString();

            }
            else
            {
                MessageBox.Show("PRODUCTO NO ENCONTRADO");
            }
            //modificar cantidades
            float canti1,canti2,totlcant,salto;
            canti1 = float.Parse(textBox2.Text);
            canti2 = float.Parse(textBox21.Text);
            totlcant = canti2-canti1;
            salto = presio * totlcant;

            string insertinto = "UPDATE PRODUCTO SET CANTIDAD_PRODUCTO='" + totlcant + "',PRECIO_TOTAL='" +salto+ "'  WHERE NOMBRE_PRODUC='" + comboBox12.Text + "'and MARCA_PRODUC='" + comboBox10.Text + "' AND TIPO_ESTDO_MATERIA='" + comboBox11.Text + "';";
            SqlCommand comando1 = new SqlCommand(insertinto, conex.Conectar_Open());
            comando1.ExecuteNonQuery();
            conex.Conectar_Open();
            MessageBox.Show("Se acualizo exitozamente");








            comboBox12.Text = "";comboBox10.Text = "";comboBox11.Text = "";
            textBox21.Clear();textBox22.Clear();textBox1.Clear();textBox2.Clear();
            pic.Image=pictureBox1.Image;
        }
        // buton agregar
        private void button4_Click(object sender, EventArgs e)
        {
            Cajas s = new Cajas();
            string queryInsert = "INSERT INTO FACTURAS(NUMERO_FACTURA,NIT,NOMBRE_FACTURA,TOTAL,US) VALUES('"+var.r+"','"+textBox6.Text+"','"+textBox3.Text+"','"+textBox5.Text+"','"+label7.Text+"');";
            SqlCommand comando = new SqlCommand(queryInsert, conex.Conectar_Open());
            comando.ExecuteNonQuery();
            conex.Conectar_Open();
            MessageBox.Show("SE AGREGO PRODUCTO");
            dataGridView1.Columns.Clear();
            textBox6.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            float recibido, toala, vuelto;
           // recibido=float.Parse(textBox4.Text);
            // toala=float.Parse(textBox5.Text);
             //vuelto = recibido-toala;
            //MessageBox.Show("EL CAMBIO ES DE "+vuelto);

        }

        private void Cajas_Load(object sender, EventArgs e)
        {
            
            
          
        }
    }
}
