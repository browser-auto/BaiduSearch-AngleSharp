using AngleSharp;
using AngleSharp.Dom.Events;
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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string baiduUrl = "http://www.baidu.com";

        /// <summary>
        /// 搜索框页面
        /// </summary>
        private HomePage homePage = new HomePage();

        /// <summary>
        /// 搜索结果列表
        /// </summary>
        private SearchResult searchResult = new SearchResult();

        private IConfiguration config = null;
        private IBrowsingContext context = null;


        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;

            this.homePage.su.Click += Su_Click;
        }

        /// <summary>
        /// 【百度一下】按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Su_Click(object sender, RoutedEventArgs e)
        {
            string keyword = this.homePage.wd.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                return;
            }

            var document = await context.OpenAsync(baiduUrl);

            var input = document.GetElementById("kw") as IHtmlInputElement;
            input.TextContent = keyword;
            input.Value = keyword;

            var form = document.GetElementById("form") as IHtmlFormElement;
            document = await form.SubmitAsync();
            if (document != null)
            {
                //搜索结果展示
                searchResult.Document = document;
                this.mainContent.Content = searchResult;
                searchResult.reload();
            }
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.mainContent.Content = homePage;

            config = Configuration.Default
                .WithCss()
                .WithJs()
                .WithDefaultCookies()
                .WithDefaultLoader();

            context = BrowsingContext.New(config);

        }
    }
}
