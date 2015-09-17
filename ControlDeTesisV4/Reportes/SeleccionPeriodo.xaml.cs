using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Models;
using ControlDeTesisV4.Reportes.Proyectos;

namespace ControlDeTesisV4.Reportes
{
    /// <summary>
    /// Interaction logic for SeleccionPeriodo.xaml
    /// </summary>
    public partial class SeleccionPeriodo
    {
        RadioButton selectedRadio = null;
        private readonly int tipoProyecto;

        public SeleccionPeriodo(int tipoProyecto)
        {
            InitializeComponent();
            this.tipoProyecto = tipoProyecto;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            int initYear = 2014;

            int year = DateTime.Now.Year;
            while (initYear >= 2014 && initYear <= (year +1))
            {
                CbxAnio.Items.Add(initYear);
                initYear++;
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

        }

        private void BtnContinuar_Click(object sender, RoutedEventArgs e)
        {
            if (selectedRadio == null)
            {
                MessageBox.Show("Seleccione el periodo del reporte que desea generar");
                return;
            }
            else if (CbxAnio.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el año del cual quiere generar el reporte");
                return;
            }

            int mes = Convert.ToInt16(selectedRadio.Name.Replace("Rad", ""));

            if (mes < 13)
            {
                MainWindow.InicioPeriodo = Convert.ToInt32(CbxAnio.Text + this.GetTwoDigitFormat(mes) + "01");
                MainWindow.FinalPeriodo = Convert.ToInt32(CbxAnio.Text + this.GetTwoDigitFormat(mes) + "32");
            }
            else
            {
                MainWindow.InicioPeriodo = Convert.ToInt32(CbxAnio.Text);
                MainWindow.FinalPeriodo = Convert.ToInt32(CbxAnio.Text);
            }


            DialogResult = true;
            this.Close();
        }

        private void Rad_Checked(object sender, RoutedEventArgs e)
        {
            selectedRadio = sender as RadioButton;
        }

        private string GetTwoDigitFormat(int diaMes)
        {
            if (diaMes < 10)
                return "0" + diaMes;
            else
                return diaMes.ToString();
        }
    }
}
