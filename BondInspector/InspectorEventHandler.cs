using Bond;

namespace BondInspector;

public abstract class InspectorEventHandler
{
    public abstract void OnBool(int id, bool value);
    public abstract void OnUInt8(int id, byte value);
    public abstract void OnUInt16(int id, ushort value);
    public abstract void OnUInt32(int id, uint value);
    public abstract void OnUInt64(int id, ulong value);
    public abstract void OnInt8(int id, sbyte value);
    public abstract void OnInt16(int id, short value);
    public abstract void OnInt32(int id, int value);
    public abstract void OnInt64(int id, long value);
    public abstract void OnFloat(int id, float value);
    public abstract void OnDouble(int id, double value);
    public abstract void OnString(int id, string value);
    public abstract void OnWString(int id, string value);

    // BT_LIST, BT_SET
    public abstract void EnterContainer(int id, BondDataType containerType, BondDataType itemType, int itemCount);
    public abstract void ExitContainer(int id, BondDataType containerType, BondDataType itemType);

    // BT_MAP
    public abstract void EnterMap(int id, BondDataType keyType, BondDataType valueType, int itemCount);
    public abstract void ExitMap(int id, BondDataType keyType, BondDataType valueType);

    // BT_STRUCT
    public abstract void EnterStruct(int id);
    public abstract void ExitStruct(int id);
}