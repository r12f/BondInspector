using Bond;

namespace BondInspector;

public class ConsoleOutputInspectorEventHandler : InspectorEventHandler
{
    private Stack<BondDataType> itemStack = new Stack<BondDataType>();

    public override void OnBool(int id, bool value)
    {
        this.WriteValue(id, BondDataType.BT_BOOL, value);
    }

    public override void OnUInt8(int id, byte value)
    {
        this.WriteValue(id, BondDataType.BT_UINT8, value);
    }

    public override void OnUInt16(int id, ushort value)
    {
        this.WriteValue(id, BondDataType.BT_UINT16, value);
    }

    public override void OnUInt32(int id, uint value)
    {
        this.WriteValue(id, BondDataType.BT_UINT32, value);
    }

    public override void OnUInt64(int id, ulong value)
    {
        this.WriteValue(id, BondDataType.BT_UINT64, value);
    }

    public override void OnInt8(int id, sbyte value)
    {
        this.WriteValue(id, BondDataType.BT_INT8, value);
    }

    public override void OnInt16(int id, short value)
    {
        this.WriteValue(id, BondDataType.BT_INT16, value);
    }

    public override void OnInt32(int id, int value)
    {
        this.WriteValue(id, BondDataType.BT_INT32, value);
    }

    public override void OnInt64(int id, long value)
    {
        this.WriteValue(id, BondDataType.BT_INT64, value);
    }

    public override void OnFloat(int id, float value)
    {
        this.WriteValue(id, BondDataType.BT_FLOAT, value);
    }

    public override void OnDouble(int id, double value)
    {
        this.WriteValue(id, BondDataType.BT_DOUBLE, value);
    }

    public override void OnString(int id, string value)
    {
        this.WriteValue(id, BondDataType.BT_STRING, value);
    }

    public override void OnWString(int id, string value)
    {
        this.WriteValue(id, BondDataType.BT_WSTRING, value);
    }

    // BT_LIST, BT_SET
    public override void EnterContainer(int id, BondDataType containerType, BondDataType itemType, int itemCount)
    {
        this.WriteIndent(id);
        Console.WriteLine("{0}: {1}<{2}> - {3} items", id, containerType, itemType, itemCount);
        this.itemStack.Push(containerType);
    }

    public override void ExitContainer(int id, BondDataType containerType, BondDataType itemType)
    {
        this.itemStack.Pop();
    }

    // BT_MAP
    public override void EnterMap(int id, BondDataType keyType, BondDataType valueType, int itemCount)
    {
        this.WriteIndent(id);
        Console.WriteLine("{0}: Map<{1}, {2}> - {3} items", id, keyType, valueType, itemCount);
        this.itemStack.Push(BondDataType.BT_MAP);
    }

    public override void ExitMap(int id, BondDataType keyType, BondDataType valueType)
    {
        this.itemStack.Pop();
    }

    // BT_STRUCT
    public override void EnterStruct(int id)
    {
        this.WriteIndent(id);
        Console.WriteLine("{0}: Struct", id);
        this.itemStack.Push(BondDataType.BT_STRUCT);
    }

    public override void ExitStruct(int id)
    {
        this.itemStack.Pop();
    }

    private void WriteValue(int id, BondDataType type, object value)
    {
        this.WriteIndent(id);
        Console.WriteLine("{0}: {1} - {2}", id, type, value);
    }

    private void WriteIndent(int id)
    {
        var indentCount = this.itemStack.Count * 2;
        Console.Write("".PadLeft(indentCount, ' '));
    }
}