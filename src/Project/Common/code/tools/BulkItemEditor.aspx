<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BulkItemEditor.aspx.cs" Inherits="Sitecore.Common.Website.sitecore.admin.BulkItemEditor" EnableEventValidation="false" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <title>Bulk Item Editor</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <link rel="stylesheet" href="style.css">
</head>

<body>
    <div class="jumbotron text-center">
        <h2>Bulk Item Editor</h2>
    </div>
    <div class="container">
        <div class="row">
            <ul class="nav nav-tabs" role="tablist">
                <li class="active">
                    <a href="#addItem" data-toggle="tab">Add Data in Sitecore Items</a>
                </li>
                <li>
                    <a href="#updateItem" data-toggle="tab">Update Data in Sitecore Items</a>
                </li>
            </ul>
            <form id="form1" runat="server">
                <asp:ScriptManager ID="scMain" runat="server"></asp:ScriptManager>
                <div class="tab-content">
                    <div class="tab-pane active" id="addItem">
                        <asp:UpdatePanel ID="upnlTab1" runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="lnkAddNewRow" CssClass="btn btn-default" runat="server" OnClick="lnkAddNewRow_Click" Style="margin-top: 1.5%">Add New Row</asp:LinkButton>
                                <br />
                                <br />
                                <asp:Repeater ID="rpBulkItemEditor" runat="server" OnItemDataBound="rpBulkItemEditor_ItemDataBound">
                                    <ItemTemplate>
                                        <fieldset style="border-bottom: 1px solid grey; margin-bottom: 20px;">
                                            <table class="tableItems">
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="parentNode">
                                                                <span>Parent Item ID/Path</span>
                                                                <asp:TextBox ID="txtParentNode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqParentNode" runat="server" ValidationGroup="BulkItem" CssClass="alert-danger"
                                                                    ControlToValidate="txtParentNode" Display="Dynamic" ErrorMessage="Required!" InitialValue="" SetFocusOnError="true">
                                                                </asp:RequiredFieldValidator>
                                                            </label>
                                                        </div>
                                                    </td>

                                                    <td class="tableTD"></td>

                                                    <td>
                                                        <div class="form-group">
                                                            <label for="templateID">
                                                                <span>Template Id/Path</span>
                                                                <asp:TextBox ID="txtTemplateID" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqTemplateID" runat="server" ControlToValidate="txtTemplateID" CssClass="alert-danger"
                                                                    ValidationGroup="BulkItem" Display="Dynamic" ErrorMessage="Required!" InitialValue="" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            </label>
                                                        </div>
                                                    </td>

                                                    <td class="tableTD"></td>

                                                    <td>
                                                        <div class="form-group">
                                                            <label for="noOfItems">
                                                                <span>No. of Items</span>
                                                                <asp:TextBox ID="txtNoOfItems" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqNoOfItems" runat="server" ControlToValidate="txtNoOfItems" ValidationGroup="BulkItem" CssClass="alert-danger"
                                                                    Display="Dynamic" ErrorMessage="Required!" InitialValue="" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            </label>
                                                        </div>
                                                    </td>

                                                    <td class="tableTD"></td>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="cblLanguage">
                                                                <span>Languages</span>
                                                                <asp:CheckBoxList ID="cblLanguage" runat="server">
                                                                </asp:CheckBoxList>
                                                            </label>
                                                        </div>
                                                    </td>
                                                    <td class="tableTD"></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Button ID="btnSubmit" CssClass="btn btn-default" runat="server" Text="Create" OnClick="btnSubmit_Click" ValidationGroup="BulkItem" UseSubmitBehavior="true" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lnkAddNewRow" EventName="Click" />
                                <asp:PostBackTrigger ControlID="btnSubmit" />
                                <asp:AsyncPostBackTrigger ControlID="rpBulkItemEditor" EventName="ItemDataBound" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="tab-pane" id="updateItem">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table class="tableItems" style="margin-top: 30px;">
                                    <tr>
                                        <td>
                                            <div class="form-group">
                                                <label for="parentNode">
                                                    <span>Parent Item ID/Path</span>
                                                    <asp:TextBox ID="txtDownloadParentID" runat="server" CssClass="form-control tableWidth"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqtxtDownloadParentID" runat="server" ControlToValidate="txtDownloadParentID" CssClass="alert-danger"
                                                        Display="Dynamic" ErrorMessage="Required!" SetFocusOnError="true" InitialValue="" ValidationGroup="Download"></asp:RequiredFieldValidator>
                                                </label>
                                            </div>
                                        </td>
                                        <td class="tableTD"></td>
                                        <td class="tableTD"></td>

                                        <td>
                                            <div class="form-group">
                                                <label for="language">
                                                    <span>Select Languages</span>
                                                    <asp:DropDownList ID="ddlDownloadLanguage" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </label>
                                            </div>
                                        </td>
                                        <td class="tableTD"></td>
                                        <td class="tableTD"></td>
                                        <td>
                                            <asp:Button ID="btnDownload" runat="server" CssClass="btn btn-primary" Text="Download File" OnClick="btnDownload_Click" ValidationGroup="Download" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnDownload" />
                            </Triggers>
                        </asp:UpdatePanel>

                        <table class="tableItems" style="margin-top: 30px;">
                            <tr>
                                <td>
                                    <div class="form-group">
                                        <label for="uploadFile">
                                            <span>Select File For Upload</span>
                                            <asp:FileUpload runat="server" ID="uploadFile" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="reqFileUpload" runat="server" ControlToValidate="uploadFile" CssClass="alert-danger"
                                                Display="Dynamic" ErrorMessage="Required!" SetFocusOnError="true" InitialValue="" ValidationGroup="upload"></asp:RequiredFieldValidator>
                                        </label>
                                    </div>
                                </td>
                                <td class="tableTD"></td>
                                <td>
                                    <div class="form-group">
                                        <label for="language">
                                            <span>Select Languages</span>
                                            <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </label>
                                    </div>
                                </td>
                                <td class="tableTD"></td>
                                <td class="tableTD"></td>
                                <td>
                                    <asp:Button runat="server" CssClass="btn btn-primary" class="btn btn-primary" ID="btnUpload" OnClick="btnUpload_Click" Text="Upload File" ValidationGroup="upload" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </form>
        </div>
    </div>
    <div id="footer">
    </div>
    <script src="jquery-3.1.1.js"></script>
    <script>

        jQuery.noConflict();
        $('.add').click(function (e) {
            e.preventDefault();
            var data = "<tr><td><div class='form-group'><input type='text' class='form-control tableWidth' id='inputData' placeholder='Parent Node'></div></td><td class='tableTD'></td><td><div class='form-group'><input type='text' class='form-control tableWidth' id='templateID' placeholder='Template ID'></div></td><td class='tableTD'></td><td><div class='form-group'><input type='text' class='form-control tableNoOfItems' id='noOfItems' placeholder='No of Items'></div></td><td class='tableTD'></td><td><div class=.form-group'><select multiple class='form-control'><option>French</option><option>Italino</option><option>English</option><option>Japannese</option></select></div></td><td class='tableTD'></td><td><button type='submit' class='btn btn-primary remove'>Remove</button></td></tr>";
            jQuery('.tableItems').append(data);
        });
        jQuery('.remove').click(function (e) {
            e.preventDefault();
            jQuery('tr').remove('td');
        });
    </script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</body>
</html>
