using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace OrderToVenders
{
    public partial class About : System.Web.UI.Page
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        OleDbConnection conn = new OleDbConnection(connectionString);
        string location;
        string BhatMaetalCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            location = Request.QueryString["loc"].ToString();

            if (location == "1")
            {
                locationTxtBox.Text = "AWS-1";
            }

            if (location == "2")
            {
                locationTxtBox.Text = "AWS-2";
            }

            BhatMaetalCode = "22";

            confrimationMsgLbl.Visible = false;
            if (!Page.IsPostBack)
            {
                getOrderNumber();
                SetInitialRow();
                GridViewRow row = GridView1.Rows[GridView1.Rows.Count - 1];
                TextBox uIdTextBox = (TextBox)row.FindControl("uidTxtBox");
                DropDownList vendorNameDDL = (DropDownList)row.FindControl("vendorNameDDL");
                uIdTextBox.Focus();

                vendorNameDDL.DataSource = bindVendorNameDDL();
                vendorNameDDL.DataValueField = "CodPro";
                vendorNameDDL.DataTextField = "NomPro";
                vendorNameDDL.DataBind();
                //vendorNameDDL.Items.Insert(0, new ListItem("--Select Vendor Name--"));
                dateTxtBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");               
            }
        }
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("UID", typeof(string)));
            dt.Columns.Add(new DataColumn("Step", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            dt.Columns.Add(new DataColumn("Pie", typeof(string)));
            dt.Columns.Add(new DataColumn("Grade", typeof(string)));
            dt.Columns.Add(new DataColumn("Hardness", typeof(string)));
            dt.Columns.Add(new DataColumn("Qty", typeof(string)));
            dt.Columns.Add(new DataColumn("Price", typeof(string)));
            dt.Columns.Add(new DataColumn("DeliveryDate", typeof(string)));
            dt.Columns.Add(new DataColumn("VendorName", typeof(string)));

            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["UID"] = string.Empty;
            dr["Step"] = string.Empty;
            dr["Description"] = string.Empty;
            dr["Pie"] = string.Empty;
            dr["Grade"] = string.Empty;
            dr["Hardness"] = string.Empty;
            dr["Qty"] = string.Empty;
            dr["Price"] = string.Empty;
            dr["DeliveryDate"] = string.Empty;
            dr["VendorName"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        private void AddNewRowToGrid()
        {
            bool vendorNameSelected=false;
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox uidTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[1].FindControl("uidTxtBox");
                        TextBox stepTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[2].FindControl("stepTxtBox");
                        TextBox descriptionTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[3].FindControl("descriptionTxtBox");
                        TextBox pieTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("pieTxtBox");
                        TextBox gradeTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[5].FindControl("gradeTxtBox");
                        TextBox hardnessTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[6].FindControl("hardnessTxtBox");
                        TextBox qtyTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[7].FindControl("qtyTxtBox");
                        TextBox priceTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[8].FindControl("priceTxtBox");
                        TextBox deliveryDateTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[9].FindControl("deliveryDateTxtBox");
                        DropDownList vendorNameDDL = (DropDownList)GridView1.Rows[rowIndex].Cells[10].FindControl("vendorNameDDL");
                        Label errormsglbl = (Label)GridView1.Rows[rowIndex].Cells[10].FindControl("errormsglbl");

                        if (vendorNameDDL.SelectedItem.Text != "--Select Vendor Name--")
                        {
                            drCurrentRow = dtCurrentTable.NewRow();
                            drCurrentRow["RowNumber"] = i + 1;
                            dtCurrentTable.Rows[i - 1]["UID"] = uidTxtBox.Text;
                            dtCurrentTable.Rows[i - 1]["Step"] = stepTxtBox.Text;
                            dtCurrentTable.Rows[i - 1]["Description"] = descriptionTxtBox.Text;
                            dtCurrentTable.Rows[i - 1]["Pie"] = pieTxtBox.Text;
                            dtCurrentTable.Rows[i - 1]["Grade"] = gradeTxtBox.Text;
                            dtCurrentTable.Rows[i - 1]["Hardness"] = hardnessTxtBox.Text;
                            dtCurrentTable.Rows[i - 1]["Qty"] = qtyTxtBox.Text;
                            dtCurrentTable.Rows[i - 1]["Price"] = priceTxtBox.Text;
                            dtCurrentTable.Rows[i - 1]["DeliveryDate"] = deliveryDateTxtBox.Text;
                            dtCurrentTable.Rows[i - 1]["VendorName"] = vendorNameDDL.SelectedItem.Text;

                            vendorNameDDL.DataSource = bindVendorNameDDL();
                            vendorNameDDL.DataValueField = "CodPro";
                            vendorNameDDL.DataTextField = "NomPro";
                            vendorNameDDL.DataBind();
                            //vendorNameDDL.Items.Insert(0, new ListItem("--Select Vendor Name--"));

                            rowIndex++;

                            vendorNameSelected = true;
                        }
                        else
                        {
                            vendorNameSelected = false;
                            vendorNameDDL.BackColor = Color.OrangeRed;
                            errormsglbl.Visible = true;
                            errormsglbl.Text = "Please Select Vendor Name...";
                            errormsglbl.ForeColor = Color.OrangeRed;
                            break;
                        }
                    }

                    if (vendorNameSelected)
                    {
                        dtCurrentTable.Rows.Add(drCurrentRow);
                        ViewState["CurrentTable"] = dtCurrentTable;
                        GridView1.DataSource = dtCurrentTable;
                        GridView1.DataBind();

                        SetPreviousData();
                    }
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox uidTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[1].FindControl("uidTxtBox");
                        TextBox stepTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[2].FindControl("stepTxtBox");
                        TextBox descriptionTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[3].FindControl("descriptionTxtBox");
                        TextBox pieTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("pieTxtBox");
                        TextBox gradeTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[5].FindControl("gradeTxtBox");
                        TextBox hardnessTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[6].FindControl("hardnessTxtBox");
                        TextBox qtyTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[7].FindControl("qtyTxtBox");
                        TextBox priceTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[8].FindControl("priceTxtBox");
                        TextBox deliveryDateTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[9].FindControl("deliveryDateTxtBox");
                        DropDownList vendorNameDDL = (DropDownList)GridView1.Rows[rowIndex].Cells[10].FindControl("vendorNameDDL");

                        vendorNameDDL.DataSource = bindVendorNameDDL();
                        vendorNameDDL.DataValueField = "CodPro";
                        vendorNameDDL.DataTextField = "NomPro";
                        vendorNameDDL.DataBind();
                        //vendorNameDDL.Items.Insert(0, new ListItem("--Select Vendor Name--"));

                        uidTxtBox.Text = dt.Rows[i]["UID"].ToString();
                        stepTxtBox.Text = dt.Rows[i]["Step"].ToString();
                        descriptionTxtBox.Text = dt.Rows[i]["Description"].ToString();
                        pieTxtBox.Text = dt.Rows[i]["Pie"].ToString();
                        gradeTxtBox.Text = dt.Rows[i]["Grade"].ToString();
                        hardnessTxtBox.Text = dt.Rows[i]["Hardness"].ToString();
                        qtyTxtBox.Text = dt.Rows[i]["Qty"].ToString();
                        priceTxtBox.Text = dt.Rows[i]["Price"].ToString();
                        deliveryDateTxtBox.Text = dt.Rows[i]["DeliveryDate"].ToString();
                        vendorNameDDL.SelectedItem.Text = dt.Rows[i]["VendorName"].ToString();

                        if (dt.Rows[i]["VendorName"].ToString() == "")
                        {
                            vendorNameDDL.SelectedItem.Text = "BHAT METALS";
                        }
                        else
                        {
                            vendorNameDDL.SelectedItem.Text = dt.Rows[i]["VendorName"].ToString();
                        }
                        rowIndex++;
                    }
                }
            }
        }
        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
            GridViewRow row = GridView1.Rows[GridView1.Rows.Count - 1];
            TextBox uIdTextBox = (TextBox)row.FindControl("uidTxtBox");
            uIdTextBox.Focus();
        }

        protected void removeBtn_Click(object sender, EventArgs e)
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox uidTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[1].FindControl("uidTxtBox");
                        TextBox stepTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[2].FindControl("stepTxtBox");
                        TextBox descriptionTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[3].FindControl("descriptionTxtBox");
                        TextBox pieTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("pieTxtBox");
                        TextBox gradeTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[5].FindControl("gradeTxtBox");
                        TextBox hardnessTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[6].FindControl("hardnessTxtBox");
                        TextBox qtyTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[7].FindControl("qtyTxtBox");
                        TextBox priceTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[8].FindControl("priceTxtBox");
                        TextBox deliveryDateTxtBox = (TextBox)GridView1.Rows[rowIndex].Cells[9].FindControl("deliveryDateTxtBox");
                        DropDownList vendorNameDDL = (DropDownList)GridView1.Rows[rowIndex].Cells[10].FindControl("vendorNameDDL");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["UID"] = uidTxtBox.Text;
                        dtCurrentTable.Rows[i - 1]["Step"] = stepTxtBox.Text;
                        dtCurrentTable.Rows[i - 1]["Description"] = descriptionTxtBox.Text;
                        dtCurrentTable.Rows[i - 1]["Pie"] = pieTxtBox.Text;
                        dtCurrentTable.Rows[i - 1]["Grade"] = gradeTxtBox.Text;
                        dtCurrentTable.Rows[i - 1]["Hardness"] = hardnessTxtBox.Text;
                        dtCurrentTable.Rows[i - 1]["Qty"] = qtyTxtBox.Text;
                        dtCurrentTable.Rows[i - 1]["Price"] = priceTxtBox.Text;
                        dtCurrentTable.Rows[i - 1]["DeliveryDate"] = deliveryDateTxtBox.Text;
                        dtCurrentTable.Rows[i - 1]["VendorName"] = vendorNameDDL.SelectedItem.Text;

                        rowIndex++;
                    }

                    ViewState["CurrentTable"] = dtCurrentTable;
                    GridView1.DataSource = dtCurrentTable;
                    GridView1.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            Button lb = (Button)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                dt.Rows.Remove(dt.Rows[rowID]);

                ViewState["CurrentTable"] = dt;

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            SetPreviousData();
        }

        public DataTable bindVendorNameDDL()
        {
            DataTable VendorNameData = new DataTable();
            try
            {
                //conn.Open();
                //OleDbCommand cmd = new OleDbCommand("Select CodPro, NomPro from Proveedores order by CodPro asc", conn);
                //OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                //adapter.Fill(VendorNameData);
                //conn.Close();

                VendorNameData.Columns.Add("CodPro");
                VendorNameData.Columns.Add("NomPro");
                VendorNameData.Rows.Add("22", "BHAT METAL");
            }
            catch (Exception ex)
            {
                confrimationMsgLbl.ForeColor = Color.OrangeRed;
                confrimationMsgLbl.Visible = true;
                confrimationMsgLbl.Text = "Error occured while binding Vendor name in DropDownList..  "+ ex.Message;
            }
            return VendorNameData;
        }


        public void getOrderNumber() 
        {
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT Top 1 NumPed FROM [Pedidos a proveedor (cabeceras)] Order By NumPed DESC", conn);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            { 
                while(dr.Read())
                {
                    orderNumTxtBox.Text = dr["NumPed"].ToString();
                }
            }
            conn.Close();
        }
        
        protected void uidTextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox uidTextBox = sender as TextBox;
                GridViewRow rows = uidTextBox.NamingContainer as GridViewRow;
                int rowIndex = rows.RowIndex;

                GridViewRow row = GridView1.Rows[rowIndex];
                TextBox stepTextBox = (TextBox)row.FindControl("stepTxtBox");
                TextBox descriptionTextBox = (TextBox)row.FindControl("descriptionTxtBox");
                TextBox elementTextBox = (TextBox)row.FindControl("pieTxtBox");
                TextBox gradeTextBox = (TextBox)row.FindControl("gradeTxtBox");
                TextBox hardnessTextBox = (TextBox)row.FindControl("hardnessTxtBox");
                TextBox qtyTextBox = (TextBox)row.FindControl("qtyTxtBox");
                TextBox priceToOrderTextBox = (TextBox)row.FindControl("priceTxtBox");
                TextBox deliveryDateTextBox = (TextBox)row.FindControl("deliveryDateTxtBox");
                DropDownList vendorNameDLL = (DropDownList)row.FindControl("vendorNameDDL");

                conn.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT [Artículos de clientes].NomArt, [Ordenes de fabricación].PieOrd, [Ordenes de fabricación].ArtOrd FROM [Artículos de clientes] INNER JOIN [Ordenes de fabricación] ON [Artículos de clientes].[CodArt] = [Ordenes de fabricación].[ArtOrd] where NumOrd =" + uidTextBox.Text + "and [Ordenes de fabricación].Location= " + location, conn);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows) 
                {
                    while (reader.Read())
                    {
                        articleTxtBox.Text = reader["ArtOrd"].ToString();
                        qtyTextBox.Text = reader["PieOrd"].ToString();
                        articleDescriptionTxtBox.Text = reader["NomArt"].ToString();
                    }
                }
                conn.Close();
                stepTextBox.Focus();
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Something went wrong...!')", true);
                confrimationMsgLbl.Visible = true;
                confrimationMsgLbl.Text = "Error occured while fecthing data on UID text changed..  " + ex.Message;
                confrimationMsgLbl.ForeColor = Color.OrangeRed;
            }
        }
    
        protected void stepTextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox stepTextBox = sender as TextBox;
                GridViewRow rows = stepTextBox.NamingContainer as GridViewRow;
                int rowIndex = rows.RowIndex;

                GridViewRow row = GridView1.Rows[rowIndex];
                TextBox uidTextBox = (TextBox)row.FindControl("uidTxtBox");
                TextBox descriptionTextBox = (TextBox)row.FindControl("descriptionTxtBox");
                TextBox pieTextBox = (TextBox)row.FindControl("pieTxtBox");
                TextBox gradeTextBox = (TextBox)row.FindControl("gradeTxtBox");
                TextBox hardnessTextBox = (TextBox)row.FindControl("hardnessTxtBox");
                TextBox qtyTextBox = (TextBox)row.FindControl("qtyTxtBox");
                TextBox priceToOrderTextBox = (TextBox)row.FindControl("priceTxtBox");
                TextBox deliveryDateTextBox = (TextBox)row.FindControl("deliveryDateTxtBox");
                DropDownList vendorNameDLL = (DropDownList)row.FindControl("vendorNameDDL");

                conn.Open();

                string CodPie = "";

                OleDbCommand cmd = new OleDbCommand("Select Operac, CodPie from[Artículos de clientes (fases)] where CodArt ='" + articleTxtBox.Text+"' and  NumFas= "+stepTextBox.Text, conn);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        descriptionTextBox.Text = reader["Operac"].ToString();
                        CodPie = reader["CodPie"].ToString();
                    }
                }
                conn.Close();
                descriptionTextBox.Focus();

                conn.Open();
                OleDbCommand command = new OleDbCommand("Select CodPie, CalPie, DurPie from[Artículos de clientes (piezas)] where CodArt ='" + articleTxtBox.Text + "' and  CodPie= '" + CodPie+"'", conn);
                OleDbDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        pieTextBox.Text = dr["CodPie"].ToString();
                        gradeTextBox.Text = dr["CalPie"].ToString();
                        hardnessTextBox.Text = dr["DurPie"].ToString();
                    }
                }
                conn.Close();
                deliveryDateTextBox.Text = DateTime.Today.AddDays(5).ToString("dd-MMM-yyyy");
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Something went wrong...!')", true);
                confrimationMsgLbl.Visible = true;
                confrimationMsgLbl.Text = "Error occured while fecthing data on Step text changed..  " + ex.Message;
                confrimationMsgLbl.ForeColor = Color.OrangeRed;
            }
        }


        protected void vendorNameSelectedIndexChanged(object sender, EventArgs e) 
        {
            DropDownList vendorNameDDL = sender as DropDownList;
            GridViewRow rows = vendorNameDDL.NamingContainer as GridViewRow;
            int rowIndex = rows.RowIndex;

            GridViewRow row = GridView1.Rows[rowIndex];
            Label errormsglbl = (Label)row.FindControl("errormsglbl");
            vendorNameDDL.BackColor = Color.Transparent;
            errormsglbl.Visible = false;

        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
            String orderNum = "";
            try
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT Top 1 NumPed FROM [Pedidos a proveedor (cabeceras)] Order By NumPed DESC", conn);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        orderNum = (Convert.ToInt32(reader["NumPed"]) + 1).ToString();
                    }
                }
                conn.Close();

                conn.Open();
                OleDbCommand cmand = new OleDbCommand("INSERT INTO [Pedidos a proveedor (cabeceras)] (NumPed, FecPed, NumOfe, ProPed) values ('" + orderNum + "','" + dateTxtBox.Text + "','0'," + BhatMaetalCode + ")", conn);
                cmand.ExecuteNonQuery();
                conn.Close();

                conn.Open();
                OleDbCommand comm = new OleDbCommand("Update [Parámetros] SET ValPar=" + orderNum + " Where CodPar = 'Último pedido (metal duro)'", conn);
                comm.ExecuteNonQuery();
                conn.Close();

            
                foreach (GridViewRow row in GridView1.Rows)
                {
                    TextBox uidTextBox = (TextBox)row.FindControl("uidTxtBox");
                    TextBox stepTxtBox = (TextBox)row.FindControl("stepTxtBox");
                    TextBox descriptionTextBox = (TextBox)row.FindControl("descriptionTxtBox");
                    TextBox pieTextBox = (TextBox)row.FindControl("pieTxtBox");
                    TextBox gradeTextBox = (TextBox)row.FindControl("gradeTxtBox");
                    TextBox hardnessTextBox = (TextBox)row.FindControl("hardnessTxtBox");
                    TextBox qtyTextBox = (TextBox)row.FindControl("qtyTxtBox");
                    TextBox priceToOrderTextBox = (TextBox)row.FindControl("priceTxtBox");
                    TextBox deliveryDateTextBox = (TextBox)row.FindControl("deliveryDateTxtBox");
                    DropDownList vendorNameDLL = (DropDownList)row.FindControl("vendorNameDDL");
                    Label errormsglbl = (Label)row.FindControl("errormsglbl");

                    if (vendorNameDLL.SelectedItem.Text == "--Select Vendor Name--")
                    {
                        vendorNameDLL.BackColor = Color.OrangeRed;
                        errormsglbl.Visible = true;
                        errormsglbl.Text = "Please Select Vendor Name....";
                    }
                    else
                    {
                        conn.Open();
                        OleDbCommand command = conn.CreateCommand();
                        command = new OleDbCommand("INSERT INTO [Pedidos a proveedor (líneas)] (NumPed, NumFas, NumOrd, CodPie, PiePed, PlaPie) values (?,?,?,?,?,?) ", conn);
                        command.Parameters.Add("@NumPed", OleDbType.Integer).Value = orderNum;
                        command.Parameters.Add("@NumFas", OleDbType.Integer).Value = stepTxtBox.Text;
                        command.Parameters.Add("@NumOrd", OleDbType.Integer).Value = uidTextBox.Text;
                        command.Parameters.Add("@CodPie", OleDbType.VarChar).Value = pieTextBox.Text;
                        command.Parameters.Add("@PiePed", OleDbType.Integer).Value = qtyTextBox.Text;
                        command.Parameters.Add("@PlaPie", OleDbType.Date).Value = deliveryDateTextBox.Text;
                        int insertrow = command.ExecuteNonQuery();
                        conn.Close();
                    }
                }

                confrimationMsgLbl.Visible = true;
                confrimationMsgLbl.Text = "Data Saved Successfully....!";
                confrimationMsgLbl.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                confrimationMsgLbl.Visible = true;
                confrimationMsgLbl.Text = "Error occured while Submitting data..  " + ex.Message;
                confrimationMsgLbl.ForeColor = Color.OrangeRed;
            }
        }

        protected void printBtn_Click(object sender, EventArgs e)
        {
            DataTable PrintingData = new DataTable();
            PrintingData.Columns.Add("UID");
            PrintingData.Columns.Add("step");
            PrintingData.Columns.Add("description");
            PrintingData.Columns.Add("pie");
            PrintingData.Columns.Add("grade");
            PrintingData.Columns.Add("hardness");
            PrintingData.Columns.Add("qty");
            PrintingData.Columns.Add("price");
            PrintingData.Columns.Add("deliveryDate");
            PrintingData.Columns.Add("vendorName");
            PrintingData.Columns.Add("orderNum");
            PrintingData.Columns.Add("orderDate");

            try
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    TextBox uidTextBox = (TextBox)row.FindControl("uidTxtBox");
                    TextBox stepTxtBox = (TextBox)row.FindControl("stepTxtBox");
                    TextBox descriptionTextBox = (TextBox)row.FindControl("descriptionTxtBox");
                    TextBox pieTextBox = (TextBox)row.FindControl("pieTxtBox");
                    TextBox gradeTextBox = (TextBox)row.FindControl("gradeTxtBox");
                    TextBox hardnessTextBox = (TextBox)row.FindControl("hardnessTxtBox");
                    TextBox qtyTextBox = (TextBox)row.FindControl("qtyTxtBox");
                    TextBox priceToOrderTextBox = (TextBox)row.FindControl("priceTxtBox");
                    TextBox deliveryDateTextBox = (TextBox)row.FindControl("deliveryDateTxtBox");
                    DropDownList vendorNameDLL = (DropDownList)row.FindControl("vendorNameDDL");

                    PrintingData.Rows.Add(uidTextBox.Text, stepTxtBox.Text, descriptionTextBox.Text, pieTextBox.Text, gradeTextBox.Text, hardnessTextBox.Text, qtyTextBox.Text, priceToOrderTextBox.Text, deliveryDateTextBox.Text, vendorNameDLL.SelectedItem.Text, orderNumTxtBox.Text, dateTxtBox.Text);
                }
            }
            catch (Exception ex)
            {
                confrimationMsgLbl.Visible = true;
                confrimationMsgLbl.Text = "Error occured while printing data on print button clicked..  " + ex.Message;
                confrimationMsgLbl.ForeColor = Color.OrangeRed;
            }
            Session["PrintingDataTable"] = PrintingData;

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Newtab", "window.open ('OrderReportPrint.aspx','_blank')", true);

        }

    }
}