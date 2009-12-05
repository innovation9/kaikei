using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace kaikei.core
{
    class CEmpleado : IConeccion
    {
        CConeccion conex;
        public int Id_empleado { set; private get; }
        public CAfp AFP { set; private get; }
        public String Nombres { set; private get; }
        public String Apellidos { set; private get; }
        public String Direccion { set; private get; }
        public int DUI { set; private get; }
        public int NIT { set; private get; }
        public int ISSS { set; private get; }
        public int NUP { set; private get; }
        public int TelefonoFijo { set; private get; }
        public int TelefonoMovil { set; private get; }
        public String Email { set; private get; }
        public Double Salario { set; private get; }

        public CEmpleado() { }
        public CEmpleado(CConeccion c, int id)
        {
            this.conex = c;
            this.Id_empleado = id;
            this.sqlGetById();
        }

        static public String getDUI(int DUI)
        {
            String d = DUI.ToString();
            return String.Format("{0:8}-{1}", d.Substring(0,8),d.Substring(8));
        }

        static public String getNIT(int NIT)
        {
            String nit = NIT.ToString();
            return String.Format("{0:4}-{1:6}-{2:3}-{3:1}",
                nit.Substring(0, 3), nit.Substring(4, 9), nit.Substring(10, 12), nit.Substring(13));
        }

        static public String formatoTelefono(int numero)
        {
            String t = numero.ToString();
            return String.Format("{0:4}-{1:4}", t.Substring(0, 3), t.Substring(4, 7));
        }

        public void sqlGetById()
        {
            try
            {
                SqlDataReader r = conex.sqlSelect(
                    String.Format("SELECT * FROM EMPLEADOS WHERE ID_EMPLEADO = '{0}'", this.Id_empleado));
                this.AFP = new CAfp(conex, Int32.Parse(r["ID_AFP"].ToString()));
                this.Nombres = r["NOMBRES"].ToString();
                this.Apellidos = r["APELLIDOS"].ToString();
                this.Direccion = r["DIRECCION"].ToString();
                this.DUI = Int32.Parse(r["DUI"].ToString());
                this.NIT = Int32.Parse(r["NIT"].ToString());
                this.ISSS = Int32.Parse(r["ISSS"].ToString());
                this.NUP = Int32.Parse(r["NUP"].ToString());
                this.TelefonoFijo = Int32.Parse(r["TELEFONOFIJO"].ToString());
                this.TelefonoFijo = Int32.Parse(r["TELEFONOMOVIL"].ToString());
                this.Email = r["EMAIL"].ToString();
                this.Salario = Double.Parse(r["SALARIONOMINAL"].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
        public int sqlInsert()
        {
            try
            {
                return conex.sqlQuery(
                    "INSERT INTO EMPLEADOS (ID_AFP,NOMBRES,APELLIDOS,DIRECCION,DUI,NIT,ISSS," +
                    "NUP,TELEFONOFIJO,TELEFONOMOVIL,EMAIL,SALARIONOMINAL) " +
                    "VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11})", this.AFP.Id_AFP, this.Nombres,
                    this.Apellidos, this.Direccion, this.DUI.ToString(), this.NIT.ToString(), this.ISSS.ToString(),
                    this.NUP.ToString(), this.TelefonoFijo.ToString(), this.TelefonoMovil.ToString(), this.Email,
                    this.Salario.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int sqlDelete()
        {
            try
            {
                return conex.sqlQuery("DELETE FROM EMPLEADOS WHERE ID_EMPLEADO = '{0}'", this.Id_empleado);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int sqlUpdate()
        {
            try
            {
                return conex.sqlQuery("UPDATE FROM EMPLEADOS SET ID_AFP = '{1}',NOMBRES = '{2}'," +
                        "APELLIDOS = '{3}',DIRECCION = '{4}',DUI = '{5}',NIT = '{6}',ISSS = '{7}',NUP = '{8}'," +
                        "TELEFONOFIJO = '{9}',TELEFONOMOVIL = '{10}',EMAIL = '{11}',SALARIONOMINAL = '{12}' " +
                        "WHERE ID_EMPLEADO = '{0}'", this.Id_empleado, this.AFP.Id_AFP, this.Nombres,
                        this.Apellidos, this.Direccion, this.DUI.ToString(), this.NIT.ToString(), this.ISSS.ToString(),
                        this.NUP.ToString(), this.TelefonoFijo.ToString(), this.TelefonoMovil.ToString(), this.Email,
                        this.Salario.ToString());
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}             
