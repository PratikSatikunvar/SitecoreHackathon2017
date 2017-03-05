﻿namespace Sitecore.Foundation.Assets.Pipelines.GetPageRendering
{
    using System.Collections.Generic;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Foundation.Assets.Models;
    using Sitecore.Foundation.Assets.Repositories;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Mvc.Pipelines.Response.GetPageRendering;
    using Sitecore.Mvc.Presentation;

    public class AddRenderingAssets : GetPageRenderingProcessor
    {
        public override void Process(GetPageRenderingArgs args)
        {
            this.AddAssets(args.PageContext.PageDefinition.Renderings);
        }

        private void AddAssets(IEnumerable<Rendering> renderings)
        {
            foreach (var rendering in renderings)
            {
                var renderingItem = this.GetRenderingItem(rendering);
                if (renderingItem == null)
                {
                    return;
                }

                AddAssetsFromItem(renderingItem);
            }
        }

        protected static void AddAssetsFromItem(Item renderingItem)
        {
            if (!renderingItem.IsDerived(Templates.RenderingAssets.ID))
                return;
            AddScriptAssetsFromRendering(renderingItem);
            AddInlineScriptFromRendering(renderingItem);
            AddStylingAssetsFromRendering(renderingItem);
            AddInlineStylingFromAssets(renderingItem);
        }

        private static void AddInlineStylingFromAssets(Item renderingItem)
        {
            var cssInline = renderingItem[Templates.RenderingAssets.Fields.InlineStyling];
            if (!string.IsNullOrEmpty(cssInline))
            {
                AssetRepository.Current.AddStyling(cssInline, renderingItem.ID.ToString(), true);
            }
        }

        private static void AddStylingAssetsFromRendering(Item renderingItem)
        {
            var cssAssets = renderingItem[Templates.RenderingAssets.Fields.StylingFiles];
            foreach (var cssAsset in cssAssets.Split(';', ',', '\n'))
            {
                AssetRepository.Current.AddStyling(cssAsset, true);
            }
        }

        private static void AddInlineScriptFromRendering(Item renderingItem)
        {
            var javaScriptInline = renderingItem[Templates.RenderingAssets.Fields.InlineScript];
            if (!string.IsNullOrEmpty(javaScriptInline))
            {
                AssetRepository.Current.AddScript(javaScriptInline, renderingItem.ID.ToString(), ScriptLocation.Body, true);
            }
        }

        private static void AddScriptAssetsFromRendering(Item renderingItem)
        {
            var javaScriptAssets = renderingItem[Templates.RenderingAssets.Fields.ScriptFiles];
            foreach (var javaScriptAsset in javaScriptAssets.Split(';', ',', '\n'))
            {
                AssetRepository.Current.AddScript(javaScriptAsset, true);
            }
        }

        private Item GetRenderingItem(Rendering rendering)
        {
            if (rendering.RenderingItem == null)
            {
                Log.Warn($"rendering.RenderingItem is null for {rendering.RenderingItemPath}", this);
                return null;
            }

            if (Context.PageMode.IsNormal && rendering.Caching.Cacheable)
            {
                AssetRepository.Current.Add(rendering.RenderingItem.ID);
            }
            return rendering.RenderingItem.InnerItem;
        }
    }
}