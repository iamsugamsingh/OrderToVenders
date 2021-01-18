using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Net;
using System.Drawing;
using Aws;

namespace OrderToVenders
{
    public partial class OrderReportPrint : System.Web.UI.Page
    {
        Exception ex;
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable PrintingData = Session["PrintingDataTable"] as DataTable;

            if (!IsPostBack)
            {
                try
                {
                    DeletePdf deletePdf = new DeletePdf();
                    string status=deletePdf.DeletePdfFiles(Server.MapPath(@"~/pdf/")).ToLower();

                    if (status.Contains("file deleted") || status.Contains("empty folder"))
                    {
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/OrderReportPdf.rdlc");
                        ReportDataSource datasource = new ReportDataSource("DataSet1", PrintingData);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(datasource);

                        SavePDF(ReportViewer1, Server.MapPath(@"~\pdf\" + PrintingData.Rows[0]["orderNum"].ToString() + ".pdf"));
                    }
                    else
                    {
                        confrimationMsgLbl.Visible = true;
                        confrimationMsgLbl.Text = "Error occur while generating PDF.. "+status;
                        confrimationMsgLbl.ForeColor = Color.Red;
                    }
                }
                catch (Exception exx)
                {
                    ex = exx;
                    confrimationMsgLbl.Visible = true;
                    confrimationMsgLbl.Text = "Error occur while generating PDF.. "+ex.Message;
                    confrimationMsgLbl.ForeColor = Color.Red;
                }

                if (ex == null)
                {
                    if (File.Exists(Server.MapPath(@"~\pdf\" + PrintingData.Rows[0]["orderNum"].ToString() + ".pdf")))
                    {
                        string path = Server.MapPath(@"~\pdf\" + PrintingData.Rows[0]["orderNum"].ToString() + ".pdf");
                        WebClient client = new WebClient();
                        Byte[] buffer = client.DownloadData(path);

                        if (buffer != null)
                        {
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-length", buffer.Length.ToString());
                            Response.BinaryWrite(buffer);
                        }
                    }
                }
            }
        }

        public void SavePDF(ReportViewer viewer, string savePath)
        {
            string deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>PDF</OutputFormat>" +
                    "  <PageWidth>8.5in</PageWidth>" +
                    "  <PageHeight>11in</PageHeight>" +
                    "  <MarginTop>0.2in</MarginTop>" +
                    "  <MarginLeft>0.2in</MarginLeft>" +
                    "  <MarginRight>0.2in</MarginRight>" +
                    "  <MarginBottom>0.2in</MarginBottom>" +
                    "  <HumanReadablePDF>True</HumanReadablePDF>" +
                    "</DeviceInfo>";
            byte[] Bytes = viewer.LocalReport.Render(format: "PDF", deviceInfo: deviceInfo);

            using (FileStream stream = new FileStream(savePath, FileMode.Create))
            {
                stream.Write(Bytes, 0, Bytes.Length);
            }
        }
    }
}