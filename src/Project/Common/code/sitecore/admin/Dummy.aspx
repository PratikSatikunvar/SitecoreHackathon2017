<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dummy.aspx.cs" Inherits="Sitecore.Common.Website.sitecore.admin.Dummy" %>

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

            <div class="tab-content">
                <div class="tab-pane active" id="addItem">
                </div>
                <div class="tab-pane" id="updateItem">
                    <form id="form2" runat="server">
                        <table class="tableItems" style="margin-top: 30px;">
                            <tr>
                                <td>
                                    <div class="form-group">
                                        <label for="parentNode">
                                            <span>Parent Node/Parent Item Path</span>
                                            <asp:TextBox ID="txtDownloadParentID" runat="server" CssClass="form-control tableWidth"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqtxtDownloadParentID" runat="server" ControlToValidate="txtDownloadParentID"
                                                Display="Dynamic" ErrorMessage="required" SetFocusOnError="true" InitialValue="" ValidationGroup="Download"></asp:RequiredFieldValidator>
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
                    </form>

                    <table class="tableItems" style="margin-top: 30px;">
                        <tr>
                            <td>
                                <div class="form-group">
                                    <label for="uploadFile">
                                        <span>Select File For Upload</span>
                                        <input type="file" name="upload" class="form-control">
                                    </label>
                                </div>
                            </td>
                            <td class="tableTD"></td>
                            <td>
                                <div class="form-group">
                                    <label for="language">
                                        <span>Select Languages</span>
                                        <select class="form-control">
                                            <option>French</option>
                                            <option>Italino</option>
                                            <option>English</option>
                                            <option>Japannese</option>
                                        </select>
                                    </label>
                                </div>
                            </td>
                            <td class="tableTD"></td>
                            <td class="tableTD"></td>
                            <td>
                                <button type="submit" class="btn btn-primary">Upload</button>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div id="footer">
    </div>
    <script src="jquery-3.1.1.js"></script>
    <script>

        $('.add').click(function (e) {
            e.preventDefault();
            var data = "<tr><td><div class='form-group'><input type='text' class='form-control tableWidth' id='inputData' placeholder='Parent Node'></div></td><td class='tableTD'></td><td><div class='form-group'><input type='text' class='form-control tableWidth' id='templateID' placeholder='Template ID'></div></td><td class='tableTD'></td><td><div class='form-group'><input type='text' class='form-control tableNoOfItems' id='noOfItems' placeholder='No of Items'></div></td><td class='tableTD'></td><td><div class=.form-group'><select multiple class='form-control'><option>French</option><option>Italino</option><option>English</option><option>Japannese</option></select></div></td><td class='tableTD'></td><td><button type='submit' class='btn btn-primary remove'>Remove</button></td></tr>";
            $('.tableItems').append(data);
        });
        $('.remove').click(function (e) {
            e.preventDefault();
            $('tr').remove('td');
        });
    </script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</body>
</html>
