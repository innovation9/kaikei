using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaikei
{
    class ValidacionesJMP
    {
        public static bool IsComboBoxValue(object value)
        {
            if (value == null)
                return false;
            else
                return true;
        }

        public static bool IsNumerico(string cadena, out int iNumero)
        {
            iNumero = 0;
            if (IsNull(cadena)) return false;

            return (int.TryParse(cadena, out iNumero));
        }

        public static bool IsNumerico(string cadena, out decimal dNumero)
        {
            dNumero = 0;
            if (IsNull(cadena)) return false;

            return (decimal.TryParse(cadena, out dNumero));
        }

        public static bool IsNumericoMayor(string cadena, out int iNumero)
        {
            iNumero = 0;
            if (IsNull(cadena)) return false;

            if (int.TryParse(cadena, out iNumero))
                if (iNumero > 0) return true; else return false;
            else return false;
        }

        public static bool IsNumericoMayor(string cadena, out decimal dNumero)
        {
            dNumero = 0;
            if (IsNull(cadena)) return false;

            if (decimal.TryParse(cadena, out dNumero))
                if (dNumero > 0) return true; else return false;
            else return false;
        }

        public static bool IsNumericoMayor(string cadena, out int iNumero,int hasta)
        {
            iNumero = 0;
            if (IsNull(cadena)) return false;

            if (int.TryParse(cadena, out iNumero))
                if (iNumero > 0 && iNumero <=hasta) return true; else return false;
            else return false;
        }

        public static bool IsNumericoMayor(string cadena, out decimal dNumero, decimal hasta)
        {
            dNumero = 0;
            if (IsNull(cadena)) return false;

            if (decimal.TryParse(cadena, out dNumero))
                if (dNumero > 0 && dNumero <=hasta) return true; else return false;
            else return false;
        }

        public static bool IsNull(string cadena)
        {
            return (string.IsNullOrEmpty(cadena.Trim()));
        }

        public static bool IsSelectedDate(string cadena, out DateTime dtFecha)
        {
            dtFecha = DateTime.Today;
            if (IsNull(cadena)) return false;

            return (DateTime.TryParse(cadena, out dtFecha));
        }
    }
}
