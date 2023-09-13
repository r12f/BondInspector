# BondInspector

This is a very simple tool to inspect the content of [Bond](https://github.com/microsoft/bond) binary data. It is useful when you get a binary bond data, but some failing to parsing it and would like to see what is inside of it.

It is currently supporting only the tagged protocol - [Compact Binary](http://microsoft.github.io/bond/manual/bond_cs.html#compact-binary) and [Fast Binary](http://microsoft.github.io/bond/manual/bond_cs.html#fast-binary), because there is no generic way to inspect the untagged protocol.

## Usage

To check the binary data, simply run the tool with the binary file as the argument. And by default, we will use the Compact Binary protocol to parse the data.

```bash
BondInspector.exe compact-binary.bin
```

If you want to use the Fast Binary protocol, you can specify the protocol with the `-f Fast` option.

```bash
BondInspector.exe fast-binary.bin -f Fast
```

Also we can pipe the binary data into the inspector.

```bash
cat compact-binary.bin | BondInspector.exe
```

## Build

To build the project, we need to install the [.NET Core SDK](https://dotnet.microsoft.com/download) first. Then run the following command to build the project.

```bash
cd BondInspector
dotnet build
```

## Known issues

In powershell, pipe data into the inspector will not work, because powershell pipe will corrupt the binary data. Please see this issue: <https://github.com/PowerShell/PowerShell/issues/1908>.