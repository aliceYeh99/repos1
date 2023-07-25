using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViewModelExam.App_Code;
using ViewModelExam.DAL;
namespace ViewModelExam
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string msg = GetData().Remark;
            Label1.Text = "Remark =" + msg;
        }

        private ViewModel GetData()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TestDBConnectionString"].ToString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

            
                string commandText = @"select 'VS001' HEADER_ID, 'BB' Remark, '10211002' CREATEUSER, '1688' UUID, '10211002' APPLIER 
union all
select 'VS002' HEADER_ID, 'BBbb' Remark, '10211002' CREATEUSER, '1688' UUID, '10211002' APPLIER --from dual";
                SqlCommand command = new SqlCommand(commandText, connection);
                //command.Parameters.Add("@ID", SqlDbType.Int);
                //command.Parameters["@ID"].Value = customerID;
                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    DataTable dt = new DataTable();
                    dt.BeginLoadData();
                    da.Fill(dt);
                    dt.EndLoadData();
                    //return dt.Rows[0]["REMARK"].ToString();
                    List<ViewModel> list = new List<ViewModel>(dt.ToList<ViewModel>());
                    list.Where(o => !string.IsNullOrEmpty(o.HEADER_ID)).ToList();
                    //list.ForEach(o => { if (!"".Equals(o.CREATEUSER)) { o.USERNAME = GetUserName(o.CREATEUSER); } });
                    return list.FirstOrDefault();
                    //return null;
                }
            }
        }
    }
}