// 201904089:04 AM

using System.IO;
using System.Threading.Tasks;

namespace TestAsyncIORead {
    public class LocalFile {
        public byte[] BufferBytes {
            get;
            set;
        }

        /// <summary>
        ///     异步读取文件
        /// </summary>
        /// <param name="filename">文件路径</param>
        /// <returns></returns>
        public async Task<byte[]> ReadFileAsnc(string filename) {
            if (!File.Exists(filename)) {
                return null;
            }
            byte[] resulBytes;

            using(FileStream readStream = File.Open(filename, FileMode.Open)) {
                int len = (int) readStream.Length;
                resulBytes = new byte[len];
                await readStream.ReadAsync(resulBytes, 0, len);
            }
            return resulBytes;
        }
    }
}
