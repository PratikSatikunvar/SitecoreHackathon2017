<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BulkItemEditor.aspx.cs" Inherits="Sitecore.Common.Website.sitecore.admin.BulkItemEditor" EnableEventValidation="false" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <title>Bulk Item Editor</title>
    <style>
        fieldset {
            border-bottom: 1px solid grey;
            padding: 15px;
            margin-bottom: 15px;
        }
    </style>
    <!-- Bootstrap -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"
        integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">
</head>
<body>
    <div class="container">
        <h2>Bulk Item Editor</h2>
    </div>
    <div class="tab-content ">
        <div class="tab-pane active" id="1" style="padding-left: 20px;">
            <form id="form1" runat="server">
                <asp:LinkButton ID="lnkAddNewRow" CssClass="btn btn-default" runat="server" OnClick="lnkAddNewRow_Click">Add New Row</asp:LinkButton>
                <br />
                <br />
                <asp:Repeater ID="rpBulkItemEditor" runat="server" OnItemDataBound="rpBulkItemEditor_ItemDataBound">
                    <ItemTemplate>
                        <fieldset>
                            <div class="form-group">
                                <label for="parentNode">
                                    <span>Parent Node/Parent Item Path</span>
                                    <asp:TextBox ID="txtParentNode" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqParentNode" runat="server" ValidationGroup="BulkItem" ControlToValidate="txtParentNode" Display="Dynamic" ErrorMessage="required" InitialValue=""></asp:RequiredFieldValidator>
                                </label>
                                <br />
                                <label for="templateID">
                                    <span>Template Id/Template Path</span>
                                    <asp:TextBox ID="txtTemplateID" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqTemplateID" runat="server" ControlToValidate="txtTemplateID" ValidationGroup="BulkItem" Display="Dynamic" ErrorMessage="required" InitialValue=""></asp:RequiredFieldValidator>
                                </label>
                                <label for="noOfItems">
                                    <span>No of Items</span>
                                    <asp:TextBox ID="txtNoOfItems" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqNoOfItems" runat="server" ControlToValidate="txtNoOfItems" ValidationGroup="BulkItem" Display="Dynamic" ErrorMessage="required" InitialValue=""></asp:RequiredFieldValidator>
                                </label>
                                <br />
                                <label for="cblLanguage">
                                    <span>Languages</span>
                                    <asp:CheckBoxList ID="cblLanguage" runat="server">
                                    </asp:CheckBoxList>
                                </label>
                            </div>
                        </fieldset>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Button ID="btnSubmit" CssClass="btn btn-default" runat="server" Text="Create" OnClick="btnSubmit_Click" ValidationGroup="BulkItem" UseSubmitBehavior="true" />
            </form>
        </div>
        <div class="tab-pane" id="2">
        </div>
        <div class="tab-pane" id="3">
        </div>
    </div>
    <!-- Latest compiled and minified JavaScript -->
    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    <script>
        function validate() {
            if (Page_ClientValidate("LoginUserValidationGroup")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</body>
</html>
