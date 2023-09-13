using Bond;
using Bond.Protocols;

namespace BondInspector;

public class Inspector
{
    private ITaggedProtocolReader reader;
    private InspectorEventHandler handler;

    public Inspector(ITaggedProtocolReader reader, InspectorEventHandler handler)
    {
        this.reader = reader;
        this.handler = handler;
    }

    public void Run()
    {
        this.ReadStruct(0);
    }

    private BondDataType ReadField()
    {
        BondDataType dt = BondDataType.BT_BOOL;
        ushort id = 0;
        this.reader.ReadFieldBegin(out dt, out id);

        if (dt == BondDataType.BT_STOP || dt == BondDataType.BT_STOP_BASE)
        {
            return dt;
        }

        this.ReadFieldValue(id, dt);

        return dt;
    }

    private void ReadFieldValue(int id, BondDataType dt)
    {
        switch (dt)
        {
            case BondDataType.BT_BOOL:
                this.handler.OnBool(id, this.reader.ReadBool());
                break;

            case BondDataType.BT_UINT8:
                this.handler.OnUInt8(id, this.reader.ReadUInt8());
                break;

            case BondDataType.BT_UINT16:
                this.handler.OnUInt16(id, this.reader.ReadUInt16());
                break;

            case BondDataType.BT_UINT32:
                this.handler.OnUInt32(id, this.reader.ReadUInt32());
                break;

            case BondDataType.BT_UINT64:
                this.handler.OnUInt64(id, this.reader.ReadUInt64());
                break;

            case BondDataType.BT_FLOAT:
                this.handler.OnFloat(id, this.reader.ReadFloat());
                break;

            case BondDataType.BT_DOUBLE:
                this.handler.OnDouble(id, this.reader.ReadDouble());
                break;

            case BondDataType.BT_INT8:
                this.handler.OnInt8(id, this.reader.ReadInt8());
                break;

            case BondDataType.BT_INT16:
                this.handler.OnInt16(id, this.reader.ReadInt16());
                break;

            case BondDataType.BT_INT32:
                this.handler.OnInt32(id, this.reader.ReadInt32());
                break;

            case BondDataType.BT_INT64:
                this.handler.OnInt64(id, this.reader.ReadInt64());
                break;

            case BondDataType.BT_STRING:
                this.handler.OnString(id, this.reader.ReadString());
                break;

            case BondDataType.BT_WSTRING:
                this.handler.OnWString(id, this.reader.ReadWString());
                break;

            case BondDataType.BT_STRUCT:
                this.ReadStruct(id);
                break;

            case BondDataType.BT_LIST:
            case BondDataType.BT_SET:
                this.ReadContainer(id, dt);
                break;

            case BondDataType.BT_MAP:
                this.ReadMap(id);
                break;

            default: // BT_STOP, BT_STOPBASE, BT_UNAVAILABLE
                throw new ArgumentOutOfRangeException("BondDataType", dt, "Unknown Bond data type.");
        }
    }

    private void ReadStruct(int id)
    {
        this.reader.ReadStructBegin();
        this.handler.EnterStruct(id);

        try
        {
            BondDataType dt = BondDataType.BT_BOOL;
            while (dt != BondDataType.BT_STOP)
            {
                dt = this.ReadField();
            }

            this.reader.ReadStructEnd();
        }
        finally
        {
            this.handler.ExitStruct(id);
        }
    }

    private void ReadContainer(int id, BondDataType containerType)
    {
        BondDataType itemType = BondDataType.BT_BOOL;
        int itemCount = 0;
        this.reader.ReadContainerBegin(out itemCount, out itemType);
        this.handler.EnterContainer(id, containerType, itemType, itemCount);

        try
        {
            for (int i = 0; i < itemCount; i++)
            {
                this.ReadFieldValue(i, itemType);
            }

            this.reader.ReadContainerEnd();
        }
        finally
        {

            this.handler.ExitContainer(id, containerType, itemType);
        }
    }

    private void ReadMap(int id)
    {
        BondDataType keyType = BondDataType.BT_BOOL;
        BondDataType valueType = BondDataType.BT_BOOL;
        int itemCount = 0;
        this.reader.ReadContainerBegin(out itemCount, out keyType, out valueType);
        this.handler.EnterMap(id, keyType, valueType, itemCount);

        try
        {

            for (ushort i = 0; i < itemCount; i++)
            {
                this.ReadFieldValue(i, keyType);
                this.ReadFieldValue(i, valueType);
            }

            this.reader.ReadContainerEnd();
        }
        finally
        {
            this.handler.ExitMap(id, keyType, valueType);
        }
    }
}