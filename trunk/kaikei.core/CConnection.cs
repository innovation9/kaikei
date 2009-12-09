using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace kaikei.core
{
    public class CConeccion
    {
        public enum TypeConeccion
        {
            WINDOW,
            SQLSERVER,
            APP
        }

        public String User {set; private get;}
        public String Password { set; private get; }
        public String Server { set; private get; }
        public String Database { set; private get; }
        String ConnecctionString { set; get; }
        public TypeConeccion TipoConeccion { set; private get; }

        SqlConnection conex;
        SqlCommand comando;
        SqlDataReader resultado;

        //Constructor
        public CConeccion()
        {
            //vacio
        }

        /*
         * Coneccion con un SQL Server.
         */
        public CConeccion(String user_id, String password, String server, String database)
        {
            this.TipoConeccion = TypeConeccion.SQLSERVER;
            this.User = user_id;
            this.Password = password;
            this.Server = server;
            this.Database = database;

            this.Conectar();

        }

        /*
         * Constructor a partir del Servidor, el nombre de
         * la base de datos.
         */
        public CConeccion(String server, String database)
        {
            this.TipoConeccion = TypeConeccion.WINDOW;
            this.Server = server;
            this.Database = database;

            this.Conectar();
        }

        public CConeccion(String sc)
        {
            this.TipoConeccion = TypeConeccion.APP;
            this.ConnecctionString = sc;
            this.Conectar();
        }

        /*
         * Regresa el estado actual de la coneccion.
         */
        public String Estado()
        {
            return this.conex.State.ToString();
        }

        /*
         * Conecta a una base de datos.
         */
        public void Conectar()
        {
            try
            {
                if (this.TipoConeccion == TypeConeccion.SQLSERVER)
                {
                    conex = new SqlConnection("user id=" + this.User + ";password=" + this.Password +
                    ";server=" + this.Server + ";database=" + this.Database + ";connection timeout=10" +
                    "MultipleActiveResultSets=True");
                }
                else if (this.TipoConeccion == TypeConeccion.WINDOW)
                {
                    conex = new SqlConnection("server=" + this.Server + ";Trusted_Connection=yes;database=" +
                    this.Database + ";connection timeout=10;MultipleActiveResultSets=True");
                }
                else if (this.TipoConeccion == TypeConeccion.APP)
                {
                    conex = new SqlConnection(this.ConnecctionString);
                }

                conex.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*
         * Conprueba si una coneccion esta funcionando.
         */
        public bool isActive()
        {
            try
            {
                return (conex.State.ToString() == "Open");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*
         * Ejecuta comandos SQL que no debuelven valores.
         * Regresa: (int) Las cantidades que se han modificado.
         */
        public int sqlQuery(String sql)
        {
            int r;

            if (sql.Length == 0 || sql == null)
            {
                return -1;
            }
            try
            {
                comando = new SqlCommand(sql, this.conex);
                r = comando.ExecuteNonQuery();
                comando.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return r;
        }

#if true
        //da problemas en compilacion xq parametros
        /*
         * Ejecuta comandos SQL que no debuelven valores.
         * Regresa: (int) Las cantidades que se han modificado.
         */
        public int sqlQuery(String sqlF, params Object[] args)
        {
            int r;

            if (sqlF.Length == 0 || sqlF == null)
            {
                return -1;
            }
            try
            {
                comando = new SqlCommand(String.Format(sqlF, args), this.conex);
                r = comando.ExecuteNonQuery();
                comando.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return r;
        }
#endif

        /*
         * Debuelve el resultado puro de una consulta.
         */
        public SqlDataReader sqlSelect(String sql)
        {
            if (sql.Length == 0 || sql == null)
            {
                return null;
            }
            try
            {
                resultado = null;
                comando = new SqlCommand(sql, this.conex);
                comando.ExecuteNonQuery();
                resultado = comando.ExecuteReader();
                comando.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }

        /*
         * Ejecuta una consulta que devuelve un campo
         * String correspondiente al id del campo.
         */
        public string sqlSelect(String sql, int campo)
        {
            if (campo < 0)
            {
                return null;
            }
            try
            {
                resultado = this.sqlSelect(sql);
                while (resultado.Read())
                {
                    return resultado[campo].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        /**
         * Ejecuta una consulta que devueleve un campo
         * String correspondientes al nombre del campo.
         */
        public string sqlSelect(String sql, String campo)
        {
            if (campo.Length <= 0)
            {
                return null;
            }
            try
            {
                resultado = this.sqlSelect(sql);
                while (resultado.Read())
                {
                    return resultado[campo].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        /**
         * cierra la coneccion actual
         */
        public void Cerrar()
        {
            try
            {
                this.conex.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
