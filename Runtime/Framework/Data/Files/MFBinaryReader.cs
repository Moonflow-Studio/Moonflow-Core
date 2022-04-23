using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MFBinaryReader
{
    MemoryStream m_Stream = null;
    BinaryReader m_BinaryReader = null;
        
    public MFBinaryReader ReadBool(ref bool value)
    {
        value = m_BinaryReader.ReadBoolean();
        return this;
    }
    public MFBinaryReader ReadInt(ref int value)
    {
        value = m_BinaryReader.ReadInt32();
        return this;
    }
    public MFBinaryReader ReadFloat(ref float value)
    {
        value = m_BinaryReader.ReadSingle();
        return this;
    }
    public MFBinaryReader ReadString(ref string value)
    {
        value = m_BinaryReader.ReadString();
        return this;
    }
    public MFBinaryReader ReadVector2(ref Vector2 value)
    {
        value.x = m_BinaryReader.ReadSingle();
        value.y = m_BinaryReader.ReadSingle();
        return this;
    }
    public MFBinaryReader ReadVector2Int(ref Vector2Int value)
    {
        value.x = m_BinaryReader.ReadInt32();
        value.y = m_BinaryReader.ReadInt32();
        return this;
    }
    public MFBinaryReader ReadVector3(ref Vector3 value)
    {
        value.x = m_BinaryReader.ReadSingle();
        value.y = m_BinaryReader.ReadSingle();
        value.z = m_BinaryReader.ReadSingle();
        return this;
    }
    public MFBinaryReader ReadVector3Int(ref Vector3Int value)
    {
        value.x = m_BinaryReader.ReadInt32();
        value.y = m_BinaryReader.ReadInt32();
        value.z = m_BinaryReader.ReadInt32();
        return this;
    }
    public MFBinaryReader ReadVector4(ref Vector4 value)
    {
        value.x = m_BinaryReader.ReadSingle();
        value.y = m_BinaryReader.ReadSingle();
        value.z = m_BinaryReader.ReadSingle();
        value.w = m_BinaryReader.ReadSingle();
        return this;
    }
    public MFBinaryReader ReadColor(ref Color value)
    {
        value.r = m_BinaryReader.ReadSingle();
        value.g = m_BinaryReader.ReadSingle();
        value.b = m_BinaryReader.ReadSingle();
        value.a = m_BinaryReader.ReadSingle();
        return this;
    }
    public MFBinaryReader ReadRGB(ref Color value)
    {
        value.r = m_BinaryReader.ReadSingle();
        value.g = m_BinaryReader.ReadSingle();
        value.b = m_BinaryReader.ReadSingle();
        return this;
    }
    public MFBinaryReader ReadQuaternion(ref Quaternion value)
    {
        value.x = m_BinaryReader.ReadSingle();
        value.y = m_BinaryReader.ReadSingle();
        value.z = m_BinaryReader.ReadSingle();
        value.w = m_BinaryReader.ReadSingle();
        return this;
    }
    public MFBinaryReader ReadMatrix(ref Matrix4x4 value)
    {
        value = Matrix4x4.identity;
        for (int i = 0; i < 16; i++)
        {
            value[i] = m_BinaryReader.ReadSingle();
        }
        return this;
    }

    public MFBinaryReader ReadAABB(ref Bounds aabb)
    {
        Vector3 min = Vector3.zero;
        Vector3 max = Vector3.zero;
        ReadVector3(ref min);
        ReadVector3(ref max);
        aabb.SetMinMax(min, max);
        return this;
    }
}
