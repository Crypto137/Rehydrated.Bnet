using bnet.protocol;
using bnet.protocol.presence;
using Google.ProtocolBuffers;

namespace Rehydrated.Bnet.Utils
{
    public enum FieldKeyProgram : uint
    {
        BNet = 16974,
        D3 = 17459,
        S2 = 21298,
        WoW = 5730135,
    }

    public enum FieldKeyGroup : uint
    {
        Account = 1,
        GameAccount = 2,
        Hero = 3,
        Party = 4,
        GameSession = 5,
    }

    internal static class PresenceUtility
    {
        public static FieldKey CreateFieldKey(FieldKeyProgram program, FieldKeyGroup group, uint field, ulong index)
        {
            return FieldKey.CreateBuilder()
                .SetProgram((uint)program)
                .SetGroup((uint)group)
                .SetPresenceField(field)
                .SetIndex(index)
                .Build();
        }

        public static Field CreateField(FieldKeyProgram program, FieldKeyGroup group, uint field, ulong index, ByteString value)
        {
            return Field.CreateBuilder()
                .SetKey(CreateFieldKey(program, group, field, index))
                .SetValue(bnet.protocol.attribute.Variant.CreateBuilder().SetMessageValue(value))
                .Build();
        }

        public static Field CreateField(FieldKeyProgram program, FieldKeyGroup group, uint field, ulong index, EntityId value)
        {
            return Field.CreateBuilder()
                .SetKey(CreateFieldKey(program, group, field, index))
                .SetValue(bnet.protocol.attribute.Variant.CreateBuilder().SetEntityidValue(value))
                .Build();
        }

        public static Field CreateField(FieldKeyProgram program, FieldKeyGroup group, uint field, ulong index, string value)
        {
            return Field.CreateBuilder()
                .SetKey(CreateFieldKey(program, group, field, index))
                .SetValue(bnet.protocol.attribute.Variant.CreateBuilder().SetStringValue(value))
                .Build();
        }

        public static Field CreateField(FieldKeyProgram program, FieldKeyGroup group, uint field, ulong index, int value)
        {
            return Field.CreateBuilder()
                .SetKey(CreateFieldKey(program, group, field, index))
                .SetValue(bnet.protocol.attribute.Variant.CreateBuilder().SetIntValue(value))
                .Build();
        }

        public static Field CreateField(FieldKeyProgram program, FieldKeyGroup group, uint field, ulong index, bool value)
        {
            return Field.CreateBuilder()
                .SetKey(CreateFieldKey(program, group, field, index))
                .SetValue(bnet.protocol.attribute.Variant.CreateBuilder().SetBoolValue(value))
                .Build();
        }

        public static FieldOperation BuildFieldOperation(FieldOperation.Types.OperationType opType, Field field)
        {
            return FieldOperation.CreateBuilder()
                .SetOperation(FieldOperation.Types.OperationType.SET)
                .SetPresenceField(field)
                .Build();
        }
    }
}
