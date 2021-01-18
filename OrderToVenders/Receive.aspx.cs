using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Drawing;

namespace OrderToVenders
{
    public partial class Receive : System.Web.UI.Page
    {
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            confrimationMsgLbl.Visible = false;
            if (!IsPostBack)
            {
                try
                {
                    int location = Convert.ToInt32(Request.QueryString["loc"].ToString());
                    if (location == 1)
                    {
                        orderDateTxtBox.Text = "AWS-1";
                    }

                    if (location == 2)
                    {
                        orderDateTxtBox.Text = "AWS-2";
                    }
                    deliveryDateTxtBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");

                    conn.Open();
                   // OleDbCommand cmd = new OleDbCommand("SELECT DISTINCTROW NumPed FROM [Pedidos a proveedor (cabeceras)] ORDER BY [Pedidos a proveedor (cabeceras)].NumPed DESC", conn);
                    OleDbCommand cmd = new OleDbCommand("select [Pedidos a proveedor (líneas)].NumPed, [Pedidos a proveedor (líneas)].NumOrd, CodPie, NumFas, PiePed, PieRec, PlaPie, FecPed from [Pedidos a proveedor (líneas)] inner join [Pedidos a proveedor (cabeceras)] on [Pedidos a proveedor (líneas)].NumPed=[Pedidos a proveedor (cabeceras)].NumPed  where PiePed<>PieRec and ProPed=22 order by FecPed asc", conn);
                    OleDbDataAdapter adap = new OleDbDataAdapter(cmd);
                    DataTable Data = new DataTable();
                    adap.Fill(Data);

                    conn.Close();

                    DataTable allData = new DataTable();
                    allData.Columns.Add("NumPed");
                    allData.Columns.Add("NumOrd");
                    allData.Columns.Add("CodPie");
                    allData.Columns.Add("NumFas");
                    allData.Columns.Add("ArtOrd");
                    allData.Columns.Add("FecPed");
                    allData.Columns.Add("PlaPie");
                    allData.Columns.Add("PiePed");
                    allData.Columns.Add("PieRec");
                    allData.Columns.Add("pendingQuantity");

                    for (int i = 0; i < Data.Rows.Count; i++)
                    {
                        conn.Open();
                        OleDbCommand comm = new OleDbCommand("Select ArtOrd from [Ordenes de fabricación] where NumOrd= " + Data.Rows[i]["NumOrd"].ToString()+" and Location="+location, conn);
                        OleDbDataReader r = comm.ExecuteReader();
                        if (r.HasRows)
                        {
                            while (r.Read())
                            {
                                allData.Rows.Add(Data.Rows[i]["NumPed"].ToString(), Data.Rows[i]["NumOrd"].ToString(), Data.Rows[i]["CodPie"].ToString(), Data.Rows[i]["NumFas"].ToString(), r["ArtOrd"].ToString(), Convert.ToDateTime(Data.Rows[i]["FecPed"].ToString()).ToString("dd-MMM-yyyy"), Convert.ToDateTime(Data.Rows[i]["PlaPie"].ToString()).ToString("dd-MMM-yyyy"), Data.Rows[i]["PiePed"].ToString(), Data.Rows[i]["PieRec"].ToString(), Convert.ToInt32(Data.Rows[i]["PiePed"].ToString()) - Convert.ToInt32(Data.Rows[i]["PieRec"].ToString()));
                            }
                        }

                        conn.Close();
                    }
                    GridView1.DataSource = allData;
                    GridView1.DataBind();                        
                }
                catch(Exception ex)
                {
                    confrimationMsgLbl.Visible = true;
                    confrimationMsgLbl.Text = "Error occur while fetching the data on page load..! " + ex.Message;
                    confrimationMsgLbl.ForeColor = Color.OrangeRed;
                }
            }
        }

        protected void quantityReceivedTxtBox_TxtChanged(object sender, EventArgs e)
        {
            TextBox quantityReceivedTextBox = sender as TextBox;
            GridViewRow rows = quantityReceivedTextBox.NamingContainer as GridViewRow;
            int rowIndex = rows.RowIndex;

            GridViewRow row = GridView1.Rows[rowIndex];
            Label orderQuantityLbl = (Label)row.FindControl("orderQuantityLbl");
            Label quantitypendingLbl = (Label)row.FindControl("quantitypendingLbl");

            quantitypendingLbl.Text = (Convert.ToInt32(orderQuantityLbl.Text) - Convert.ToInt32(quantityReceivedTextBox.Text)).ToString();

            if (trackArrowKeyDown.Value == "Down")
            {
                rowIndex++;
                if (GridView1.Rows.Count > rowIndex)
                {
                    row = GridView1.Rows[rowIndex];
                    TextBox nextQuantityReceivedTextBox = (TextBox)row.FindControl("quantityReceivedTxtBox");
                    nextQuantityReceivedTextBox.Focus();
                }
            }

            if (trackArrowKeyDown.Value == "Up")
            {
                rowIndex--;
                if (GridView1.Rows.Count > rowIndex)
                {
                    row = GridView1.Rows[rowIndex];
                    TextBox nextQuantityReceivedTextBox = (TextBox)row.FindControl("quantityReceivedTxtBox");
                    nextQuantityReceivedTextBox.Focus();
                }
            }
        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
            if (isQuantityRecievedZero() == false)
            {
                try
                {
                    int qty = 0;

                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        Label orderNumLbl = (Label)row.FindControl("orderNumLbl");
                        Label uidLbl = (Label)row.FindControl("uidLbl");
                        Label elementLbl = (Label)row.FindControl("elementLbl");
                        Label stepLbl = (Label)row.FindControl("stepLbl");
                        Label articleLbl = (Label)row.FindControl("articleLbl");
                        Label orderdateLbl = (Label)row.FindControl("orderdateLbl");
                        Label deliverydateLbl = (Label)row.FindControl("deliverydateLbl");
                        Label orderQuantityLbl = (Label)row.FindControl("orderQuantityLbl");
                        TextBox quantityReceivedTxtBox = (TextBox)row.FindControl("quantityReceivedTxtBox");
                        Label errorLbl = (Label)row.FindControl("errorLbl");
                        int AuxCtd1 = 0;

                        if (quantityReceivedTxtBox.Text!="0")
                        {
                            conn.Open();
                            OleDbCommand cmd = new OleDbCommand("Select AuxCtd from [Pedidos a proveedor (líneas)] where NumOrd=" + uidLbl.Text + " and CodPie= '" + elementLbl.Text+"'", conn);
                            OleDbDataReader r = cmd.ExecuteReader();
                            if (r.HasRows)
                            {
                                while (r.Read())
                                {
                                    if (r["AuxCtd"].ToString() == "")
                                    {
                                        AuxCtd1 = 0;
                                    }
                                    else
                                    {
                                        AuxCtd1 = Convert.ToInt32(r["AuxCtd"].ToString());
                                    }
                                }
                            }
                            conn.Close();
                            if (AuxCtd1 == 0)
                            {
                                conn.Open();
                                OleDbCommand cmnd = new OleDbCommand("UPDATE [Pedidos a proveedor (líneas)] SET AuxCtd=" + qty + " WHERE NumOrd = ? AND  CodPie=? ", conn);
                                cmnd.Parameters.Add("@uid", OleDbType.VarChar).Value = uidLbl.Text;
                                cmnd.Parameters.Add("@element", OleDbType.VarChar).Value = elementLbl.Text;
                                cmnd.ExecuteNonQuery();
                                conn.Close();
                            }
                            else if (AuxCtd1 != 0)
                            {
                                int t = AuxCtd1 + Convert.ToInt32(qty);

                                conn.Open();
                                OleDbCommand com = new OleDbCommand("UPDATE [Pedidos a proveedor (líneas)] SET AuxCtd=" + Convert.ToString(t) + " WHERE NumOrd = ? AND  CodPie=? ", conn);
                                com.Parameters.Add("@uid", OleDbType.VarChar).Value = uidLbl.Text;
                                com.Parameters.Add("@element", OleDbType.VarChar).Value = elementLbl.Text;
                                com.ExecuteNonQuery();
                                conn.Close();
                            }
                            conn.Open();
                            OleDbCommand command = new OleDbCommand("INSERT INTO [Ordenes de fabricación (historia/exterior)]  (NumOrd,NumFas, CodPie,FecAlbExt,CanPie,CodProExt,OrderDate) VALUES (?,?,?,?,?,?,?)", conn);
                            command.Parameters.Add("@NumOrd", OleDbType.Integer).Value = Convert.ToInt32(uidLbl.Text);
                            command.Parameters.Add("@NumFas", OleDbType.Integer).Value = Convert.ToInt32(stepLbl.Text);
                            command.Parameters.Add("@CodPie", OleDbType.VarChar).Value = elementLbl.Text;
                            command.Parameters.Add("@FecAlbExt", OleDbType.Date).Value = deliveryDateTxtBox.Text;
                            command.Parameters.Add("@CanPie", OleDbType.Integer).Value = Convert.ToInt32(quantityReceivedTxtBox.Text);
                            command.Parameters.Add("@CodProExt", OleDbType.Integer).Value = Convert.ToInt32(vendorCodeTxtBox.Text);
                            command.Parameters.Add("@OrderDate", OleDbType.Date).Value = orderdateLbl.Text;
                            command.ExecuteNonQuery();
                            conn.Close();

                            conn.Open();
                            OleDbCommand comnd = new OleDbCommand("UPDATE [Pedidos a proveedor (líneas)] SET PieRec=" + Convert.ToString(quantityReceivedTxtBox.Text) + " WHERE NumOrd = ? AND  CodPie=? ", conn);
                            comnd.Parameters.Add("@uid", OleDbType.VarChar).Value = uidLbl.Text;
                            comnd.Parameters.Add("@element", OleDbType.VarChar).Value = elementLbl.Text;
                            comnd.ExecuteNonQuery();
                            conn.Close();

                            int AuxCtd = 0, PiePed = 0;

                            conn.Open();
                            OleDbCommand cmd2 = new OleDbCommand("select PiePed,AuxCtd FROM [Pedidos a proveedor (líneas)] WHERE NumOrd = ? AND  CodPie=? ", conn);
                            cmd2.Parameters.Add("@uid", OleDbType.VarChar).Value = Convert.ToInt32(uidLbl.Text);
                            cmd2.Parameters.Add("@element", OleDbType.VarChar).Value = elementLbl.Text;
                            OleDbDataReader reader2 = cmd2.ExecuteReader();
                            if (reader2.Read())
                            {
                                AuxCtd = Convert.ToInt32(reader2.GetValue(reader2.GetOrdinal("AuxCtd")));
                                PiePed = Convert.ToInt32(reader2.GetValue(reader2.GetOrdinal("PiePed")));
                            }
                            conn.Close();

                            if (AuxCtd - PiePed <= 0)
                            {
                                conn.Open();
                                OleDbCommand c = new OleDbCommand("DELETE FROM [Pedidos a proveedor (líneas)] WHERE NumOrd =? AND  CodPie=? ", conn);
                                c.Parameters.Add("@uid", OleDbType.VarChar).Value = Convert.ToInt32(uidLbl.Text);
                                c.Parameters.Add("@element", OleDbType.VarChar).Value = elementLbl.Text;
                                c.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                        
                    }

                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Order Received.')", true);

                    confrimationMsgLbl.Visible = true;
                    confrimationMsgLbl.Text = "Data Saved Successfully...!";
                    confrimationMsgLbl.ForeColor = Color.Green;
                }
                catch (Exception ex)
                {
                    confrimationMsgLbl.Visible = true;
                    confrimationMsgLbl.Text = "Error occur while submitting the data..! "+ ex.Message;
                    confrimationMsgLbl.ForeColor = Color.OrangeRed;
                }
            }
        }

        public bool isQuantityRecievedZero()
        {
            bool status = false;
            foreach (GridViewRow row in GridView1.Rows)
            {
                TextBox quantityReceivedTxtBox = (TextBox)row.FindControl("quantityReceivedTxtBox");
                Label errorLbl = (Label)row.FindControl("errorLbl");

                if (Convert.ToInt32(quantityReceivedTxtBox.Text) == -1)
                {
                    errorLbl.Text = "'Quantity Received Value cannot be zero...!'";
                    errorLbl.Visible = true;
                    errorLbl.ForeColor = Color.OrangeRed;
                    quantityReceivedTxtBox.BackColor = Color.OrangeRed;
                    status = true;
                    break;
                }
                else
                {
                    status = false;
                }
            }
            return status;
        }
    }
}