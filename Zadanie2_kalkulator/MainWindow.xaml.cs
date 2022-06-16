using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Zadanie2_kalkulator.Calc;
using Zadanie2_kalkulator.Calc.Models;

namespace Zadanie2_kalkulator
{
    /*
      Projekt na zaliczneie z programowania .NET
      Autor: Damian Piotrowski
      Grupa: ININ3_PR1
      Album: 63172
     */
    public partial class MainWindow : Window
    {
        private Calculator _calculator;

        public MainWindow()
        {
            InitializeComponent();

            var model = new CalculatorModel();   
            _calculator = new Calculator(model);

            DataContext = model;
        }

        private void AddNumber(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            _calculator.AddNumber(Convert.ToChar(button.Content));
        }
        private void AddSign(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            _calculator.AddSign(Convert.ToChar(button.Content));
        }
        private void ClearAll(object sender, RoutedEventArgs e)
        {
            _calculator.CE();
        }
        private void ClearCalculationArea(object sender, RoutedEventArgs e)
        {
            _calculator.C();
        }
        private void DeleteLastAddedNumber(object sender, RoutedEventArgs e)
        {
            _calculator.DeleteLast();
        }
        private void EqualSign(object sender, RoutedEventArgs e)
        {
            _calculator.EqualSign();
        }
        private void Exponentiation(object sender, RoutedEventArgs e)
        {
            _calculator.ExponentiationSign();
        }
        private void SquareSign(object sender, RoutedEventArgs e)
        {
            _calculator.SquareRootSign();
        }
        private void ReciprocalSign(object sender, RoutedEventArgs e)
        {
            _calculator.ReciprocalSign();
        }      
        private void PercentSign(object sender, RoutedEventArgs e)
        {
            _calculator.PercentSign();
        }
        private void FactorialSign(object sender, RoutedEventArgs e)
        {
            _calculator.FactorialSign();
        }
        private void ModuloSign(object sender, RoutedEventArgs e)
        {
            _calculator.ModuloSign();
        }
        private void LogSign(object sender, RoutedEventArgs e)
        {
            _calculator.LogSign();
        }
        private void FloorSign(object sender, RoutedEventArgs e)
        {
            _calculator.FloorSign();
        }
        private void CeilingSign(object sender, RoutedEventArgs e)
        {
            _calculator.CeilingSign();
        }
        private void DotSign(object sender, RoutedEventArgs e)
        {
            _calculator.DotSign();
        }
        private void ChangeSign(object sender, RoutedEventArgs e)
        {
            _calculator.ChangeSign();
        }


    }
}
