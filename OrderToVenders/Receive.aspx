<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Receive.aspx.cs" Inherits="OrderToVenders.Receive" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" EnableViewState="true">
        <ContentTemplate>

        <script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery/jquery-1.4.2.min.js"></script>
        

    <script>
        function jScript() {
            var $inp = $('.cls');

            var firstIdx = $inp.index(0);
            $(".cls:eq(" + firstIdx + ")").focus();
            $(".cls:eq(" + firstIdx + ")").select();

            $inp.bind('keydown', function (e) {
                //var key = (e.keyCode ? e.keyCode : e.charCode);
                var key = e.which;
                if (key == 38) {
                    e.preventDefault();
                    var nxtIdx = $inp.index(this) - 1;
                    $(".cls:eq(" + nxtIdx + ")").focus();
                    $(".cls:eq(" + nxtIdx + ")").select();
                    document.getElementById("trackArrowKeyDown").value = "Up";
                }
                if (key == 40) {
                    e.preventDefault();
                    var nxtIdx = $inp.index(this) + 1;
                    $(".cls:eq(" + nxtIdx + ")").focus();
                    $(".cls:eq(" + nxtIdx + ")").select();
                    document.getElementById("trackArrowKeyDown").value = "Down";
                }

            });
        }
    </script>

            <script type="text/javascript" language="javascript">Sys.Application.add_load(jScript);</script>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" EnableViewState="true">
        <ContentTemplate>
    <div class="Container">

        <h3 style="margin-top:10px;">
            <center>Receive Job Work</center>
            <h3>
            </h3>
            <center>
                <table border="0" cellpadding="5" style="margin-top:25px;">
                    <tr>
                        <td>
                            Location<br>
                            <asp:TextBox ID="orderDateTxtBox" runat="server" class="form-control" required></asp:TextBox>
                            </br>
                        </td>
                        <%--<td>
                            Order Num<br>
                            <asp:DropDownList ID="orderNumDDL" runat="server" class="form-control" AutoPostBack="true"
                                onselectedindexchanged="orderNumDDL_SelectedIndexChanged" Width="100px">
                            </asp:DropDownList>
                            </br>
                        </td>
                        <td>
                            Order Date<br>
                            <asp:TextBox ID="orderDateTxtBox" runat="server" class="form-control" required></asp:TextBox>
                            </br>
                        </td>
                        <td>
                            Inq/Ref<br>
                            <asp:TextBox ID="inqRefTxtBox" runat="server" class="form-control"></asp:TextBox>
                            </br>
                        </td>--%>
                        <td>
                            Vendor<br>
                            <asp:TextBox ID="vendorCodeTxtBox" runat="server" class="form-control" Text="22" required></asp:TextBox>
                            </br>
                        </td>
                        <td>
                            Vendor Name<br>
                            <asp:TextBox ID="vendorNameTxtBox" runat="server" class="form-control" Text="BHAT METALS" required></asp:TextBox>
                            </br>
                        </td>
                        <td>
                            Delivery Date<br>
                            <asp:TextBox ID="deliveryDateTxtBox" runat="server" class="form-control" required></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="deliveryDateTxtBox" Format="dd-MMM-yyyy"/>
                            </br>
                        </td>
                    </tr>
                </table>
            </center>
        </h3>
    
    </div>
     </ContentTemplate>
    </asp:UpdatePanel>         
        <script type="text/javascript" language="javascript">Sys.Application.add_load(jScript);</script>
       <div class="Container" style="margin-top:-40px;">
        <asp:GridView ID="GridView1" runat="server"  Width="100%" AutoGenerateColumns="false" HeaderStyle-CssClass="GridHeader"  EnableViewState="true" HorizontalAlign="Center">
                <Columns> 
                    <asp:TemplateField HeaderText="Order No" HeaderStyle-Width="125px">

                        <ItemTemplate>

                            <asp:Label ID="orderNumLbl" runat="server" Text='<%# Eval("NumPed") %>'></asp:Label>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="UID" HeaderStyle-Width="100px">

                        <ItemTemplate>

                            <asp:Label ID="uidLbl" runat="server" Text='<%# Eval("NumOrd") %>'></asp:Label>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Element" HeaderStyle-Width="100px">

                        <ItemTemplate>

                            <asp:Label ID="elementLbl" runat="server" Text='<%# Eval("CodPie") %>'></asp:Label>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Step" HeaderStyle-Width="100px">

                        <ItemTemplate>

                            <asp:Label ID="stepLbl" runat="server" Text='<%# Eval("NumFas") %>'></asp:Label>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Article" HeaderStyle-Width="150px">

                        <ItemTemplate>
                            <asp:Label ID="articleLbl" runat="server" Text='<%# Eval("ArtOrd") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Order Date" HeaderStyle-Width="200px">

                        <ItemTemplate>
                            
                            <asp:Label ID="orderdateLbl" runat="server" Text='<%# Bind("FecPed","{0:dd-MMM-yyyy}") %>'></asp:Label>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delivery Date" HeaderStyle-Width="200px">

                        <ItemTemplate>
                            
                            <asp:Label ID="deliverydateLbl" runat="server" Text='<%# Bind("PlaPie","{0:dd-MMM-yyyy}") %>'></asp:Label>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Order Quantity" HeaderStyle-Width="200px">

                        <ItemTemplate>
                            <asp:Label ID="orderQuantityLbl" runat="server" Text='<%# Eval("PiePed") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Quantity Received" HeaderStyle-Width="200px">

                        <ItemTemplate>

                            <asp:TextBox class="cls" Width="100%" ID="quantityReceivedTxtBox" runat="server" AutoPostBack="true" OnTextChanged="quantityReceivedTxtBox_TxtChanged" Text='<%# Eval("PieRec") %>' required style="border:2px solid gray; border-radius:5px;"></asp:TextBox>
                            <span>
                                <asp:Label ID="errorLbl" runat="server" Font-Size="11px" Font-Bold="True"></asp:Label>
                            </span>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Quantity Pending" HeaderStyle-Width="150px">

                        <ItemTemplate>
                            <asp:Label ID="quantitypendingLbl" runat="server" Text='<%# Eval("pendingQuantity") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
            </Columns>
                
             <HeaderStyle CssClass="GridHeader" BackColor="Black" ForeColor="White"></HeaderStyle>
        </asp:GridView>
     <center>

         <asp:UpdateProgress ID="updProgress"
        AssociatedUpdatePanelID="UpdatePanel3"
        runat="server">
            <ProgressTemplate>           
            <img src="images/processing.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress>   

        
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
        <asp:Button ID="submitButton" runat="server" Text="Submit" class="btn btn-success" 
                Style="width:200px; margin-top:25px; font-size:larger" 
                onclick="submitButton_Click" />
            <br />
            <br />
            <asp:Label ID="confrimationMsgLbl" runat="server" Visible="false"></asp:Label>
                <asp:HiddenField ID="trackArrowKeyDown" runat="server" ClientIDMode="Static"/>

             </ContentTemplate>
        </asp:UpdatePanel> 
    </center>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>

    

</asp:Content>