using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EDGW.Logging;
using HtmlAgilityPack;
using OMCCore.Globalization;
using OMCCore.UI;
using OMCCore.Web;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace OMCCore.News
{

    public partial class NewsControlViewModel:ObservableObject
    {
        public NewsControlViewModel()
        {
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
            RefreshingTask = new Task(SyncImage, token);
            RefreshingTask.Start();
        }

        private void SyncImage()
        {
            logger.info("Image Sync Start");
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    logger.info("Image Sync Ended.");
                    return;
                }
                Thread.Sleep(10000);
                lock (News)
                {
                    Calc();
                }
            }
        }

        ~NewsControlViewModel()
        {
            logger.info("News Control VM Disposed");
            tokenSource.Cancel();
        }
        CancellationTokenSource tokenSource { get; }
        CancellationToken token { get; }
        public Task RefreshingTask { get; set; }
        public Logger logger = new Logger("News Engine", nameof(NewsControlViewModel));
        public ObservableCollection<New> News { get; } = new();
        public void Add(New @new)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                News.Add(@new);
            });
        }
        public void Clear()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                News.Clear();
            });
        }
        [ObservableProperty] bool isError = false;
        [ObservableProperty] bool isLoading = true;
        [ObservableProperty] string? errorMessage;
        [ObservableProperty] New? selected;
        int SelectedIndex = 0;
        [RelayCommand]
        public async Task RefreshAsync()
        {
            await Task.Run(Refresh);
        }

        public void Calc()
        {
            if (News.Count > 0)
            {
                SelectedIndex = (SelectedIndex + 1) % News.Count;
                Selected = News[SelectedIndex];
            }
        }
        public void Refresh()
        {
            lock (News)
            {
                try
                {
                    IsLoading = true;
                    IsError = false;
                    Clear();
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(WebHelper.GetData("https://www.mcbbs.net/"));
                    foreach (var node in doc.DocumentNode.SelectNodes("//div[@id=\"portal_left\"]//div[@id=\"portal_block_728_content\"]//div[@id=\"slideshow_3\"]/div[@class=\"slideshow_item\"]"))
                    {
                        HtmlDocument docx = new HtmlDocument();
                        docx.LoadHtml(node.OuterHtml);
                        var n = docx.DocumentNode.SelectSingleNode("//a");
                        string imguri = docx.DocumentNode.SelectSingleNode("//img")?.Attributes["src"]?.Value ?? "";
                        string text = n?.Attributes["title"]?.Value ?? "";
                        New @new = new New();
                        try
                        {
                            @new.ImgUri = new Uri(imguri);
                        }
                        catch (Exception ex)
                        {
                            logger.error($"Cannot parse img uri \"{imguri}\"", ex);
                        }
                        @new.Text = text;
                        Add(@new);
                    }
                    SelectedIndex = 0;
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Calc();
                    });
                }
                catch (Exception ex)
                {
                    IsError = true;
                    ErrorMessage = ex.Message;
                    logger.error("Cannot load news.", ex);
                }
                finally
                {
                    IsLoading = false;
                }
            }
        }
    }
}
