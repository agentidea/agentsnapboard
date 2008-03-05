using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Xml;
using System.Xml.XPath;
using FlickrNet;

using AgentStoryComponents.core;

namespace AgentStoryComponents.extAPI
{
    public interface IContentProvider
    {
         void setDeveloperID(string DeveloperID);
         string getMediaMetaJSON(string UserName, bool includeEmbedHTML, int width, int height);
         List<mediaMeta> getMediaMeta(string UserName,bool includeEmbedHTML,int width,int height);
         string getThumbHTML(mediaMeta media, int width, int height);

    }

    public class YouTube : IContentProvider
    {

        private const int WIDTH_THUMB = 430;
        private const int HEIGHT_THUMB = 368;

        private string _developerID = null;

        private List<string> _urls;
        private List<string> urls
        {
            get { 
                if (_urls == null) _urls = new List<string>();
                return _urls;
            }
            
        }

        private void addUrl(string url)
        {
            this.urls.Add(url);
        }

        /// <summary>
        /// web browser client
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string getContents(string url)
        {
            System.Net.WebClient wc = new WebClient();
            UTF8Encoding objUTF8 = new UTF8Encoding();
            string output = objUTF8.GetString(wc.DownloadData(url));
            return output;
        }

        private System.Xml.XmlDocument getContentsAsDOM(string url)
        {
            string content = this.getContents(url);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(content);
            return doc;
        }

        protected string Format(double number, string mask)
        {
            string ret = "";
            ret = number.ToString(mask);
            return ret;
        }

        public string getVideoStatsHTML(string url,string user)
        {
            StringBuilder html = new StringBuilder();

            List<mediaMeta> metaVideos = 
                this.getVideos(url,false,0,0);


            html.Append("<br>");
            html.Append("<h3>");
            html.Append("<a href=\"http://youtube.com/profile?user=");
            html.Append(user);
            html.Append("\" target='_TOP'>");
            html.Append(user);
            html.Append("</a>");
            html.Append("</h3>");

            //lonelygirl15
            html.Append("<table width='100%' cellpadding='0' cellspacing='0'>");
            html.Append("<tr class='clsHeader' >");
            
            html.Append("<td>title</td>");
            html.Append("<td>views</td>");
            html.Append("<td>rating count</td>");
            html.Append("<td>rating avg</td>");
            html.Append("<td>comments</td>");
            html.Append("</tr>");

            int flip = 0;

            foreach(mediaMeta vm in metaVideos)
            {
                if (flip == 0)
                {
                    html.Append("<tr class='clsRowA'>");
                    flip = 1;
                }
                else
                {
                    html.Append("<tr class='clsRowB'>");
                    flip = 0;
                }


                html.Append("<td width='35%'>");
                html.Append("<a href=\"");
                html.Append("http://youtube.com/watch?v=");
                html.Append(vm.id);
                html.Append("\" target=\"_TOP\">");
                html.Append(vm.title);
                html.Append("</a>");
                html.Append("</td>");
                html.Append("<td align='right'>" + this.Format( vm.viewCount, "#,##") + "</td>");
                html.Append("<td align='right'>" + vm.ratingCount + "</td>");
                html.Append("<td align='right'>" + vm.ratingAvg + "</td>");
                html.Append("<td align='right'>" + this.Format( vm.commentCount, "#,##") + "</td>");
                html.Append("</tr>");
            }
            
            html.Append("</table>");
            

            return html.ToString();
        }

        private List<mediaMeta> getVideos(string url,bool includeEmbedData,int w,int h )
        {
            XmlDocument doc = this.getContentsAsDOM(url);
            List<mediaMeta> metaVideos = new List<mediaMeta>();

            XmlNodeList videos = doc.GetElementsByTagName("video");
            foreach (XmlNode video in videos)
            {
                mediaMeta vm = new mediaMeta();
                vm.id           = getStringVal("./id", video );
                vm.title        = getStringVal("./title", video);
                vm.viewCount    = getNumVal("./view_count" , video );
                vm.ratingAvg    = getNumVal("./rating_avg", video);
                vm.ratingCount  = getNumVal("./rating_count", video);
                vm.commentCount = getNumVal("./comment_count", video);
                vm.userCode     =  getStringVal("./author", video);
                if (includeEmbedData)
                    vm.embedData = this.getThumbHTML(vm, w, h);

                metaVideos.Add(vm);
            }

            return metaVideos;
        }

        private string getStringVal(string path, XmlNode node)
        {
            XmlNode tmp = node.SelectSingleNode(path);
            return Convert.ToString(tmp.FirstChild.Value);
        }
        private double getNumVal(string path, XmlNode node)
        {
            XmlNode tmp = node.SelectSingleNode(path);
            return Convert.ToDouble(tmp.FirstChild.Value);
        }

        private System.Data.DataSet getContentsAsDataset(string url)
        {
            string content = this.getContents(url);
            System.Data.DataSet ds = new System.Data.DataSet();
            System.IO.StringReader sr = new System.IO.StringReader(content);
            ds.ReadXml(sr);
            return ds;
        }


        #region IContentProvider Members

        public void setDeveloperID(string DeveloperID)
        {
            this._developerID = DeveloperID;
        }

        public List<mediaMeta> getMediaMeta(string UserName,bool includeEmbedHTML, int width , int height)
        {
            //"http://www.youtube.com/api2_rest?method=youtube.videos.list_by_user&dev_id=BQOPXj9u7UE&user=lonelygirl15"

            StringBuilder url = new StringBuilder();
            url.Append("http://www.youtube.com/api2_rest");
            url.Append("?");
            url.Append("method=youtube.videos.list_by_user");
            url.Append("&dev_id=");
            url.Append(this._developerID);
            url.Append("&user=");
            url.Append(UserName);

            return this.getVideos(url.ToString(),includeEmbedHTML,width,height);

        }



        public string getThumbHTML(mediaMeta media,int width,int height)
        {

            StringBuilder html = new StringBuilder();

            /* YouTube Embed HTML 
             
          <object width="425" height="350">
            <param name="movie" value="http://www.youtube.com/v/ZllfQZCc2_M"></param>
            <param name="wmode" value="transparent"></param>
            <embed src="http://www.youtube.com/v/ZllfQZCc2_M" type="application/x-shockwave-flash" wmode="transparent" width="425" height="350">
            </embed>
           </object>
           
             */

            string urlToMedia = "http://www.youtube.com/v/" + media.id;

            html.Append("<object");
            
            html.Append(" height=\"");
            html.Append(height);
            html.Append("\" ");
            html.Append(" width=\"");
            html.Append(width);
            html.Append("\" ");

            html.Append(">");

            html.Append("<param name=\"movie\" value=\"");
            html.Append(urlToMedia);
            html.Append("\"></param> ");

            html.Append("<param name=\"wmode\" value=\"");
            html.Append("transparent");
            html.Append("\"></param> ");

            html.Append("<embed src=\"");
            html.Append(urlToMedia);
            html.Append("\"");
            html.Append("type=\"application/x-shockwave-flash\" wmode=\"transparent\" >");
            html.Append("</embed>");
            html.Append("</object>");

            return html.ToString();

        }

        #endregion

        #region IContentProvider Members


        public string getMediaMetaJSON(string UserName, bool includeEmbedHTML, int width, int height)
        {
            List<mediaMeta> mm = this.getMediaMeta(UserName, includeEmbedHTML, width, height);
            StringBuilder json = new StringBuilder();


            json.Append("{");
            json.Append("'mediaItems':");
            json.Append("[");

            foreach (mediaMeta mediaItem in mm)
            {
                json.Append("{");

                json.Append("'");
                json.Append("id");
                json.Append("'");
                json.Append(":");
                json.Append("'");
                json.Append(TheUtils.ute.encode64( mediaItem.id ));
                json.Append("'");
                json.Append(",");

                json.Append("'");
                json.Append("title");
                json.Append("'");
                json.Append(":");
                json.Append("'");
                json.Append( TheUtils.ute.encode64( mediaItem.title ));
                json.Append("'");
                json.Append(",");
                json.Append("'");
                json.Append("userCode");
                json.Append("'");
                json.Append(":");
                json.Append("'");
                json.Append(TheUtils.ute.encode64(mediaItem.userCode));
                json.Append("'");

                //json.Append("'");
                //json.Append("embedHTML");
                //json.Append("'");
                //json.Append(":");
                //json.Append("'");
                //json.Append(TheUtils.ute.encode64(mediaItem.embedData));
                //json.Append("'");
                
                
                json.Append("}");
                json.Append(",");
            }

            if (mm.Count > 0) json.Remove(json.Length - 1, 1);
            
            json.Append("]");
            json.Append("}");

            return json.ToString();
        }

        #endregion
    }

    public class MyElements : IContentProvider
    {

        private const int WIDTH_THUMB = 430;
        private const int HEIGHT_THUMB = 368;

        private string _developerID = null;

        #region IContentProvider Members

        public void setDeveloperID(string DeveloperID)
        {
            this._developerID = DeveloperID;
        }
        public List<mediaMeta> getMediaMeta(string UserName, bool includeEmbedHTML, int width, int height)
        {
            return this.getImages(UserName, includeEmbedHTML, width, height);

        }
        public string getThumbHTML(mediaMeta media, int width, int height)
        {
            throw new NotImplementedException("no need to implement this");
            return "";
        }
        public string getMediaMetaJSON(string UserName, bool includeEmbedHTML, int width, int height)
        {
            List<mediaMeta> mm = this.getMediaMeta(UserName, includeEmbedHTML, width, height);
            StringBuilder json = new StringBuilder();


            json.Append("{");
            json.Append("'mediaItems':");
            json.Append("[");

            foreach (mediaMeta mediaItem in mm)
            {
                json.Append("{");

                json.Append("'");
                json.Append("id");
                json.Append("'");
                json.Append(":");
                json.Append("'");
                try
                {
                    json.Append(TheUtils.ute.encode64(mediaItem.id));
                }
                catch(Exception ex)
                {
                    //string msg = ex.Message;
                    json.Append("unknown");
                }

                json.Append("'");
                json.Append(",");

                json.Append("'");
                json.Append("title");
                json.Append("'");
                json.Append(":");
                json.Append("'");
                try
                {
                    json.Append(TheUtils.ute.encode64(mediaItem.title));
                }
                catch(Exception ex)
                {
                    json.Append("untitled");
                }
                json.Append("'");
                json.Append(",");
                json.Append("'");
                json.Append("userCode");
                json.Append("'");
                json.Append(":");
                json.Append("'");
                try
                {
                    json.Append(TheUtils.ute.encode64(mediaItem.userCode));
                }
                catch (Exception ex)
                {
                    json.Append("anon");
                }
                json.Append("'");

                json.Append(",");
                json.Append("'");
                json.Append("embedHTML");
                json.Append("'");
                json.Append(":");
                json.Append("'");

                try
                {
                    json.Append(mediaItem.embedData);
                }
                catch (Exception exp)
                {
                    json.Append(TheUtils.ute.encode64(exp.Message));
                }
                json.Append("'");


                json.Append("}");
                json.Append(",");
            }

            if (mm.Count > 0) json.Remove(json.Length - 1, 1);

            json.Append("]");
            json.Append("}");

            return json.ToString();
            
            
        }

        #endregion

        private List<mediaMeta> getImages(string userName, bool includeEmbedData, int w, int h)
        {
            List<mediaMeta> metaImages = new List<mediaMeta>();
            PageElements pageElems = new PageElements(config.conn);
            List<PageElement> pel = pageElems.getPageElements(userName,config.numPEperUser);

            foreach (PageElement pe in pel)
            {
                mediaMeta vm = new mediaMeta();
                vm.id = Convert.ToString( pe.ID );
                vm.title = pe.Tags;
                vm.userCode = pe.by.UserName;

                if (includeEmbedData)
                    vm.embedData = pe.Value;

                metaImages.Add(vm);
            }
            return metaImages;
        }
    }

    public class MyFlikr : IContentProvider
    {

        private string api_key = "f1f8238b3f2dad04e9953076f1e413d5";
        private string secret = "25842cae43c61d04";

        #region IContentProvider Members

  
        //refactor smell?
        public string getMediaMetaJSON(string UserName, bool includeEmbedHTML, int width, int height)
        {
            List<mediaMeta> mm = this.getMediaMeta(UserName, includeEmbedHTML, width, height);
            StringBuilder json = new StringBuilder();


            json.Append("{");
            json.Append("'mediaItems':");
            json.Append("[");

            foreach (mediaMeta mediaItem in mm)
            {
                json.Append("{");

                json.Append("'");
                json.Append("id");
                json.Append("'");
                json.Append(":");
                json.Append("'");
                try
                {
                    json.Append(TheUtils.ute.encode64(mediaItem.id));
                }
                catch (Exception ex)
                {
                    //string msg = ex.Message;
                    json.Append("unknown");
                }

                json.Append("'");
                json.Append(",");

                json.Append("'");
                json.Append("title");
                json.Append("'");
                json.Append(":");
                json.Append("'");
                try
                {
                    json.Append(TheUtils.ute.encode64(mediaItem.title));
                }
                catch (Exception ex)
                {
                    json.Append("untitled");
                }
                json.Append("'");
                json.Append(",");
                json.Append("'");
                json.Append("userCode");
                json.Append("'");
                json.Append(":");
                json.Append("'");
                try
                {
                    json.Append(TheUtils.ute.encode64(mediaItem.userCode));
                }
                catch (Exception ex)
                {
                    json.Append("anon");
                }
                json.Append("'");

                json.Append(",");
                json.Append("'");
                json.Append("embedHTML");
                json.Append("'");
                json.Append(":");
                json.Append("'");

                try
                {
                    json.Append(TheUtils.ute.encode64(mediaItem.embedData));
                }
                catch (Exception exp)
                {
                    json.Append(TheUtils.ute.encode64(exp.Message));
                }
                json.Append("'");


                json.Append("}");
                json.Append(",");
            }

            if (mm.Count > 0) json.Remove(json.Length - 1, 1);

            json.Append("]");
            json.Append("}");

            return json.ToString();


        }


        public List<mediaMeta> getMediaMeta(string UserName, bool includeEmbedHTML, int width, int height)
        {
            List<mediaMeta> mm = this.getImages(UserName, true, width, height);
            return mm;
        }

    
        #endregion

        #region unused interface methods
        public void setDeveloperID(string DeveloperID)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public string getThumbHTML(mediaMeta media, int width, int height)
        {
            return "";
        }


        #endregion

        public List<mediaMeta> getImages(string tags, bool includeEmbedData, int w, int h)
        {
            List<mediaMeta> metaVideos = new List<mediaMeta>();

            FlickrNet.Flickr flickr = new Flickr(api_key, secret);
            PhotoSearchOptions searchOptions = new PhotoSearchOptions();
            searchOptions.Tags = tags;

            Photos photosReturned = flickr.PhotosSearch(searchOptions);
            long totalPhotos = photosReturned.TotalPhotos;
            long pageSize = photosReturned.PhotosPerPage;

            
            int cursor = 0;

            foreach (FlickrNet.Photo photo in photosReturned.PhotoCollection)
            {
                if (cursor > config.numPEperUser) //prevent too many images from returning!
                    break;

                mediaMeta im = new mediaMeta();
                im.title = photo.Title;
                im.userCode = photo.UserId;

                //flickr.PeopleFindByUsername

                im.embedData = this.getThumbHTML(photo);
                metaVideos.Add(im);
                cursor++;

            }

            return metaVideos;
        }


        private string getPropegatorHTML( FlickrNet.Photo ph, string hyperlinktext, string url)
        {
            StringBuilder html = new StringBuilder();

            try
            {

                html.Append("<tr>");
                html.Append("<td>");

                string imageTag = @"<img src='";
                imageTag += url;
                imageTag += "'";
                imageTag += " />";
                imageTag += "<div>";
                imageTag += ph.Title;
                imageTag += "<br>";
                imageTag += ph.OwnerName;
                imageTag += "</div>";



                string content = "<div class=\"clsLibDropUrl\" onclick=\"";
                content += "storyView.dropElemIn64('" + TheUtils.ute.encode64(imageTag) + "', storyView.getOffsetCoord() , storyView.getTmpGUID() , null );\" >";
                content += hyperlinktext;
                content += "</div>";

                html.Append(content);
                //html.Append(hyperlinktext);
                html.Append("</td>");
                html.Append("</tr>");
            }
            catch (Exception ex)
            {
                html.Append("<tr>");
                html.Append("<td>");
                html.Append(ex.Message);
                html.Append("</td>");
                html.Append("</tr>");
            }

            return html.ToString();

        }
        private string getThumbHTML(FlickrNet.Photo ph)
        {
            StringBuilder html = new StringBuilder();

            html.Append("<table border=0 class='clsFlickrPanel'>");
            html.Append("<tr>");


            html.Append("<td>");    //small image
            html.Append("<!-- display image --> <img ");
            html.Append(" src='");
            html.Append( ph.SmallUrl );
            html.Append("'");
            html.Append(" />");
            html.Append("</td>");

            html.Append("<td><div class='clsActionLinks'>"); //action links

                html.Append("<table id='actionLinks' border=0>");

                try { html.Append(this.getPropegatorHTML(ph, "tiny", ph.ThumbnailUrl)); }      catch (Exception ex) {}
                try { html.Append(this.getPropegatorHTML(ph, "small", ph.ThumbnailUrl)); }     catch (Exception ex) {}
                try { html.Append(this.getPropegatorHTML(ph, "medium", ph.MediumUrl)); }       catch (Exception ex) {}
                try { html.Append(this.getPropegatorHTML(ph, "large", ph.LargeUrl)); }         catch (Exception ex) {}
                try { html.Append(this.getPropegatorHTML(ph, "original", ph.OriginalUrl)); }   catch (Exception ex) {}

                html.Append("</table>");


            html.Append("</div></td>");

            html.Append("</tr>");

           

            //html.Append("<div class='clsPhotoFlickrAutor'>");
            //html.Append(ph.UserId);
            //html.Append("</div>");
            html.Append("");
            html.Append("</table>");


            return html.ToString();
        }
    }
}
