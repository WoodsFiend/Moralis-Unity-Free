using System;
using System.Collections.Generic;

namespace MoralisSDK.Tools
{
    public class ArrayChunker
    {
        public static List<T[]> ChunkArray<T>(T[] array, int chunkSize)
        {
            List<T[]> chunks = new List<T[]>();

            for (int i = 0; i < array.Length; i += chunkSize)
            {
                int currentChunkSize = Math.Min(chunkSize, array.Length - i);
                T[] chunk = new T[currentChunkSize];
                Array.Copy(array, i, chunk, 0, currentChunkSize);
                chunks.Add(chunk);
            }

            return chunks;
        }
    }
}