using System;
using System.Collections.Generic;
using System.IO;
using CJEngine.Models.Join_Entities;

namespace CJEngine.Models
{
    public class Algorithm
    {
        public int Id { get; set; }
        public string FunctionName { get; set; }
        public string Filename { get; set; }
        public string Description { get; set; }
        public bool Valid { get; set; }

        public IList<ExpAlgorithm> ExpAlgorithms { get; set; }

        public void setAlgorithmAsRelativePath(String absPath)
        {
            String relativeTo = "ReEngine\\Rscripts";
            String relPath = Path.GetRelativePath(relativeTo, absPath);
            Filename = "/" + relPath.Replace("\\", "/");
        }
    }
}
