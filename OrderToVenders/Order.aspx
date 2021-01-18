<%@ Page Title="Order Job Work" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Order.aspx.cs" Inherits="OrderToVenders.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <script src="Scripts/jquery-1.4.2.min.js"></script>
    <script>
        function jScript() {
            $('#TextBox1').focus();
            var $inp = $('.cls');
            $inp.bind('keydown', function (e) {
                //var key = (e.keyCode ? e.keyCode : e.charCode);
                var key = e.which;
                if (key == 13) {
                    e.preventDefault();
                    var nxtIdx = $inp.index(this) + 1;
                    $(".cls:eq(" + nxtIdx + ")").focus();
                    if (nxtIdx % 10 == 0) {
                        $(".cls:eq(" + nxtIdx + ")").focus();
                        $(".cls:eq(" + nxtIdx + ")").click();
                        $(".cls:eq(" + nxtIdx + ")").select();
                    }
                }
                if (key == 38) {
                    e.preventDefault();
                    var nxtIdx = $inp.index(this) - 10;
                    $(".cls:eq(" + nxtIdx + ")").focus();
                    $(".cls:eq(" + nxtIdx + ")").select();
                }
                if (key == 40) {
                    e.preventDefault();
                    var nxtIdx = $inp.index(this) + 10;
                    $(".cls:eq(" + nxtIdx + ")").focus();
                    $(".cls:eq(" + nxtIdx + ")").select();
                }
                if (key == 37) {
                    e.preventDefault();
                    var nxtIdx = $inp.index(this) - 1;
                    $(".cls:eq(" + nxtIdx + ")").focus();
                    $(".cls:eq(" + nxtIdx + ")").select();
                }
                if (key == 39) {
                    e.preventDefault();
                    var nxtIdx = $inp.index(this) + 1;
                    $(".cls:eq(" + nxtIdx + ")").focus();
                    $(".cls:eq(" + nxtIdx + ")").select();
                }
            });
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" EnableViewState="true">
        <ContentTemplate>
            <script type="text/javascript" language="javascript">Sys.Application.add_load(jScript);</script>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" EnableViewState="true">
        <ContentTemplate>

        <h3 style="margin-top:25px;">
            <center>Order Job Work</center>
            <h3>
            </h3>
            <div class="Container">
                <center>
                    <table border="0" cellpadding="5">
                        <tr>
                            <td>
                                Location<br>
                                <asp:TextBox ID="locationTxtBox" runat="server" class="form-control" 
                                    required=""></asp:TextBox>
                                </br>
                            </td>
                            <td>
                                Date<br>
                                <asp:TextBox ID="dateTxtBox" runat="server" class="form-control" required=""></asp:TextBox>
                                </br>
                            </td>
                            <td>
                                Order Number<br>
                                <asp:TextBox ID="orderNumTxtBox" runat="server" class="form-control" 
                                    required=""></asp:TextBox>
                                </br>
                            </td>
                            <td>
                                Article<br>
                                <asp:TextBox ID="articleTxtBox" runat="server" class="form-control" required=""></asp:TextBox>
                                </br>
                            </td>
                            <td>
                                Article Description<br>
                                <asp:TextBox ID="articleDescriptionTxtBox" runat="server" class="form-control" 
                                    required=""></asp:TextBox>
                                </br>
                            </td>
                            <%--<td>
                                <br />
                                <asp:Button ID="viewBtn" runat="server" class="btn btn-light" 
                                    Style="background-color: #d3d3d3;" Text="View" />
                            </td>--%>
                            <td>
                                <br />
                                <asp:Button ID="printBtn" runat="server" class="btn" onclick="printBtn_Click" 
                                    Style="background-color: #d3d3d3; margin-top:-28px;" Text="Print" />
                            </td>
                        </tr>
                    </table>
                </center>
            </div>
        </h3>
    
            </ContentTemplate>
    </asp:UpdatePanel>         
        
       <script type="text/javascript" language="javascript">Sys.Application.add_load(jScript);</script>
       <div class="Container">
        <asp:GridView ID="GridView1" runat="server" ShowFooter="true" 
                AutoGenerateColumns="false"  Font-Size="Small" HorizontalAlign="Center" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle">

                <Columns>
                    <asp:TemplateField HeaderText = "Rem. Rows" ItemStyle-Width="70" >  
                        <ItemTemplate>  
                            <asp:Button ID="removeBtn" class="btn btn-danger" runat="server" style="width:100%;  font-size:12px; padding:3px;" Text="Remove" onclick="removeBtn_Click" UseSubmitBehavior="false"/>
                        </ItemTemplate>  
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText = "Row No." ItemStyle-Width="60">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="UID">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="uidTxtBox"  runat="server"   CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>" AutoPostBack="true" OnTextChanged="uidTextChanged" required></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Step">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="stepTxtBox"  runat="server"  AutoPostBack="true" OnTextChanged="stepTextChanged" required></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Description">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="descriptionTxtBox" runat="server"  required></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Pie">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="pieTxtBox" runat="server"  CommandName="Select"  CommandArgument="<%# Container.DataItemIndex %>" required></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Grade">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="gradeTxtBox" runat="server"  required></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Hardeness">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="hardnessTxtBox" runat="server" required></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Qty">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="qtyTxtBox" runat="server"  required></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Price">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="priceTxtBox" runat="server"></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delivery Date">

                        <ItemTemplate>

                            <asp:TextBox class="cls" ID="deliveryDateTxtBox" runat="server" required></asp:TextBox>                           

                        </ItemTemplate>

                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Vendor Name">

                        <ItemTemplate>

                            <asp:DropDownList ID="vendorNameDDL" runat="server"  class="cls" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="vendorNameSelectedIndexChanged">
                             <asp:ListItem Value="22" Text="BHAT METALS"></asp:ListItem>
                             </asp:DropDownList>
                             <span>
                                 <asp:Label ID="errormsglbl" runat="server" Visible="False"></asp:Label>
                            </span>

                        </ItemTemplate>

                        <FooterStyle HorizontalAlign="Right" />

                        <FooterTemplate>

                            <asp:Button ID="ButtonAdd" class="cls btn btn-sm btn-success" runat="server" style="width:100%; border:none;" Text="Add New Row" onClick="ButtonAdd_Click" BorderStyle="Outset" AccessKey="n" />

                        </FooterTemplate>

                    </asp:TemplateField>

                </Columns>
                <HeaderStyle BackColor="Black" ForeColor="White" HorizontalAlign="Center" 
                    VerticalAlign="Middle" />
            </asp:GridView> 
            
        <center>
        <asp:Button ID="submitButton" runat="server" Text="Submit" class="btn btn-success" 
                Style="width:200px; margin-top:25px; font-size:larger" 
                onclick="submitButton_Click" />
            <br />
            <br />
            <asp:Label ID="confrimationMsgLbl" runat="server" visible="false"></asp:Label>
    </center>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
