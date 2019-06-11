using System;

namespace TestAsyncIORead {
    public class ReturnEndReadEventargs : EventArgs
    {
        public ReturnEndReadEventargs(bool isReturn,string value)
        {
            IsReturn = isReturn;
            ReadValue = value;
        }
        public string ReadValue { get; set; }
        public bool IsReturn { get; set; }
 
    }
}