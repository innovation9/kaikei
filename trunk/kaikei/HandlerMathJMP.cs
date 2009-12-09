using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ControlsWPF_JMP.Controls;

namespace Kaikei
{
    public class HandlerMathJMP
    {
        public static void NumeroDecimalMath(object sender, RoutedEventArgs e)
        {
            TextBoxJMP txtTMP = sender as TextBoxJMP;
            if (txtTMP.Text.Length > 0 && txtTMP.Text.Substring(0, 1) == "=")
            {
                string result = MathEvalJMP.StringEval(txtTMP.Text.Substring(1));

                //Ahora tenemos q redondear el resultado a 2 cifras
                decimal dRes = decimal.Parse(result);
                txtTMP.Text = Math.Round(dRes, 2).ToString();
            }
        }

        public static void NumeroEnteroMath(object sender, RoutedEventArgs e)
        {
            TextBoxJMP txtTMP = sender as TextBoxJMP;
            if (txtTMP.Text.Length > 0 && txtTMP.Text.Substring(0, 1) == "=")
            {
                string result = MathEvalJMP.StringEval(txtTMP.Text.Substring(1));

                //Ahora tenemos q redondear el resultado a 2 cifras
                decimal dRes = decimal.Parse(result);
                txtTMP.Text = Math.Round(dRes, 0).ToString();
            }
        }

        public static void TextSelect(object sender, RoutedEventArgs e)
        {
            ((TextBoxJMP)sender).SelectAll();
        }
    }
}
