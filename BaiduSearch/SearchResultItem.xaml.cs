using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// SearchResultItem.xaml 的交互逻辑
    /// </summary>
    public partial class SearchResultItem : UserControl
    {

        public SearchResultItem()
        {
            InitializeComponent();
        }

        public SearchResultItem(string title, string desc, string link) : this()
        {
            this.desc.Text = desc;
            this.link.Inlines.Add(new TextBlock() { Text = title });
            if (string.IsNullOrEmpty(link))
            {
                this.link.NavigateUri = null;
            }
            else
            {
                this.link.NavigateUri = new Uri(link);
            }
        }

        private void link_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            if (link.NavigateUri != null)
            {
                Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri));
            }
        }
    }
}
