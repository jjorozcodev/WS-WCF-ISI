using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCF_ISI_UCA_Demo
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        SqlConnection con = new SqlConnection("Data Source=OROZCODEV\\OROZCODEV; Initial Catalog = WCF_Test; User = sa; Password = 123;");

        public string ValidateLogin(string user, string pwd)
        {
            string mensaje = "";
            List<string> UserPassword = new List<string>();
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM USUARIOS WHERE USERNAME = @user AND PASSWORDUSER = @pass", con);
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@pass", pwd);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if(dt.Rows.Count > 0)
                {
                    mensaje = "Usuario encontrado.";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string name = dt.Rows[i]["Username"].ToString();
                        string pass = dt.Rows[i]["PasswordUser"].ToString();

                        UserPassword.Add(name);
                        UserPassword.Add(pass);
                    }
                }
                else
                {
                    mensaje = "No hubo coincidencias";
                }
                con.Close();
                
            }

            return mensaje;
        }
    }
}
