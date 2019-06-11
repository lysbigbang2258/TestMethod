// 201903083:32 PM

using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace TestMVVMFram.Model {
    public class TreeNodeModel : ObservableObject
    {
        public string NodeID { get; set; }
        public string NodeName { get; set; }
        public List<TreeNodeModel> Children { get; set; }
    }
}
