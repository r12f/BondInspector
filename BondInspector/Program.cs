using BondInspector;
using Bond.Protocols;
using Bond.IO.Unsafe;
using CommandLine;

static void RunOptions(CommandLineOptions opts)
{
    InputBuffer ib;

    if (string.IsNullOrEmpty(opts.BinaryFilePath))
    {
        if (Console.IsInputRedirected == false)
        {
            throw new ArgumentException("No input file specified and stdin is not redirected.");
        }

        // Read all data from console pipe
        using Stream stdin = Console.OpenStandardInput();
        using MemoryStream ms = new MemoryStream();
        stdin.CopyTo(ms);
        ms.Position = 0;
        ib = new Bond.IO.Unsafe.InputBuffer(ms.ToArray());
    }
    else
    {
        ib = new InputBuffer(File.ReadAllBytes(opts.BinaryFilePath));
    }

    ITaggedProtocolReader protocolReader = opts.BinaryFormat switch
    {
        BondBinaryFormat.Compact => new CompactBinaryReader<InputBuffer>(ib),
        BondBinaryFormat.Fast => new FastBinaryReader<InputBuffer>(ib),
        _ => throw new ArgumentOutOfRangeException("BondFormat", opts.BinaryFormat, "Unknown Bond format."),
    };

    var inspector = new Inspector(
        protocolReader,
        new ConsoleOutputInspectorEventHandler());

    inspector.Run();
}

CommandLine.Parser.Default.ParseArguments<CommandLineOptions>(args)
    .WithParsed(RunOptions);