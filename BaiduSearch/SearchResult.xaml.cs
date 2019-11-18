using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BaiduSearch
{
    /// <summary>
    /// SearchResult.xaml 的交互逻辑
    /// </summary>
    public partial class SearchResult : UserControl
    {
        /// <summary>
        /// 搜索结果dom
        /// </summary>
        public IDocument Document { get; set; }

        public SearchResult()
        {
            InitializeComponent();
        }

        public void reload()
        {
            if (Document == null)
            {
                return;
            }

            searchResultList.Items.Clear();

            var results = Document.GetElementsByClassName("result c-container");

            foreach (var item in results)
            {
                if (item is IHtmlDivElement)
                {
                    try
                    {
                        var div = item as IHtmlDivElement;
                        var title = div.FirstChild.TextContent;
                        var desc = div.GetElementsByClassName("c-abstract").First().TextContent;
                        var link = div.LastChild.FirstChild as IHtmlAnchorElement;

                        var searchResultItem = new SearchResultItem(title, desc, link == null ? string.Empty : link.Href);
                        searchResultList.Items.Add(searchResultItem);
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }
    }
}
