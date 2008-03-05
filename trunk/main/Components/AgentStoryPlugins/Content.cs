using System;
using System.Collections.Generic;
using System.Text;

namespace AgentStoryPlugins
{
    public class mediaMeta
    {
        public int PK;
        public string id;
        public string title;
        public double viewCount;
        public double commentCount;
        public double ratingAvg;
        public double ratingCount;
        public string userCode;
        public mediaTypes type;
        public string embedData;
    }

    public enum mediaTypes : int
    {
        text = 1, 
        audio =2,
        video =3,
        image =4,
        random =5
    }




    

}
