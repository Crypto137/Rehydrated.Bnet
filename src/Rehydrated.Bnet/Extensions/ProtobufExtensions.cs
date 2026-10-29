using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;

namespace Rehydrated.Bnet.Extensions
{
    internal static class ProtobufExtensions
    {
        /// <summary>
        /// Returns the method id for the provided <see cref="MethodDescriptor"/>.
        /// </summary>
        public static uint GetMethodId(this MethodDescriptor method)
        {
            return (uint)method.Options[bnet.protocol.Rpc.MethodId.Descriptor];
        }

        /// <summary>
        /// Returns the <see cref="MethodDescriptor"/> for the specified method id in this <see cref="IService"/>.
        /// Returns <see langword="null"/> if the method is not found.
        /// </summary>
        public static MethodDescriptor GetMethod(this IService service, uint methodId)
        {
            foreach (MethodDescriptor method in service.DescriptorForType.Methods)
            {
                if (method.GetMethodId() == methodId)
                    return method;
            }

            return null;
        }

        /// <summary>
        /// Reads an <see cref="IMessage"/> into the provided <see cref="IBuilder"/> with the specified size limit.
        /// </summary>
        public static void ReadMessage(this CodedInputStream input, IBuilder builder, ExtensionRegistry extensionRegistry, int byteLimit)
        {
            // Do the equivalent of CodedInputStream.ReadMessage() without reading size.
            int oldLimit = input.PushLimit(byteLimit);
            builder.WeakMergeFrom(input, extensionRegistry);
            input.CheckLastTagWas(0u);
            input.PopLimit(oldLimit);
        }

        public static string ToHexString(this ByteString byteString)
        {
            return Convert.ToHexString(ByteString.Unsafe.GetBuffer(byteString));
        }
    }
}
