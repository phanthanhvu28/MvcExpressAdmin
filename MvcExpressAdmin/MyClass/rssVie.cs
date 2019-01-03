using HtmlAgilityPack;
using MvcExpressAdmin.Models;
using MvcExpressAdmin.MyClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace MvcExpressAdmin.MyClass
{

    public class rssVie
    {
        static string[] web = new string[] { "24h.com.vn", "vnexpress.net", "news.zing.vn", "tuoitre.vn", "dantri.com.vn", "thanhnien.vn",
                    "kenh14.vn", "ngoisao.net", "afamily.vn", "vietbao.vn", "tienphong.vn", "cafef.vn", "nld.com.vn", "giadinh.net.vn",
                    "laodong.vn", "vietnamnet.vn", "thethao247.vn", "vtv.vn", "tintuc.vn", "sggp.org.vn",
                    "bongdaplus.vn", "congly.vn", "soha.vn", "techz.vn","gamek.vn","ictnews.vn","ndh.vn","cafebiz.vn"};

     
        static NewsDataContext db = new NewsDataContext();
       
        public static async void GetRSS(string linkRss, int newspapermenuId, int manv)
        {
            try
            {                
                await Task.Run(() =>
                {
                    XmlDocument newsUrl = new XmlDocument();
                    newsUrl.Load(linkRss);
                    XDocument doc = XDocument.Parse(newsUrl.InnerXml);

                    var docs = doc.Root.Element("channel").ToString();
                    newsUrl.LoadXml(docs);
                    XmlNodeList idNodes = newsUrl.SelectNodes("channel/item");
                    int countnews = idNodes.Count > 40 ? 40 : idNodes.Count;
                    ClsData.countRequest += countnews;
                    for (int i = countnews - 1; i >= 0; i--)
                    {
                        string title_convert = MyTag.CvtChar(idNodes[i]["title"].InnerText);
                        string link_convert = MyTag.RemoveIllegalCharacters(idNodes[i]["link"].InnerText);
                        string Description = MyTag.RemoveHtml(MyTag.CvtChar(idNodes[i]["description"].InnerText));
                        string pubdate = idNodes[i]["pubDate"].InnerText;
                        string img = "";

                        if (link_convert.Contains("news.zing.vn") || link_convert.Contains("khoahoc.tv"))
                            img = idNodes[i]["enclosure"].Attributes["url"].InnerText;
                        else if (link_convert.Contains("bongdaplus.vn") || link_convert.Contains("xaluan.com"))
                            img = idNodes[i]["media:content"].Attributes["url"].InnerText;
                        else
                            img = MyTag.GetImg(idNodes[i]["description"].InnerText);
                        if (ClsData.CheckNewsSync(link_convert, newspapermenuId) == true)
                        {
                            string imgSave = link_convert.Contains("laodong.vn") ? (GetImageSave(link_convert) == "" ? img : "http://media.laodong.vn" + GetImageSave(link_convert)) : (GetImageSave(link_convert) == "" ? img : GetImageSave(link_convert));
                            string summary = MyTag.CvtChar(GetContent(link_convert));
                            if (summary.Length > 10)
                            {
                                string rssID = ClsData.RssIDSync();
                               // Conn.UpdateRowData("insert into rssNews(rssID,NewspaperMenuId,Title,IconRss,IconSave,Link,[Description],Summary,rssDate,DateInput,Effect,MaNV) values('" + rssID + "'," + newspapermenuId + ",N'" + MyTag.CvtChar(title_convert) + "','" + img + "','" + imgSave + "','" + link_convert + "',N'" + MyTag.CvtChar(Description) + "',N'" + MyTag.CvtChar(summary) + "','" + pubdate + "',getdate(),1," + manv + ")");
                                if (ClsData.InsertNewsSync(rssID, newspapermenuId, title_convert, img, img, link_convert, Description, summary, pubdate))
                                    ClsData.countRequestSuccess++;
                                else
                                    ClsData.countRequestDup++;
                            }
                            else
                            {
                                ClsData.countRequestFail++;
                            }
                        }
                        else
                        {
                            ClsData.countRequestDup++;
                        }
                    }
                });
            }
            catch { }
        }
        public static string GetImageSave(string link)
        {
            try
            {
                string content = GetContent(link);
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(content);
                var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//img").Attributes["src"].Value;
                return htmlBody;
            }
            catch { return ""; }
        }
        public static string ParseImage(string pHtml)
        {

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pHtml);
            var imgs = doc.DocumentNode.Descendants().Where(n => n.Name == "img" || n.Name == "image" || n.Name == "video" || n.Name == "table").ToList();

            if (imgs == null)
            {
                return pHtml;
            }
            else
            {
                foreach (var item in imgs)
                {

                    item.SetAttributeValue("class", "img-responsive");
                    item.SetAttributeValue("alt", "Myimg");
                }

                using (StringWriter tw = new StringWriter())
                {
                    doc.Save(tw);
                    return tw.ToString();
                }
            }
        }
        public static string GetContent(string link)
        {
            try
            {
                HtmlWeb webLink = new HtmlWeb();
                var htmlDoc = webLink.Load(link);

                string sKeyResult = web.FirstOrDefault<string>(s => link.Contains(s));

                switch (sKeyResult)
                {
                    #region 24h.com.vn

                    case "24h.com.vn":
                        var v1 = htmlDoc.DocumentNode.SelectSingleNode("//article[@class='nwsHt']");
                        if (v1 != null)
                        {
                            if (v1.SelectSingleNode("//header[@class='atclTit mrT10']") != null)
                                v1.SelectSingleNode("//header[@class='atclTit mrT10']").Remove();

                            if (v1.SelectSingleNode("//div[@class='updTm mrT5']") != null)
                                v1.SelectSingleNode("//div[@class='updTm mrT5']").Remove();

                            if (v1.SelectSingleNode("//div[@class='sbNws']") != null)
                                v1.SelectSingleNode("//div[@class='sbNws']").Remove();


                            if (v1.SelectSingleNode("//div[@class='flRt']") != null)
                                v1.SelectSingleNode("//div[@class='flRt']").Remove();


                            if (v1.SelectSingleNode("//div[@class='clear padTB5']") != null)
                                v1.SelectSingleNode("//div[@class='clear padTB5']").Remove();
                            if (v1.SelectSingleNode("//div[@class='baiviet-bailienquan']") != null)
                                v1.SelectSingleNode("//div[@class='baiviet-bailienquan']").Remove();
                            if (v1.SelectSingleNode("//div[@class='bv-lq']") != null)
                                v1.SelectSingleNode("//div[@class='bv-lq']").Remove();
                            v1.Descendants().Where(n => n.Name == "script" || n.Name == "iframe").ToList().ForEach(n => n.Remove());

                            HtmlNode viewVideoPlay = v1.SelectSingleNode("//div[@class='viewVideoPlay']");
                            if (viewVideoPlay != null)
                            {
                                HtmlNode node = v1.SelectSingleNode("//div[@class='zplayerDiv']");
                                if (node != null)
                                {
                                    string datadiv = node.Attributes["data-config"].Value;
                                    string linkvideo = MyTag.Between(datadiv, "&file=", "&fileAlt=");
                                    string lvi = linkvideo.Replace("***", ",");
                                    string[] lstlink = lvi.Split(',');
                                    string linkposter = MyTag.Between(datadiv, "&image=", "&autoReEnableMini=");
                                    HtmlNode video = htmlDoc.CreateElement("div");
                                    video.SetAttributeValue("class", "myvideos");
                                    viewVideoPlay.AppendChild(video);
                                    video.InnerHtml = "<video style=max-width:100%; poster=" + linkposter + "> <source src=" + lstlink[0] + " type=video/mp4></video>";
                                }
                                v1.SelectSingleNode("//div[@class='media-player']").Remove();
                            }

                            return ParseImage(MyTag.RemoveTagA(v1.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region news.zing.vn

                    case "news.zing.vn":
                        var v2 = htmlDoc.DocumentNode.SelectSingleNode("//*[@class='the-article-body cms-body']");
                        if (v2 != null)
                        {
                            if (v2.SelectSingleNode("//table[@class='article']") != null)
                                v2.SelectSingleNode("//table[@class='article']").Remove();

                            return ParseImage(MyTag.RemoveTagA(v2.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region dantri.com.vn
                    case "dantri.com.vn":
                        var v3 = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='divNewsContent']") == null ? htmlDoc.DocumentNode.SelectSingleNode("//div[@class='detail-content']") : htmlDoc.DocumentNode.SelectSingleNode("//*[@id='divNewsContent']");
                        if (v3 != null)
                        {
                            HtmlNode videoV3 = v3.SelectSingleNode("//div[@class='VCSortableInPreviewMode IMSCurrentEditorEditObject']");
                            if (videoV3 != null)
                            {
                                string datasrc = videoV3.Attributes["data-src"].Value;
                                string divid = videoV3.Attributes["id"].Value;
                                HtmlNode node = v3.SelectSingleNode("//div[@id='" + divid + "']");
                                string linkvideo = MyTag.Between(datasrc, "http://", ".mp4");
                                HtmlNode video = htmlDoc.CreateElement("div");
                                node.AppendChild(video);
                                video.InnerHtml = "<video style=max-width:100%;> <source src=" + "http://" + linkvideo + ".mp4 type=video/mp4></video>";
                                htmlDoc.DocumentNode.SelectSingleNode("//div[@class='VideoCMS_Caption']").Remove();
                                videoV3.Attributes["data-src"].Remove();
                            }
                            if (v3.SelectSingleNode("//div[@class='news-tag']") != null)
                                v3.SelectSingleNode("//div[@class='news-tag']").Remove();

                            return ParseImage(MyTag.RemoveTagA(v3.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region thanhnien.vn
                    case "thanhnien.vn":// chưa xử lý image large
                        var v4 = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='main_detail']");
                        if (v4 != null)
                        {
                            if (v4.SelectSingleNode("//div[@id='chapeau']") != null)
                                v4.SelectSingleNode("//div[@id='chapeau']").Remove();
                            if (v4.SelectSingleNode("//div[@id='relatednews']") != null)
                                v4.SelectSingleNode("//div[@id='relatednews']").Remove();
                            if (v4.SelectSingleNode("//article[@class='story clearfix']") != null)
                                v4.SelectSingleNode("//article[@class='story clearfix']").Remove();

                            if (v4.SelectSingleNode("//article[@class='story story--left clearfix']") != null)
                                v4.SelectSingleNode("//article[@class='story story--left clearfix']").Remove();

                            if (v4.SelectSingleNode("//div[@class='details__morenews']") != null)
                                v4.SelectSingleNode("//div[@class='details__morenews']").Remove();

                            if (v4.SelectSingleNode("//table[@class='quote quote--grey']") != null)
                                v4.SelectSingleNode("//table[@class='quote quote--grey']").Remove();


                            if (v4.SelectSingleNode("//div[@id='AdAsia']") != null)
                                v4.SelectSingleNode("//div[@id='AdAsia']").Remove();
                            if (v4.SelectSingleNode("//div[@id='adsctl00_main_AdsHome18']") != null)
                                v4.SelectSingleNode("//div[@id='adsctl00_main_AdsHome18']").Remove();

                            string img = htmlDoc.DocumentNode.SelectSingleNode("//img[@class='storyavatar']") != null ? "<div><img src=" + htmlDoc.DocumentNode.SelectSingleNode("//img[@class='storyavatar']").Attributes["src"].Value + " /></div>" : "";

                            return img + ParseImage(MyTag.RemoveTagA(v4.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region kenh14.vn
                    case "kenh14.vn":
                        var v5 = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='knc-content']");
                        if (v5 != null)
                        {
                            return ParseImage(MyTag.RemoveTagA(v5.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region tuoitre.vn
                    case "tuoitre.vn":
                        var v6 = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='fck']");
                        if (v6 != null)
                        {
                            var result = v6.SelectNodes("//div[@class='VCSortableInPreviewMode']");
                            if (result != null)
                            {
                                foreach (var r in result)
                                {
                                    r.Remove();
                                }
                            }

                            return ParseImage(MyTag.RemoveTagA(v6.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region ngoisao.net
                    case "ngoisao.net":
                        var v7 = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='fck_detail width_common block_ads_connect']");
                        if (v7 != null)
                        {
                            return ParseImage(MyTag.RemoveTagA(v7.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region afamily.vn
                    case "afamily.vn":
                        //var v8 = htmlDoc.DocumentNode.SelectSingleNode("//*[@class='detail_content fl mgt15']");
                        var v8 = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='afcbc-body vceditor-content']");
                        if (v8 != null)
                        {
                            return ParseImage(MyTag.RemoveTagA(v8.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region vietbao.vn
                    case "vietbao.vn":
                        var v9 = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='vb-content-detailbox']");
                        if (v9 != null)
                        {
                            var r9 = v9.SelectNodes("//div[@class='module mod-tlq-detail block-1']");
                            if (r9 != null)
                            {
                                foreach (var r in r9)
                                {
                                    r.Remove();
                                }
                            }
                            return ParseImage(MyTag.RemoveTagA(v9.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region tienphong.vn
                    case "tienphong.vn":
                        var v10 = htmlDoc.DocumentNode.SelectSingleNode("//*[@class='article-col-b']");
                        if (v10 != null)
                        {
                            if (v10.SelectSingleNode("//div[@class='article-relate-b']") != null)
                                v10.SelectSingleNode("//div[@class='article-relate-b']").Remove();


                            if (v10.SelectSingleNode("//div[@class='fyi fyi-mobile mb10']") != null)
                                v10.SelectSingleNode("//div[@class='fyi fyi-mobile mb10']").Remove();

                            if (v10.SelectSingleNode("//div[@class='banner fyi fyi--InnerArticle']") != null)
                                v10.SelectSingleNode("//div[@class='banner fyi fyi--InnerArticle']").Remove();
                            if (v10.SelectSingleNode("//div[@class='article-pin-social fixed']") != null)
                                v10.SelectSingleNode("//div[@class='article-pin-social fixed']").Remove();
                            if (v10.SelectSingleNode("//div[@class='article-orginal-social']") != null)
                                v10.SelectSingleNode("//div[@class='article-orginal-social']").Remove();
                            if (v10.SelectSingleNode("//div[@class='fyi mt10']") != null)
                                v10.SelectSingleNode("//div[@class='fyi mt10']").Remove();
                            if (v10.SelectSingleNode("//div[@class='article-hot-video']") != null)
                                v10.SelectSingleNode("//div[@class='article-hot-video']").Remove();

                            if (v10.SelectSingleNode("//div[@class='article-pin-social']") != null)
                                v10.SelectSingleNode("//div[@class='article-pin-social']").Remove();
                            return ParseImage(MyTag.RemoveTagA(v10.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region cafef.vn
                    case "cafef.vn":
                        var v11 = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='contentdetail']");
                        if (v11 != null)
                        {
                            if (v11.SelectSingleNode("//div[@class='chisochungkhoan']") != null)
                                v11.SelectSingleNode("//div[@class='chisochungkhoan']").Remove();

                            var r11 = v11.SelectNodes("//div[@class='VCSortableInPreviewMode link-content-footer']");
                            if (r11 != null)
                            {
                                foreach (var r in r11)
                                {
                                    r.Remove();
                                }
                            }

                            if (v11.SelectSingleNode("//div[@class='tindnd clearfix']") != null)
                                v11.SelectSingleNode("//div[@class='tindnd clearfix']").Remove();

                            return ParseImage(MyTag.RemoveTagA(v11.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region nld.com.vn
                    case "nld.com.vn":
                        var v12 = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='divNewsContent']") == null ? htmlDoc.DocumentNode.SelectSingleNode("//div[@class='contentdetail clearall']") : htmlDoc.DocumentNode.SelectSingleNode("//div[@id='divNewsContent']");
                        if (v12 != null)
                        {
                            if (v12.SelectSingleNode("//div[@class='tinlienquanold displaynone']") != null)
                                v12.SelectSingleNode("//div[@class='tinlienquanold displaynone']").Remove();
                            if (v12.SelectSingleNode("//div[@class='tlqdetail displaynone']") != null)
                                v12.SelectSingleNode("//div[@class='tlqdetail displaynone']").Remove();
                            if (v12.SelectSingleNode("//div[@id='LoadTinLQRight']") != null)
                                v12.SelectSingleNode("//div[@id='LoadTinLQRight']").Remove();
                            return ParseImage(MyTag.RemoveTagA(v12.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region giadinh.net.vn
                    case "giadinh.net.vn":
                        var v13 = htmlDoc.DocumentNode.SelectSingleNode("//*[@class='content-new clear']");
                        if (v13 != null)
                        {
                            return ParseImage(MyTag.RemoveTagA(v13.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region laodong.vn
                    case "laodong.vn"://khong có icon(xem lai)
                        var v14 = htmlDoc.DocumentNode.SelectSingleNode("//*[@class='article-content']");
                        if (v14 != null)
                        {
                            return ParseImage(MyTag.RemoveTagA(v14.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region vietnamnet.vn
                    case "vietnamnet.vn":

                        var v15 = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='ArticleContent']");
                        if (v15 != null)
                        {
                            if (v15.SelectSingleNode("//span[@class='bold']") != null)
                                v15.SelectSingleNode("//span[@class='bold']").Remove();

                            if (v15.SelectSingleNode("//div[@class='article-relate']") != null)
                                v15.SelectSingleNode("//div[@class='article-relate']").Remove();

                            var v152 = v15.SelectNodes("//div[@class='inner-article']");
                            if (v152 != null)
                            {
                                foreach (var r in v152)
                                {
                                    r.Remove();
                                }
                            }

                            return ParseImage(MyTag.RemoveTagA(v15.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region thethao247.vn
                    case "thethao247.vn":
                        var v16 = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='main-detail']");
                        if (v16 != null)
                        {
                            if (v16.SelectSingleNode("//div[@id='add']") != null)
                                v16.SelectSingleNode("//div[@id='add']").Remove();

                            return ParseImage(MyTag.RemoveTagA(v16.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region vtv.vn
                    case "vtv.vn":
                        var v18 = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='ta-justify']") == null ? htmlDoc.DocumentNode.SelectSingleNode("//div[@class='content_detail ta-justify']") : htmlDoc.DocumentNode.SelectSingleNode("//div[@class='ta-justify']");
                        if (v18 != null)
                        {
                            if (v18.SelectSingleNode("//div[@class='VCSortableInPreviewMode active']") != null)
                                v18.SelectSingleNode("//div[@class='VCSortableInPreviewMode active']").Remove();
                            return ParseImage(MyTag.RemoveTagA(v18.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region tintuc.vn
                    case "tintuc.vn":
                        var v19 = htmlDoc.DocumentNode.SelectSingleNode("//[@id='articleContent']");
                        if (v19 != null)
                        {
                            if (v19.SelectSingleNode("//[@id='ads_end_content']") != null)
                                v19.SelectSingleNode("//[@id='ads_end_content']").Remove();
                            if (v19.SelectSingleNode("//[@class='boxinfo mg-b-10']") != null)
                                v19.SelectSingleNode("//[@class='boxinfo mg-b-10']").Remove();

                            return ParseImage(MyTag.RemoveTagA(v19.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region sggp.org.vn
                    case "sggp.org.vn":
                        var v20 = htmlDoc.DocumentNode.SelectSingleNode("//*[@class='content cms-body']");
                        if (v20 != null)
                        {
                            return ParseImage(MyTag.RemoveTagA(v20.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region bongdaplus.vn
                    case "bongdaplus.vn":
                        var v21 = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='content']");
                        if (v21 != null)
                        {
                            v21.Descendants().Where(n => n.Name == "script").ToList().ForEach(n => n.Remove());
                            if (v21.SelectSingleNode("//*[@class='pollview']") != null)
                                v21.SelectSingleNode("//*[@class='pollview']").Remove();
                            if (v21.SelectSingleNode("//*[@class='fbshr inpage']") != null)
                                v21.SelectSingleNode("//*[@class='fbshr inpage']").Remove();
                            HtmlNode embedbox = v21.SelectSingleNode("//table[@class='embedbox']");
                            if (embedbox != null)
                            {
                                HtmlNode node = embedbox.SelectSingleNode("//td[@class='embitem' and @embobj='video']");
                                if (node != null)
                                {
                                    node.SelectSingleNode("//td[@embobj='video']//div").Remove();
                                    var vnode = node.SelectNodes("//td[@class='embitem']");
                                    // string src = "";
                                    //foreach (var n in vnode)
                                    //{
                                    //    src = n.SelectSingleNode("//td[@class='embitem']").GetAttributeValue("embid", "default");

                                    string src = node.SelectSingleNode("//td[@class='embitem']").GetAttributeValue("embid", "default");
                                    HtmlNode div = htmlDoc.CreateElement("div");
                                    node.AppendChild(div);

                                    div.InnerHtml = "<iframe src='http://video.bongdaplus.vn/media-box/video/" + src + ".bdmedia' style='width: 100%;height: 270px;' scrolling='no' frameborder='0' marginwidth='0'></iframe>";
                                    // }
                                }
                            }
                            return ParseImage(MyTag.RemoveTagA(v21.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region congly.vn
                    case "congly.vn":
                        var v22 = htmlDoc.DocumentNode.SelectSingleNode("//*[@class='news-dscontent']");
                        if (v22 != null)
                        {
                            return ParseImage(MyTag.RemoveTagA(v22.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region soha.vn
                    case "soha.vn":
                        var v23 = htmlDoc.DocumentNode.SelectSingleNode("//*[@class='detail-body']");
                        var v232 = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='entry-body']");
                        if (v23 != null)
                        {
                            if (v23.SelectSingleNode("//*[@class='related-news']") != null)
                                v23.SelectSingleNode("//*[@class='related-news']").Remove();

                            return ParseImage(MyTag.RemoveTagA(v23.OuterHtml));
                        }
                        else if (v232 != null)
                        {
                            if (v232.SelectSingleNode("//div[@class='VCSortableInPreviewMode alignRight active']") != null)
                                v232.SelectSingleNode("//div[@class='VCSortableInPreviewMode alignRight active']").Remove();
                            if (v232.SelectSingleNode("//div[@class='VCSortableInPreviewMode link-content-footer IMSCurrentEditorEditObject']") != null)
                                v232.SelectSingleNode("//div[@class='VCSortableInPreviewMode link-content-footer IMSCurrentEditorEditObject']").Remove();

                            return ParseImage(MyTag.RemoveTagA(v232.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region techz.vn

                    case "techz.vn":
                        var v24 = htmlDoc.DocumentNode.SelectSingleNode("//*[@class='detail-news']") == null ? htmlDoc.DocumentNode.SelectSingleNode("//*[@class='detail-entry']") : htmlDoc.DocumentNode.SelectSingleNode("//*[@class='detail-news']");
                        if (v24 != null)
                        {
                            if (v24.SelectSingleNode("//div[@id='htv-if1']") != null)
                            {
                                v24.SelectSingleNode("//div[@id='htv-if1']").Remove();
                            }
                            return ParseImage(MyTag.RemoveTagA(v24.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region vnexpress.net
                    case "vnexpress.net":
                        var v25 = htmlDoc.DocumentNode.SelectSingleNode("//*[@class='content_detail fck_detail width_common block_ads_connect']");
                        if (v25 != null)
                        {
                            if (htmlDoc.DocumentNode.SelectSingleNode("//*[@class='block_filter_live width_common mb10']") != null)
                                htmlDoc.DocumentNode.SelectSingleNode("//*[@class='block_filter_live width_common mb10']").Remove();
                            //v25 = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='container_tab_live']");

                            //if (v25 == null)
                            //{
                            //    v25 = htmlDoc.DocumentNode.SelectSingleNode("//*[@class='content_detail fck_detail width_common']");
                            //    if (v25 == null)
                            //    {
                            //        v25 = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='player_playing']");
                            //    }
                            //}

                            // return ParseImage(MyTag.RemoveTagA(v25.OuterHtml));
                        }
                        //else
                        //{
                        //    var n25 = v25.SelectSingleNode("//div[@class='box_embed_video_parent embed_video_new']");
                        //    if (n25 != null)
                        //    {
                        //        string s25 = n25.SelectSingleNode("//div[@class='box_embed_video_parent embed_video_new']").Attributes["data-vid"].Value;
                        //        string img25 = v25.SelectSingleNode("//div[@data-vid='" + s25 + "']//img").Attributes["src"].Value;
                        //        var src25 = n25.SelectSingleNode("//video[@id='media-video-" + s25 + "']").Attributes["src"].Value;
                        //        HtmlNode div = htmlDoc.CreateElement("div");
                        //        div.SetAttributeValue("class", "myvideos");
                        //        n25.AppendChild(div);
                        //        div.InnerHtml = "<video style=max-width:100%; poster=" + img25 + "> <source  src=" + src25 + " type=video/mp4></video>";

                        //        n25.SelectSingleNode("//div[@class='box_img_video embed-container']").Remove();

                        //    }
                        //}
                        return ParseImage(MyTag.RemoveTagA(v25.OuterHtml));
                    #endregion
                    #region gamek.vn
                    case "gamek.vn":
                        var v26 = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='rightdetail_content']");
                        if (v26 != null)
                        {
                            var r26 = v26.SelectNodes("//div[@class='VCSortableInPreviewMode link-content-footer']");
                            if (r26 != null)
                            {
                                foreach (var r in r26)
                                {
                                    r.Remove();
                                }
                            }
                            return ParseImage(MyTag.RemoveTagA(v26.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region ictnews.vn
                    case "ictnews.vn":

                        var v27 = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='content-detail']");

                        if (v27 != null)
                        {
                            v27.Descendants().Where(n => n.Name == "script").ToList().ForEach(n => n.Remove());
                            return ParseImage(MyTag.RemoveTagA(v27.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region ndh.vn
                    case "ndh.vn":
                        var v28 = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='main-detail']") == null ? htmlDoc.DocumentNode.SelectSingleNode("//div[@class='detail']") : htmlDoc.DocumentNode.SelectSingleNode("//div[@class='main-detail']");
                        if (v28 != null)
                        {
                            if (v28.SelectSingleNode("//div[@class='most-detail']") != null)
                                v28.SelectSingleNode("//div[@class='most-detail']").Remove();

                            return ParseImage(MyTag.RemoveTagA(v28.OuterHtml));
                        }
                        return "";
                    #endregion
                    #region cafebiz.vn
                    case "cafebiz.vn":
                        var v29 = htmlDoc.DocumentNode.SelectSingleNode("//*[@class='detail-content']");
                        if (v29 != null)
                        {
                            var r29 = v29.SelectNodes("//*[@class='VCSortableInPreviewMode link-content-footer IMSCurrentEditorEditObject']");
                            if (r29 != null)
                            {
                                foreach (var r in r29)
                                {
                                    r.Remove();
                                }
                            }
                            var r291 = v29.SelectNodes("//*[@class='VCSortableInPreviewMode link-content-footer IMSCurrentEditorEditObject active']");
                            if (r291 != null)
                            {
                                foreach (var r in r291)
                                {
                                    r.Remove();
                                }
                            }

                            return ParseImage(MyTag.RemoveTagA(v29.OuterHtml));
                        }
                        return "";
                    #endregion
                    default:
                        return "";
                }
            }
            catch
            {

                return "";
            }
        }

    }
}