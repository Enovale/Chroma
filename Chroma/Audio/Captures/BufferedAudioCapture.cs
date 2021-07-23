using System.IO;

namespace Chroma.Audio.Captures
{
    public class BufferedAudioCapture : AudioCapture
    {
        private Stream _stream;
        
        public BufferedAudioCapture(
            Stream stream,
            AudioDevice device = null, 
            AudioFormat format = null, 
            ChannelMode channelMode = ChannelMode.Mono, 
            int frequency = 44100, ushort bufferSize = 4096) 
            : base(device, format, channelMode, frequency, bufferSize)
        {
            _stream = stream;
        }
        
        protected override void ProcessAudioBuffer(byte[] buffer)
        {
            _stream.Write(buffer, 0, buffer.Length);
        }
    }
}