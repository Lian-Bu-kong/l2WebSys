using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Core.Util
{
    public static class MsgUtil
    {

        /// <summary>
        /// 將byte array解碼為對應型別資料
        /// </summary>
        /// <param name="byteData">要反序列化的byte array</param>
        /// <param name="type">要轉換的型別</param>
        /// <returns></returns>
        public static object RawDeserialize(this byte[] byteData, Type type)
        {
            try
            {
                int rawsize = Marshal.SizeOf(type);
                if (rawsize > byteData.Length)
                {
                    return null;
                }

                int iCursor = 0;

                ConvertEndian(byteData, type, iCursor);

                IntPtr buffer = Marshal.AllocHGlobal(rawsize);
                Marshal.Copy(byteData, 0, buffer, rawsize);
                Object structureData = Marshal.PtrToStructure(buffer, type);

                //釋放
                Marshal.FreeHGlobal(buffer);
                return structureData;
            }
            catch
            {
                throw;
            }
        }
        private static byte[] ConvertEndian(byte[] byteData, Type type, int iCursor = 0)
        {
            try
            {
                foreach (FieldInfo fi in type.GetFields())
                {
                    Type FieldType = fi.FieldType;
                    bool bIsArray = false;
                    int iSizeConst = 0;

                    foreach (CustomAttributeData cad in fi.CustomAttributes)
                    {
                        foreach (CustomAttributeTypedArgument CA in cad.ConstructorArguments)
                        {
                            if (CA.ArgumentType.Name == "UnmanagedType")
                            {
                                if (CA.Value.GetType() == typeof(byte[]))
                                {
                                    bIsArray = true;
                                    break;
                                }
                            }
                        }
                        foreach (CustomAttributeNamedArgument NA in cad.NamedArguments)
                        {
                            if (NA.MemberName == "SizeConst")
                            {
                                iSizeConst = Convert.ToInt32(NA.TypedValue.Value);
                                break;
                            }
                        }
                    }

                    int iMLength;
                    if (bIsArray)
                    {
                        FieldType = fi.FieldType.Assembly.GetType(fi.FieldType.FullName.Replace("[]", ""));
                        for (Int16 i = 0; i <= iSizeConst - 1; i++)
                        {
                            switch (FieldType.GetType())
                            {
                                case Type intType when intType == typeof(int):
                                case Type shortType when shortType == typeof(short):
                                case Type longType when longType == typeof(long):
                                case Type floatType when floatType == typeof(float):
                                case Type doubleType when doubleType == typeof(double):
                                    {
                                        // 需要進行轉換
                                        iMLength = Marshal.SizeOf(FieldType);
                                        Array.Reverse(byteData, iCursor, iMLength);
                                        iCursor += iMLength;
                                        break;
                                    }

                                case Type stringType when stringType == typeof(string):
                                    {
                                        break;
                                    }

                                default:
                                    {
                                        ConvertEndian(byteData, FieldType, iCursor);
                                        break;
                                    }
                            }
                        }
                    }
                    else
                    {
                        switch (FieldType)
                        {
                            case Type intType when intType == typeof(int):
                            case Type shortType when shortType == typeof(short):
                            case Type longType when longType == typeof(long):
                            case Type floatType when floatType == typeof(float):
                            case Type doubleType when doubleType == typeof(double):
                                {
                                    // 需要進行轉換
                                    iMLength = Marshal.SizeOf(FieldType);
                                    Array.Reverse(byteData, iCursor, iMLength);
                                    iCursor += iMLength;
                                    break;
                                }
                            case Type charType when charType == typeof(char[]):
                            case Type stringType when stringType == typeof(string):
                                {
                                    iMLength = iSizeConst;
                                    iCursor += iMLength;
                                    break;
                                }

                            default:
                                {
                                    ConvertEndian(byteData, FieldType, iCursor);
                                    break;
                                }
                        }
                    }
                }
            }
            catch
            {
                return null;
            }

            return byteData;
        }
    }
}
