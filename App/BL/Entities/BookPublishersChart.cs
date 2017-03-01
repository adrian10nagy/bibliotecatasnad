
namespace BL.Entities
{
    using System;
using System.Collections.Generic;

    public class BookPublishersChart
    {
        public List<string> Labels;
        public Dataset Dataset;
    }

    public class Dataset
    {
        public string[] Data;
        public string[] Percentage;
        public string[] BackgroundColor;
        public string[] SquareColors;
    }
}
