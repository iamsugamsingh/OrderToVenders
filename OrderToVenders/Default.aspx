<%@ Page Title="Order To Vendor" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="OrderToVenders._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<div class="Container">
    <div class="row">
            <div class="col-lg-6">
                <asp:LinkButton ID="orderLinkBtn" runat="server" onclick="orderLinkBtn_Click" class="location1 content">
                    
                </asp:LinkButton>
            </div>
            <div class="col-lg-6">
                <asp:LinkButton ID="receiveLinkBtn" runat="server" onclick="receiveLinkBtn_Click" class="location2 content">
                    
                </asp:LinkButton>
            </div>
        </div>
    <br />
    <br /> 
        <center>
            <asp:LinkButton ID="backLinkBtn" runat="server" onclick="backLinkBtn_Click" text="Back">
                    
                </asp:LinkButton>
        </center>       
    </div>
</asp:Content>
