using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagement.Areas.TestCenter.Models
{
    public class TestCenter
    {
        public IEnumerable<TestCenter> TestListCollction { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TestCenter()
        {
            this.Test_Attend_Marks = new HashSet<Test_Attend_Marks>();
        }

        public int TestID { get; set; }
        public Nullable<int> ClassID { get; set; }
        public string TestName { get; set; }
        public Nullable<int> outOFF { get; set; }
        public string Description { get; set; }

        public virtual Class Class { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Test_Attend_Marks> Test_Attend_Marks { get; set; }
    }
}