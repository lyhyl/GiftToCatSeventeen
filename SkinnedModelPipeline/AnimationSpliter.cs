using System;
using System.Collections.Generic;
using System.IO;

namespace SkinnedModelPipeline
{
    class AnimationSpliter
    {
        List<KeyValuePair<string, int>> splitedKeyFrames = new List<KeyValuePair<string, int>>();

        public uint KeyframeCount { get; set; }

        public AnimationSpliter(string path)
        {
            Split(path);
        }

        private void Split(string path)
        {
            string dataStr = ReadFile(path);
            string[] dataSplitBlock = dataStr.Split(new char[] { '\n' });
            char[] separator = new char[] { ',' };
            try
            {
                if (dataSplitBlock.Length < 2)
                    throw new Exception(string.Format("Need one animation at least.", path));
                KeyframeCount = uint.Parse(dataSplitBlock[0]);
                for (int i = 1; i < dataSplitBlock.Length; ++i)
                {
                    string[] dataSplitNameFrame = dataSplitBlock[i].Split(separator);
                    splitedKeyFrames.Add(new KeyValuePair<string, int>(
                        dataSplitNameFrame[0],
                        int.Parse(dataSplitNameFrame[1])));
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Bad Split File {0}", path), e);
            }
            if (splitedKeyFrames.Count == 0)
                throw new Exception(string.Format("Bad Split File {0}", path));
        }

        private static string ReadFile(string path)
        {
            FileStream dataFile = null;
            string data;
            try
            {
                dataFile = new FileStream(path, FileMode.Open);
                StreamReader reader = new StreamReader(dataFile);
                data = reader.ReadToEnd();
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Open Spliter File {0} Failed!", path), e);
            }
            finally
            {
                if (dataFile != null)
                    dataFile.Close();
            }
            return data;
        }

        public List<KeyValuePair<string, int>> SplitData
        {
            get { return splitedKeyFrames; }
        }
    }
}
