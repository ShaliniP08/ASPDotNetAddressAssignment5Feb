using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppDropDown5Feb
{
    public partial class AddressDetails : Page
    {
        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            lblDisplay.Visible = false;
            if ((DropDownListCountry.SelectedIndex > 0) && (DropDownListState.SelectedIndex > 0) && (DropDownListCity.SelectedIndex > 0))
            {
                lblDisplay.Visible = true;
                StringBuilder sbs = new StringBuilder();
                sbs.AppendLine("Your address is : ");
                sbs.AppendLine($"{DropDownListCity.SelectedItem},");
                sbs.AppendLine($"{DropDownListState.SelectedItem},");
                sbs.AppendLine($"{DropDownListCountry.SelectedItem}.");
                lblDisplay.Text = sbs.ToString();
            }

            if (!Page.IsPostBack)
            {
                BindingMethodForCountry();
            }
        }
  
        private void BindingMethodForCountry()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[1].ToString()))
                {
                    cn.Open();
                    using (SqlDataAdapter adp = new SqlDataAdapter("Select * from TblCountry", cn))
                    {
                        DataSet ds = new DataSet();
                        adp.Fill(ds);

                        DropDownListCountry.DataSource = ds;
                        DropDownListCountry.DataValueField = "CountryId";
                        DropDownListCountry.DataTextField = "CountryName";
                        DropDownListCountry.DataBind();

                        DropDownListCountry.Items.Insert(0, new ListItem("-----------Select Country-----------", "0"));
                        DropDownListState.Items.Insert(0, new ListItem("------------Select State------------", "0"));
                        DropDownListCity.Items.Insert(0, new ListItem("-------------Select City-------------", "0"));

                        if (DropDownListCountry.SelectedValue == "0")  //If Country is not selected then clear State DropDownList
                        {
                            DropDownListState.Items.Clear();
                            DropDownListState.Items.Insert(0, new ListItem("------------Select State------------", "0")); //index, listItem item(text,value)                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        
        protected void DropDownListCountry_SelectedIndexChanged(object sender, EventArgs e) //Select all States corresponding to the selected Country
        {
            try
            {
                int CountryId = Convert.ToInt32(DropDownListCountry.SelectedValue);

                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[1].ToString()))
                {
                    cn.Open();
                    using (SqlDataAdapter adp = new SqlDataAdapter("Select * from TblState where CountryId="+CountryId, cn))
                    {
                        DataSet ds = new DataSet();
                        adp.Fill(ds);

                        DropDownListState.DataSource = ds;
                        DropDownListState.DataValueField = "CountryId";
                        DropDownListState.DataValueField = "StateId";
                        DropDownListState.DataTextField = "StateName";
                        DropDownListState.DataBind();

                        DropDownListState.Items.Insert(0, new ListItem("------------Select State------------", "0"));

                        if (DropDownListState.SelectedValue == "0")  //If State is not selected then clear City DropDownList and hide the label
                        {
                            lblDisplay.Visible = false;
                            DropDownListCity.Items.Clear();
                            DropDownListCity.Items.Insert(0, new ListItem("-------------Select City-------------", "0"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void DropDownListState_SelectedIndexChanged(object sender, EventArgs e) //Select all Cities corresponding to the selected State
        {
            try
            {
                int StateId = Convert.ToInt32(DropDownListState.SelectedValue);

                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[1].ToString()))
                {
                    cn.Open();
                    using (SqlDataAdapter adp = new SqlDataAdapter("Select * from TblCity where StateId=" + StateId, cn))
                    {

                        DataSet ds = new DataSet();
                        adp.Fill(ds);

                        DropDownListCity.DataSource = ds;
                        DropDownListState.DataValueField = "StateId";
                        DropDownListCity.DataValueField = "CityId";
                        DropDownListCity.DataTextField = "CityName";
                        DropDownListCity.DataBind();

                        DropDownListCity.Items.Insert(0, new ListItem("-------------Select City-------------", "0"));

                        if (DropDownListCity.SelectedValue == "0")  //If City is not selected then hide the label
                        {
                            lblDisplay.Visible = false;
                            DropDownListCity.Items.Insert(0, new ListItem("-------------Select City-------------", "0"));
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            
        }
        #endregion
    }
}