using Sitecore.Feature.BulkItemEditor.Models;
using Sitecore.Feature.BulkItemEditor.API;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Sitecore.Globalization;
using Sitecore.Data.Managers;

namespace Sitecore.Common.Website.sitecore.admin
{
    public partial class BulkItemEditor : System.Web.UI.Page
    {
        #region Properties
        //Dummy Datasource to be able to add new section on "Add New Row" click.
        private List<int> Rows
        {
            get
            {
                if (ViewState["Rows"] == null)
                {
                    ViewState["Rows"] = new List<int>() { 1 };
                }
                return (List<int>)ViewState["Rows"];
            }
            set
            {
                ViewState["Rows"] = value;
            }
        }

        #endregion Properties

        #region Events
        /// <summary>
        /// Page load method. Languages dropdown and repeater getting bind.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBulkItemRepeater(); //Bind repeate. so, that one form section appears

                ddlDownloadLanguage.DataSource = GetLanguages(); //Language dropdown binding
                ddlDownloadLanguage.DataBind();
            }
        }

        /// <summary>
        /// Add new row event handler. adds a new form section to generate another set of items.
        /// </summary>
        protected void lnkAddNewRow_Click(object sender, EventArgs e)
        {
            var tempRows = (List<int>)ViewState["Rows"];
            tempRows.Add(0);
            Rows = tempRows;

            BindBulkItemRepeater();
        }

        /// <summary>
        /// Submit event handler. Responsible for creating items.
        /// </summary>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                foreach (RepeaterItem item in rpBulkItemEditor.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        var model = new ItemCreatorModel();

                        //Find controls from item in repeater
                        var ParentNode = (TextBox)item.FindControl("txtParentNode");
                        var TemplateID = (TextBox)item.FindControl("txtTemplateID");
                        var NoOfItems = (TextBox)item.FindControl("txtNoOfItems");
                        var Languages = (CheckBoxList)item.FindControl("cblLanguage");

                        //Set model values
                        model.NumberOfItems = System.Convert.ToInt32(NoOfItems.Text);
                        model.ParentNode = ParentNode.Text;
                        model.TemplateId = TemplateID.Text;
                        model.Languages = Languages.SelectedValue;

                        //create items in CMS
                        var _itemCreator = new ItemCreator();
                        _itemCreator.ItemGenerator(model);
                    }
                }
            }
        }

        /// <summary>
        /// Download data event handler. You can download the data of items which you want to edit/update.
        /// </summary>
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDownloadParentID.Text))
            {
                var _itemUpdater = new ItemUpdater();
                _itemUpdater.DownloadExcel(txtDownloadParentID.Text, ddlDownloadLanguage.SelectedValue);
            }
        }

        /// <summary>
        /// Event handler to bind language dropdown in repeater.
        /// </summary>
        protected void rpBulkItemEditor_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var cblLanguage = (CheckBoxList)e.Item.FindControl("cblLanguage");
            var languages = GetLanguages();
            foreach (ListItem item in languages)
            {
                cblLanguage.Items.Add(item);
            }
        }

        #endregion Events

        #region Methods

        /// <summary>
        /// Bind repeater to generate multiple forms.
        /// </summary>
        private void BindBulkItemRepeater()
        {
            rpBulkItemEditor.DataSource = Rows;
            rpBulkItemEditor.DataBind();
        }

        /// <summary>
        /// Get languages from the context site.
        /// </summary>
        private ListItemCollection GetLanguages()
        {
            ListItemCollection Languages = new ListItemCollection();
            Sitecore.Data.Database master = Sitecore.Data.Database.GetDatabase("master");
            var langVersions = new List<Language>();
            var installedLanguages = LanguageManager.GetLanguages(master);

            foreach (var language in installedLanguages)
            {
                Languages.Add(new ListItem() { Text = language.Name, Value = language.CultureInfo.TwoLetterISOLanguageName });
            }
            return Languages;
        }

        #endregion Methods
    }
}