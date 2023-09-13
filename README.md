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

Then we will get the output like this:

```bash
> BondInspector.exe "binary.bin"
0: Struct
  0: BT_UINT32 - 105
  1: BT_WSTRING - abc
  2: BT_WSTRING - test-data
  3: BT_LIST<BT_STRUCT> - 15 items
    0: Struct
      0: Struct
        0: BT_UINT16 - 123
        1: BT_UINT16 - 456
      1: Struct
        0: BT_INT32 - 1
        1: BT_UINT16 - 2
        2: BT_UINT16 - 3
    1: Struct
      0: Struct
        0: BT_UINT16 - 456
        1: BT_UINT16 - 789
      1: Struct
        0: BT_INT32 - 4
        1: BT_UINT16 - 5
        2: BT_UINT16 - 6
...
```

## Build

To build the project, we need to install the [.NET Core SDK](https://dotnet.microsoft.com/download) first. Then run the following command to build the project.

```bash
cd BondInspector
dotnet build
```

## Known issues

In powershell, pipe data into the inspector will not work, because powershell pipe will corrupt the binary data. Please see this issue: <https://github.com/PowerShell/PowerShell/issues/1908>.