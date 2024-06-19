using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreMaxTech_Assessment
{
    class FileTracker
    {
        private static void CharAdded(char character)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(character);
            Console.ResetColor();
        }

        private static void CharDeleted(char character)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(character);
            Console.ResetColor();
        }

        public static void WatchFileChanges(string filePath, double timeToWait)
        {
            DateTime? lastChangeTS = null;
            string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".txt");

            File.WriteAllText(tempFilePath, string.Empty);

            while (true)
            {
                Console.WriteLine();
                if (lastChangeTS != File.GetLastWriteTime(filePath))
                {
                    lastChangeTS = File.GetLastWriteTime(filePath);

                    MessageHelper.Info($"Checking for file changes at {DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}");

                    FileStream filePathStream = File.OpenRead(filePath);
                    FileStream tempFilePathStream = new FileStream(tempFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                    try
                    {
                        int tempFileByte = tempFilePathStream.ReadByte();
                        int fileByte = filePathStream.ReadByte();

                        while (tempFileByte != -1 || fileByte != -1)
                        {
                            //If there is a character at the position and if they are not equal, consider them updated which is deleted and added operations.
                            if (tempFileByte != -1 && fileByte != -1)
                            {
                                if (tempFileByte != fileByte)
                                {
                                    Console.Write('[');
                                    
                                    CharDeleted((char)tempFileByte);
                                    CharAdded((char)fileByte);
                                    
                                    Console.Write(']');
                                }
                                else
                                {
                                    Console.Write((char)fileByte);
                                }
                            }

                            //If tempFile stream has ended and other is not, it means that there is lot more data in target file
                            if (tempFileByte == -1 && fileByte != -1)
                            {
                                CharAdded((char)fileByte);
                            }

                            //If tempFile stream has not ended and other is ended, it means that rest of the data is deleted.
                            if (tempFileByte != -1 && fileByte == -1)
                            {
                                CharDeleted((char)tempFileByte);
                            }

                            tempFileByte = tempFilePathStream.ReadByte();
                            fileByte = filePathStream.ReadByte();
                        }

                        tempFilePathStream.Close();
                        CopyFileContents(filePathStream, tempFilePath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        tempFilePathStream.Close();
                        filePathStream.Close();
                    }
                }
                else
                {
                    Console.WriteLine($"No changes detected from last write time {lastChangeTS.Value.ToLongTimeString()}");
                }

                Thread.Sleep(Convert.ToInt32(timeToWait));
            }
        }

        private static void CopyFileContents(FileStream source, string destinationFilePath)
        {
            source.Seek(0, SeekOrigin.Begin);

            if (File.Exists(destinationFilePath))
                File.Delete(destinationFilePath);

            using (FileStream destination = new FileStream(destinationFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                byte[] buffer = new Byte[1024];
                int bytesRead;

                while ((bytesRead =
                        source.Read(buffer, 0, 1024)) > 0)
                {
                    destination.Write(buffer, 0, bytesRead);
                }
            }
        }
    }
}
