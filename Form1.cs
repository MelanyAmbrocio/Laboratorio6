using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratorio6
{
    public partial class Form1 : Form
    {
        List<Cliente> clientes = new List<Cliente>();
        List<Vehiculo> vehiculos = new List<Vehiculo>();
        List<Alquiler> alquileres = new List<Alquiler>();
        List<Reporte> reportes = new List<Reporte>();

        public Form1()
        {
            InitializeComponent();
        }
        private void LeerClientes()
        {

            FileStream stream = new FileStream("Clientes.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Cliente cliente = new Cliente();
                cliente.Nit = reader.ReadLine();
                cliente.Nombre = reader.ReadLine();
                cliente.Direccion = reader.ReadLine();
                clientes.Add(cliente);

            }
            reader.Close();
        }
        private void LeerAlquileres()
        {

            FileStream stream = new FileStream("Alquiler.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Alquiler alquiler = new Alquiler();
                alquiler.Nit = reader.ReadLine();
                alquiler.Placa = reader.ReadLine();
                alquiler.FechaAlquiler = Convert.ToDateTime(reader.ReadLine());
                alquiler.FechaDevolucion = Convert.ToDateTime(reader.ReadLine());
                alquiler.KmRecorridos = Convert.ToInt32(reader.ReadLine());
                
                alquileres.Add(alquiler);

            }
            reader.Close();
        }
        private void LeerVehiculos()
        {

            FileStream stream = new FileStream("Vehiculos.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Vehiculo vehiculo = new Vehiculo();
                vehiculo.Placa = reader.ReadLine();
                vehiculo.Marca = reader.ReadLine();
                vehiculo.Modelo = Convert.ToInt32(reader.ReadLine());
                vehiculo.Color = reader.ReadLine();
                vehiculo.Precio = Convert.ToDouble(reader.ReadLine());

                vehiculos.Add(vehiculo);
            }
            reader.Close();
        }

        private void Guardar()
        {

            FileStream stream = new FileStream("Vehiculos.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);

            foreach (var vehiculo in vehiculos)
            {
                writer.WriteLine(vehiculo.Placa);
                writer.WriteLine(vehiculo.Marca);
                writer.WriteLine(vehiculo.Modelo);
                writer.WriteLine(vehiculo.Color);
                writer.WriteLine(vehiculo.Precio);

            }
            writer.Close();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            LeerVehiculos();
            LeerClientes();
            LeerAlquileres();

            MostrarVehiculos();
            MostrarClientes();

            int mayor = alquileres.Max(a => a.KmRecorridos);
            lblMayorKm.Text = mayor.ToString()+" km";

        }

        private void btnAgregarV_Click(object sender, EventArgs e)
        {
            Vehiculo vehiculoRepetido = vehiculos.Find(n => n.Placa == txtPlaca.Text);

            if (vehiculoRepetido == null)
            {
                Vehiculo vehiculo = new Vehiculo();
                vehiculo.Placa = txtPlaca.Text;
                vehiculo.Marca = txtMarca.Text;
                vehiculo.Modelo = int.Parse(txtModelo.Text);
                vehiculo.Color = txtColor.Text;
                vehiculo.Precio = double.Parse(txtPreciokm.Text);


                vehiculos.Add(vehiculo);
            }
            else
                MessageBox.Show("El vehículo con placa "+vehiculoRepetido.Placa + " ya fue registrado");

            Guardar();
            txtPlaca.Text = "";
            txtMarca.Text = "";
            txtModelo.Text = "";
            txtColor.Text = "";
            txtPreciokm.Text = "";

            MostrarVehiculos();

        }
        private void MostrarVehiculos()
        {
            dataGridViewVehiculos.DataSource = null;
            dataGridViewVehiculos.DataSource = vehiculos;
            dataGridViewVehiculos.Refresh();
        }
        private void MostrarClientes()
        {
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = clientes;
            dataGridView2.Refresh();
        }
        private void MostrarReporte()
        {
            dataGridView3.DataSource = null;
            dataGridView3.DataSource = reportes;
            dataGridView3.Refresh();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            Reporte reporte = new Reporte();

            for (int i = 0; i < alquileres.Count; i++)
            {
                for (int j = 0; j < clientes.Count; j++)
                {
                    if (alquileres[i].Nit == clientes[j].Nit)
                    {
                        reporte.Nombre = clientes[j].Nombre;
                    }
                }
                for (int k = 0; k < vehiculos.Count; k++)
                {
                    if (alquileres[i].Placa == vehiculos[k].Placa)
                    {
                        reporte.Placa = vehiculos[k].Placa;
                        reporte.Marca = vehiculos[k].Marca;
                        reporte.FechaDevolucion = alquileres[i].FechaDevolucion;
                        reporte.Total = alquileres[i].KmRecorridos * vehiculos[k].Precio;

                    }
                }
                
            }
            reportes.Add(reporte);
            MostrarReporte();
        }
    }
}
