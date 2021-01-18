using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderToVenders
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                orderLinkBtn.Text = "Order";
                receiveLinkBtn.Text = "Receive";

                if (orderLinkBtn.Text == "Order" && receiveLinkBtn.Text == "Receive")
                {
                    backLinkBtn.Visible = false;
                }
                else
                {
                    backLinkBtn.Visible = true;
                }
            }            
        }

        protected void orderLinkBtn_Click(object sender, EventArgs e)
        {
            if (ViewState["btnClicked"] == null)
            {
                ViewState["btnClicked"] = "orderBtn";
                orderLinkBtn.Text = "AWS1";
                receiveLinkBtn.Text = "AWS2";
                backLinkBtn.Visible = true;
            }
            else
            {
                if (ViewState["btnClicked"].ToString() == "orderBtn" && orderLinkBtn.Text == "AWS1")
                {
                    Response.Redirect("~/Order.aspx?loc=1");
                }
                else if (ViewState["btnClicked"].ToString() == "receiveBtn" && orderLinkBtn.Text == "AWS1")
                {
                    Response.Redirect("~/Receive.aspx?loc=1");
                }
                else
                {
                    ViewState["btnClicked"] = "orderBtn";
                    orderLinkBtn.Text = "AWS1";
                    receiveLinkBtn.Text = "AWS2";
                    backLinkBtn.Visible = true;
                }
            }
        }

        protected void receiveLinkBtn_Click(object sender, EventArgs e)
        {
            if (ViewState["btnClicked"] == null)
            {
                ViewState["btnClicked"] = "receiveBtn";
                orderLinkBtn.Text = "AWS1";
                receiveLinkBtn.Text = "AWS2";
                backLinkBtn.Visible = true;
            }
            else
            {
                if (ViewState["btnClicked"].ToString() == "receiveBtn" && receiveLinkBtn.Text == "AWS2")
                {
                    Response.Redirect("~/Receive.aspx?loc=2");
                }
                else if (ViewState["btnClicked"].ToString() == "orderBtn" && receiveLinkBtn.Text == "AWS2")
                {
                    Response.Redirect("~/Order.aspx?loc=2");
                }
                else
                {
                    ViewState["btnClicked"] = "receiveBtn";
                    orderLinkBtn.Text = "AWS1";
                    receiveLinkBtn.Text = "AWS2";
                    backLinkBtn.Visible = true;
                }
            }
        }

        protected void backLinkBtn_Click(object sender, EventArgs e)
        {
            orderLinkBtn.Text = "Order";
            receiveLinkBtn.Text = "Receive";
            backLinkBtn.Visible = false;
        }
    }
}
