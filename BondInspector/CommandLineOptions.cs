using CommandLine;

enum BondBinaryFormat
{
    Compact,
    Fast,
}

class CommandLineOptions
{
    public CommandLineOptions()
    {
        BinaryFilePath = "";
    }

    [Value(0, Default = "", HelpText = "Binary file path. If not set, we read from stdin.")]
    public string BinaryFilePath { get; set; }

    [Option('f', "format", Default = BondBinaryFormat.Compact, HelpText = "Binary format. Can be Compact or Fast.")]
    public BondBinaryFormat BinaryFormat { get; set; }
}