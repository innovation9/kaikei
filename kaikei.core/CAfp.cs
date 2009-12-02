using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kaikei.core
{
    class CAfp : IConeccion
    {
        CConeccion conexion;

        public int Id_AFP { set; private get; }
        public String Nombre { set; private get; }
        public Double Tasa { set; private get; }

        public CAfp()
        {
        }

        public CAfp(CConeccion c,int id)
        {
            this.conexion = c;
            this.Id_AFP = id;
            
        }

        public int sqlInsert()
        {
            try
            {
                return conexion.sqlQuery("INSERT INTO AFPS (NOMBRE,TASA) VALUES ('{0}',{1})",
                    this.Nombre, this.Tasa);
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
                return conexion.sqlQuery("DELETE FROM AFPS WHERE ID_AFP = '{0}'", this.Id_AFP);
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
                return conexion.sqlQuery("UPDATE AFPS SET NOMBRE = '{0}' , TASA = {1} WHERE ID_AFP = '{2}'",
                    this.Nombre, this.Tasa, this.Id_AFP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
