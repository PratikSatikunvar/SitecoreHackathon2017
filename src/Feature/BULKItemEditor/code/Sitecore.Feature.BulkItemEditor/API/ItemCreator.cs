using Sitecore.Feature.BulkItemEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Data;
using Sitecore.Globalization;
using System.Text;

namespace Sitecore.Feature.BulkItemEditor.API
{
    public class ItemCreator
    {
        /// <summary>
        /// Method to generate the items as per input in the CMS.
        /// </summary>
        /// <param name="ItemCreatorModel"> model containing parent item Id/Path, Template Id/Path, Languages, no. of items to create etc. </param>
        public string ItemGenerator(ItemCreatorModel model)
        {
            StringBuilder log = new StringBuilder();
            try
            {
                log.Append("Item Creation Process Started\n");
                    
                for (int i = 0; i < model.NumberOfItems; i++)
                {
                    Sitecore.Data.Database masterDB = Sitecore.Data.Database.GetDatabase("master");

                    #region Item Creation
                    bool IsParentId = ID.IsID(model.ParentNode);
                    Item parentItem;
                    if (!IsParentId)
                    {
                        parentItem = masterDB.GetItem(model.ParentNode);
                    }
                    else
                    {
                        parentItem = masterDB.GetItem(ID.Parse(model.ParentNode));
                    }
                    TemplateItem template = masterDB.GetTemplate(model.TemplateId);
                    #endregion End Item Creation
                    
                    Item newItem = parentItem.Add("Item-" + i, template);

                    #region Adding Language Versions

                    var languageCollection = model.Languages.Split(',');

                    foreach (string languageCode in languageCollection)
                    {
                        using (new LanguageSwitcher(languageCode))
                        {
                            Item parent = masterDB.GetItem(newItem.ParentID, Language.Parse(languageCode));

                            Item NewItem = parent.GetChildren().Where(x => x.ID == newItem.ID).FirstOrDefault();

                            NewItem.Editing.BeginEdit();

                            NewItem.Versions.AddVersion();

                            NewItem.Editing.EndEdit();
                        }
                    }

                    log.Append(newItem.Name+ " Created successfully with ID: "+ newItem.ID+ "\n");
                    #endregion
                }
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