using System;
using System.Linq;
using Zadanie2_kalkulator.Calc.Models;

namespace Zadanie2_kalkulator.Calc
{
    public class Calculator
    {
        private CalculatorModel _model;

        private bool WasCalculated = false;
        public Calculator(CalculatorModel model)
        {
            _model = model;
        }

        public void AddNumber(char number)
        {
            if(WasCalculated == true)
            {
                CE();
                WasCalculated = false;
            }
            if (_model.CalculationArea == "0")
            {
                _model.CalculationArea = number.ToString();
            }
            else
            {
                _model.CalculationArea += number;
            }
        }
        public void CE()
        {
            _model.CalculationArea = "0";
            _model.LastCalculationEvent = "";
        }
        public void C()
        {
            _model.CalculationArea = "0";
        }
        public void DeleteLast()
        {
            if (_model.CalculationArea.Length > 1)
            {
                _model.CalculationArea = _model.CalculationArea.Substring(0, _model.CalculationArea.Length - 1);
            }
        }
        public void AddSign(char sign)
        {
            if((sign == '+' || sign == '-' || sign == '×' || sign == '÷') && !_model.LastCalculation.Contains("="))
            {
                if (!_model.LastCalculation.Contains("+")
                    && !_model.LastCalculation.Contains("-")
                    && !_model.LastCalculation.Contains("×")
                    && !_model.LastCalculation.Contains("÷")
                    && !_model.LastCalculation.Contains("MOD"))
                {
                    if (!(_model.LastCalculation.LastOrDefault() == sign))
                    {
                        switch (sign)
                        {
                            case '+':
                                _model.LastCalculationEvent += _model.CurrentValue + " + ";
                                break;
                            case '-':
                                _model.LastCalculationEvent += _model.CurrentValue + " - ";
                                break;
                            case '×':
                                _model.LastCalculationEvent += _model.CurrentValue + " × ";
                                break;
                            case '÷':
                                _model.LastCalculationEvent += _model.CurrentValue + " ÷ ";
                                break;
                        }
                    }
                }
                else
                {
                    switch (sign)
                    {
                        case '+':
                            _model.LastCalculationEvent = _model.LastCalculation.Substring(0, _model.LastCalculation.Length-3) + " + ";
                            break;
                        case '-':
                            _model.LastCalculationEvent = _model.LastCalculation.Substring(0, _model.LastCalculation.Length - 3) + " - ";
                            break;
                        case '×':
                            _model.LastCalculationEvent = _model.LastCalculation.Substring(0, _model.LastCalculation.Length - 3) + " × ";
                            break;
                        case '÷':
                            _model.LastCalculationEvent = _model.LastCalculation.Substring(0, _model.LastCalculation.Length - 3) + " ÷ ";
                            break;
                    }
                }

                _model.CalculationArea = "0";

            }
        }
        public void EqualSign()
        {
            if(!_model.LastCalculation.Contains('='))
            {
                if (_model.LastCalculation.Contains('+'))
                {
                    string valueA = _model.LastCalculation.Substring(0, _model.LastCalculation.IndexOf(" "));
                    var result = Addition(double.Parse(valueA), double.Parse(_model.CurrentValue));
                    _model.LastCalculationEvent += _model.CurrentValue.ToString() + " = ";
                    _model.CalculationArea = result.ToString();
                    WasCalculated = true;
                }
                if (_model.LastCalculation.Contains('-') && _model.CurrentValue[0] != '-')
                {
                    string valueA = _model.LastCalculation.Substring(0, _model.LastCalculation.IndexOf(" "));
                    var result = Subtraction(double.Parse(valueA), double.Parse(_model.CurrentValue));
                    _model.LastCalculationEvent += _model.CurrentValue.ToString() + " = ";
                    _model.CalculationArea = result.ToString();
                    WasCalculated = true;
                }
                if (_model.LastCalculation.Contains('×'))
                {
                    string valueA = _model.LastCalculation.Substring(0, _model.LastCalculation.IndexOf(" "));
                    var result = Multiplication(double.Parse(valueA), double.Parse(_model.CurrentValue));
                    _model.LastCalculationEvent += _model.CurrentValue.ToString() + " = ";
                    _model.CalculationArea = result.ToString();
                    WasCalculated = true;
                }
                if (_model.LastCalculation.Contains("%"))
                {
                    string valueA = _model.LastCalculation.Substring(0, _model.LastCalculation.IndexOf(" "));
                    var result = Modulo(double.Parse(valueA), double.Parse(_model.CurrentValue));
                    _model.LastCalculationEvent += _model.CurrentValue.ToString() + " = ";
                    _model.CalculationArea = result.ToString();
                    WasCalculated = true;
                }
                if (_model.LastCalculation.Contains('÷'))
                {
                    string valueA = _model.LastCalculation.Substring(0, _model.LastCalculation.IndexOf(" "));
                    var result = Division(double.Parse(valueA), double.Parse(_model.CurrentValue));
                    if (result == null)
                    {
                        _model.LastCalculationEvent += _model.CurrentValue.ToString() + " = ";
                        _model.CalculationArea = "Błąd";
                    }
                    else
                    {
                        _model.LastCalculationEvent += _model.CurrentValue.ToString() + " = ";
                        _model.CalculationArea = result.ToString();
                    }

                    WasCalculated = true;
                }
            }  
        }

        public void ExponentiationSign()
        {
            if (!_model.LastCalculation.Contains('='))
            {
                var result = Exponentiation(double.Parse(_model.CurrentValue));
                _model.LastCalculationEvent += _model.CurrentValue.ToString() + "² = ";
                _model.CalculationArea = result.ToString();
                WasCalculated = true;
            }
        }       
        public void SquareRootSign()
        {
            if (!_model.LastCalculation.Contains('='))
            {
                var result = SquareRoot(double.Parse(_model.CurrentValue));
                _model.LastCalculationEvent += "√" + _model.CurrentValue.ToString() + " = ";
                if(result < 0)
                {
                    _model.CalculationArea = "Błąd";
                }
                else
                {
                    _model.CalculationArea = result.ToString();
                }
                WasCalculated = true;
            }
        }
        public void ReciprocalSign()
        {
            if (!_model.LastCalculation.Contains('='))
            {
                var result = Reciprocal(double.Parse(_model.CurrentValue));
                _model.LastCalculationEvent += "1/" + _model.CurrentValue.ToString() + " = ";
                _model.CalculationArea = result.ToString();
                WasCalculated = true;
            }
        }       
        public void PercentSign()
        {
            if ((_model.LastCalculation.Contains("+")
                    || _model.LastCalculation.Contains("-")
                    || _model.LastCalculation.Contains("×")
                    || _model.LastCalculation.Contains("÷")) && _model.CurrentValue[0] != '-' && _model.LastCalculation[0] != '-')
            {
                if (!_model.LastCalculation.Contains('='))
                {
                    var result = Percent(double.Parse(_model.CurrentValue));
                    _model.CurrentValue = result.ToString();
                    EqualSign();
                }
            }
        }
        public void FactorialSign()
        {
            if (!_model.LastCalculation.Contains('='))
            {
                var result = Factorial(double.Parse(_model.CurrentValue));
                _model.LastCalculationEvent += _model.CurrentValue.ToString() + "! = ";
                _model.CalculationArea = result.ToString();
                WasCalculated = true;
            }
        }

        public void ModuloSign()
        {
            if (!_model.LastCalculation.Contains("="))
            {
                if (!_model.LastCalculation.Contains("+")
                    && !_model.LastCalculation.Contains("-")
                    && !_model.LastCalculation.Contains("×")
                    && !_model.LastCalculation.Contains("÷")
                    && !_model.LastCalculation.Contains("%"))
                {
                    if (!(_model.LastCalculation.LastOrDefault() == '%'))
                    {
                        _model.LastCalculationEvent += _model.CurrentValue + " % ";
                    }
                }
                else
                {
                    _model.LastCalculationEvent = _model.LastCalculation.Substring(0, _model.LastCalculation.Length - 3) + " % ";
                }

                _model.CalculationArea = "0";

            }
        }
        public void LogSign()
        {
            if (!_model.LastCalculation.Contains('='))
            {
                var result = Log(double.Parse(_model.CurrentValue));
                _model.LastCalculationEvent += "LOG " + _model.CurrentValue.ToString() + " = ";
                _model.CalculationArea = result.ToString();
                WasCalculated = true;
            }
        }
        public void FloorSign()
        {
            if (!_model.LastCalculation.Contains('='))
            {
                var result = Floor(double.Parse(_model.CurrentValue));
                _model.LastCalculationEvent += _model.CurrentValue.ToString() + " = ";
                _model.CalculationArea = result.ToString();
                WasCalculated = true;
            }
        }
        public void CeilingSign()
        {
            if (!_model.LastCalculation.Contains('='))
            {
                var result = Ceiling(double.Parse(_model.CurrentValue));
                _model.LastCalculationEvent += _model.CurrentValue.ToString() + " = ";
                _model.CalculationArea = result.ToString();
                WasCalculated = true;
            }
        }

        public void DotSign()
        {
            if (!_model.CurrentValue.Contains(','))
            {
                _model.CalculationArea += ",";
            }
        }
        public void ChangeSign()
        {
            if (_model.CalculationArea[0] == '-')
            {
                if (_model.CalculationArea != "0")
                {
                    _model.CalculationArea = _model.CurrentValue.Substring(1);
                }
            }
            else
            {
                if (_model.CalculationArea != "0")
                {
                    _model.CalculationArea = "-" + _model.CurrentValue;
                }
            }
        }
        private double Addition(double a, double b)
        {
            return (a + b);
        }
        private double Subtraction(double a, double b)
        {
            return (a - b);
        }
        private double Multiplication(double a, double b)
        {
            return (a * b);
        }
        private double? Division(double a, double b)
        {
            if (b == 0) return null;
            return (a / b);
        }
        private double Exponentiation(double a)
        {
            return a*a;
        }
        private double Modulo(double a, double b)
        {
            return Math.Round(a) % Math.Round(b);
        }
        private double Percent(double a)
        {

            return a/100;
        }
        private double SquareRoot(double a)
        {
            if(a>=0)
            {
                return Math.Sqrt(a);
            }
            return -1;
        }
        private double Reciprocal(double a)
        {
            return 1 / a;
        }
        private double Factorial(double a)
        {
            if ((int)a < 2) return 1;
            return (int)a * Factorial((int)a - 1);
        }
        private double Log(double a)
        {
            return Math.Log10(a);
        }
        private double Floor(double a)
        {
            return Math.Floor(a);
        }
        private double Ceiling(double a)
        {
            return Math.Ceiling(a);
        }


    }
}
