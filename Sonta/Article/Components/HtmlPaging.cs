using DotNetNuke.Web.Mvc.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Article.Components
{
    public static class HtmlPaging
    {
        public static MvcHtmlString BootstrapPager(this DnnHtmlHelper helper, int currentPageIndex, int totalItems, int pageSize = 10, int numberOfLinks = 5)
        {
            if (currentPageIndex <= 0)
                currentPageIndex = 1;
            if (totalItems <= 0)
            {
                return MvcHtmlString.Empty;
            }
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var lastPageNumber = (int)Math.Ceiling((double)currentPageIndex / numberOfLinks) * numberOfLinks;
            var firstPageNumber = lastPageNumber - (numberOfLinks - 1);
            var hasPreviousPage = currentPageIndex > 1;
            var hasNextPage = currentPageIndex < totalPages;
            if (lastPageNumber > totalPages)
            {
                lastPageNumber = totalPages;
            }

            var div = new TagBuilder("div");
            div.Attributes["style"] = "display:flex;align-items:center";

            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination flex-wrap");
            ul.InnerHtml += AddLink(1, currentPageIndex == 1, "disabled", "<<", "First Page");
            ul.InnerHtml += AddLink(currentPageIndex - 1, !hasPreviousPage, "disabled", "<", "Previous Page");
            for (int i = firstPageNumber; i <= lastPageNumber; i++)
            {
                ul.InnerHtml += AddLink(i, i == currentPageIndex, "active", i.ToString(), i.ToString());
            }
            ul.InnerHtml += AddLink(currentPageIndex + 1, !hasNextPage, "disabled", ">", "Next Page");
            ul.InnerHtml += AddLink(totalPages, currentPageIndex == totalPages, "disabled", ">>", "Last Page");

            var span = new TagBuilder("span");
            span.Attributes["style"] = "margin-left: 20px";
            span.InnerHtml = "Total: " + totalItems;

            div.InnerHtml += ul;
            div.InnerHtml += span;

            return MvcHtmlString.Create(div.ToString());
        }

        private static TagBuilder AddLink(int index, bool condition, string classToAdd, string linkText, string tooltip)
        {
            var li = new TagBuilder("li");
            li.AddCssClass("page-item");
            li.MergeAttribute("title", tooltip);
            if (condition)
            {
                li.AddCssClass(classToAdd);
            }
            var a = new TagBuilder("a");
            a.Attributes["style"] = "cursor: pointer;";
            a.AddCssClass("page-link");
            a.MergeAttribute("onClick", "PagingAction(" + index + ")");
            a.SetInnerText(linkText);
            li.InnerHtml = a.ToString();
            return li;
        }
    }
}