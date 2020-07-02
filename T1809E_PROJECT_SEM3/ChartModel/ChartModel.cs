using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace T1809E_PROJECT_SEM3.ChartModel
{
    public class ChartModel
    {
        [DataContract]
        public class DataPoint
        {
            public DataPoint(string label, double y)
            {
                this.Label = label;
                this.Y = y;
            }

            //Explicitly setting the name to be used while serializing to JSON.
            [DataMember(Name = "label")]
            public string Label = "";

            //Explicitly setting the name to be used while serializing to JSON.
            [DataMember(Name = "y")]
            public Nullable<double> Y = null;

        }

        [DataContract]
        public class DataPoint2
        {
            public DataPoint2(string label, double y)
            {
                this.Label = label;
                this.Y = y;
            }

            //Explicitly setting the name to be used while serializing to JSON.
            [DataMember(Name = "label")]
            public string Label = "";

            //Explicitly setting the name to be used while serializing to JSON.
            [DataMember(Name = "y")]
            public Nullable<double> Y = null;
        }
        [DataContract]
        public class DataPoint3
        {
            public DataPoint3(int label, double y)
            {
                this.Label = label;
                this.Y = y;
            }



            //Explicitly setting the name to be used while serializing to JSON.
            [DataMember(Name = "label")]
            public int Label;

            //Explicitly setting the name to be used while serializing to JSON.
            [DataMember(Name = "y")]
            public Nullable<double> Y = null;

        }

    }

}