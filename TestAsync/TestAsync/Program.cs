namespace TestAsync
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
// This class creates a single large buffer which can be divided up 
// and assigned to SocketAsyncEventArgs objects for use with each 
// socket I/O operation.  
// This enables bufffers to be easily reused and guards against 
// fragmenting heap memory.
// 
// The operations exposed on the BufferManager class are not thread safe.