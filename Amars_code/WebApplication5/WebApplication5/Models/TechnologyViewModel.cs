using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class TechnologyViewModel
    {
        public List<Technology> Technologies
        {
            get
            {
                return StaticData.Technologies;
            }
        }
    }
}