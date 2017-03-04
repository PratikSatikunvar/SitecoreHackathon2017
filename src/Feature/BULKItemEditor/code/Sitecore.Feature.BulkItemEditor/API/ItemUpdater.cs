using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Data;
using Sitecore.Globalization;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Web.Hosting;
using Sitecore.Data.Fields;
using Sitecore.Feature.BulkItemEditor.Models;
using Sitecore.Diagnostics;

namespace Sitecore.Feature.BulkItemEditor.API
{
    public class ItemUpdater
    {
        /// <summary>
        /// Download the item data which needed to be updated.
        /// </summary>
        /// <param name="parentItemId"> Id of the parent item whose children needed to fetch </param>
        /// <param name="language"> Language in which item's data needed to be downloaded </param>
        public void DownloadExcel(string parentItemId, string language)
        {

            try
            {
                Sitecore.Data.Database master = Sitecore.Data.Database.GetDatabase("master");

                Language languageItem;
                Language.TryParse(language, out languageItem);
                var parentItem = master.GetItem(parentItemId, languageItem);
                if (parentItem != null)
                {
                    HttpContext.Current.Response.ClearContent();
                    HttpContext.Current.Response.Buffer = true;
                    HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Customers.xls"));
                    HttpContext.Current.Response.ContentType = "application/ms-excel";

                    var children = parentItem.Children;
                    string fieldLabel = string.Empty, fieldValue = string.Empty;
                    foreach (Item item in children)
                    {
                        fieldLabel = fieldValue = string.Empty;
                        fieldLabel += "Item ID" + "\t" + "Item Name" + "\t";
                        fieldValue += item.ID.ToString() + "\t" + item.DisplayName + "\t";

                        item.Fields.ReadAll();
                        var fields = item.Fields.Where(d => !d.Shared && !d.Name.StartsWith("__") && d.Name.Trim() != "");
                        foreach (Field field in fields)
                        {
                            fieldLabel += field.Name + "\t";
                            fieldValue += item[field.Name] + "\t";
                        }

                        HttpContext.Current.Response.Write(fieldLabel + "\n");
                        HttpContext.Current.Response.Write(fieldValue + "\n");
                        HttpContext.Current.Response.Write("\n");
                    }

                    HttpContext.Current.Response.End();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error in method DownloadData. parentItemID: " + parentItemId + ", Language: " + language + ex.ToString(), ex);
            }
        }

        /// <summary>
        /// Create directory for given path if not exist.
        /// </summary>
        /// <param name="FilePath"> pPath of the file. </param>
        public void createDirectory(string FilePath)
        {
            try
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(FilePath);
                fileInfo.Directory.Create(); // If the directory already exists, this method does nothing.
            }
            catch (Exception ex)
            {
                Log.Error("Error in method createDirectory. FilePath: " + FilePath + ex.ToString(), ex);
            }
        }


        public string ItemUpdate(Dictionary<string, string> ItemInformation, string language)
        {
            StringBuilder log = new StringBuilder();
            try
            {
                log.Append("Item Creation Process Started\n");

                Sitecore.Data.Database masterDB = Sitecore.Data.Database.GetDatabase("master");

                //ID gu = new ID();

                //string itemId = "00000000-0000-0000-0000-000000000000";

                Item contextItem = masterDB.GetItem(ID.Parse(ItemInformation.Values.FirstOrDefault()), Language.Parse(language));
                using (new SecurityModel.SecurityDisabler())
                {
                    contextItem.Editing.BeginEdit();
                    foreach (var data in ItemInformation)
                    {
                        contextItem[data.Key] = data.Value;
                    }
                    contextItem.Editing.EndEdit();
                }
                log.Append(contextItem.Name + " Updated successfully with ID: " + contextItem.ID + "\n");

            }
            catch (Exception ex)
            {
                log.Append(ex.Message + "\n");
                log.Append(ex.StackTrace + "\n");
            }

            return log.ToString();
        }
    }
}