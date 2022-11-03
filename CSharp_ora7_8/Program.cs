using System.Formats.Asn1;
using System.IO;

int min = 200000, max = 5500000, amount = 100000;
string txtData = "D:\\data.txt", binData = "D:\\BinData";
List<int> inputList = new List<int>();
Random rnd = new Random();
for (int i = 0; i < amount; i++)
{
    inputList.Add(rnd.Next(min, max));
}

var watch = new System.Diagnostics.Stopwatch();

// Writing into txt
StreamWriter sw = new StreamWriter(txtData);
try
{
    watch.Start();
    for (int i = 0; i < inputList.Count; i++)
    {
        sw.WriteLine(inputList[i]);
    }
    watch.Stop();
    Console.WriteLine($"Execution Time - txt write: {watch.ElapsedMilliseconds} ms");
}
catch (Exception e)
{
    Console.WriteLine("Exception: " + e.Message);
}
finally
{
    sw.Close();
}

// Writing into binary
BinaryWriter bw;
try
{
    bw = new BinaryWriter(new FileStream(binData, FileMode.Create));
}
catch (IOException e)
{
    Console.WriteLine(e.Message + "\n Cannot create file.");
    return;
}
try
{
    watch.Restart();
    for (int i = 0; i < inputList.Count; i++)
    {
        bw.Write(inputList[i]);
    }
    watch.Stop();
    Console.WriteLine($"Execution Time - binary write: {watch.ElapsedMilliseconds} ms");
}
catch (Exception e)
{
    Console.WriteLine("Exception: " + e.Message);
}
finally
{
    bw.Close();
}

// Reading from txt
String line;
List<int> txtList = new List<int>();
try
{
    StreamReader sr = new StreamReader(txtData);
    watch.Restart();
    line = sr.ReadLine();
    while (line != null)
    {
        txtList.Add(int.Parse(line));
        line = sr.ReadLine();
    }
    watch.Stop();
    Console.WriteLine($"Execution Time - txt read: {watch.ElapsedMilliseconds} ms");
    sr.Close();
}
catch (Exception e)
{
    Console.WriteLine("Exception: " + e.Message);
}

// Binary read
BinaryReader br;
FileStream fs;
List<int> binList = new List<int>();
try
{
    //fs = new FileStream("D:\\BinData", FileMode.Open);
    br = new BinaryReader(File.Open(binData, FileMode.Open));
}
catch (IOException e)
{
    Console.WriteLine(e.Message + "\n Cannot open file.");
    return;
}
try
{
    watch.Restart();
    //while (br.PeekChar() != -1/*br.BaseStream.Position != br.BaseStream.Length*/)
    //{
    //    binList.Add(br.ReadInt32());
    //}
    for(int i = 0; i < amount; i++)
    {
        binList.Add(br.ReadInt32());
    }
    watch.Stop();
    Console.WriteLine($"Execution Time - binary read: {watch.ElapsedMilliseconds} ms");
}
catch (IOException e)
{
    Console.WriteLine(e.Message + "\n Cannot read from file.");
    return;
}
br.Close();
Console.ReadKey();