using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MFBinaryReader
{
    protected MemoryStream m_Stream = null;
    protected BinaryReader m_BinaryReader = null;

    public MFBinaryReader()
    {
        m_Stream = new MemoryStream(8192);
        m_BinaryReader = new BinaryReader(m_Stream);
    }
    public bool ReadBool()
    {
        return m_BinaryReader.ReadBoolean();
    }
    public int ReadInt()
    {
        return m_BinaryReader.ReadInt32();
    }
    public float ReadFloat()
    {
        return m_BinaryReader.ReadSingle();
    }
    public string ReadString()
    {
        return m_BinaryReader.ReadString();
    }
    public Vector2 ReadVector2()
    {
        Vector2 value = Vector2.zero;
        value.x = m_BinaryReader.ReadSingle();
        value.y = m_BinaryReader.ReadSingle();
        return value;
    }
    public Vector2Int ReadVector2Int()
    {
        Vector2Int value = Vector2Int.zero;
        value.x = m_BinaryReader.ReadInt32();
        value.y = m_BinaryReader.ReadInt32();
        return value;
    }
    public Vector3 ReadVector3()
    {
        Vector3 value = Vector3.zero;
        value.x = m_BinaryReader.ReadSingle();
        value.y = m_BinaryReader.ReadSingle();
        value.z = m_BinaryReader.ReadSingle();
        return value;
    }
    public Vector3Int ReadVector3Int()
    {
        Vector3Int value = Vector3Int.zero;
        value.x = m_BinaryReader.ReadInt32();
        value.y = m_BinaryReader.ReadInt32();
        value.z = m_BinaryReader.ReadInt32();
        return value;
    }
    public Vector4 ReadVector4()
    {
        Vector4 value = Vector4.zero;
        value.x = m_BinaryReader.ReadSingle();
        value.y = m_BinaryReader.ReadSingle();
        value.z = m_BinaryReader.ReadSingle();
        value.w = m_BinaryReader.ReadSingle();
        return value;
    }
    public Color ReadColor()
    {
        Color value = Color.black;
        value.r = m_BinaryReader.ReadSingle();
        value.g = m_BinaryReader.ReadSingle();
        value.b = m_BinaryReader.ReadSingle();
        value.a = m_BinaryReader.ReadSingle();
        return value;
    }
    public void ReadRGB(ref Color value)
    {
        value.r = m_BinaryReader.ReadSingle();
        value.g = m_BinaryReader.ReadSingle();
        value.b = m_BinaryReader.ReadSingle();
    }
    public Quaternion ReadQuaternion()
    {
        Quaternion value = Quaternion.identity;
        value.x = m_BinaryReader.ReadSingle();
        value.y = m_BinaryReader.ReadSingle();
        value.z = m_BinaryReader.ReadSingle();
        value.w = m_BinaryReader.ReadSingle();
        return value;
    }
    public Matrix4x4 ReadMatrix()
    {
        Matrix4x4 value = new Matrix4x4();
        value = Matrix4x4.identity;
        for (int i = 0; i < 16; i++)
        {
            value[i] = m_BinaryReader.ReadSingle();
        }
        return value;
    }

    public Bounds ReadAABB()
    {
        Bounds aabb = new Bounds();
        Vector3 min = ReadVector3();
        Vector3 max = ReadVector3();
        aabb.SetMinMax(min, max);
        return aabb;
    }

    public AnimationCurve ReadAnimationCurve()
    {
        AnimationCurve ac = new AnimationCurve();
        ac.preWrapMode = (WrapMode)m_BinaryReader.ReadInt32();
        ac.postWrapMode = (WrapMode)m_BinaryReader.ReadInt32();
        int length = m_BinaryReader.ReadInt32();
        ac.keys = new Keyframe[length];
        for (int i = 0; i < length; i++)
        {
            ac.keys[i] = ReadKeyframe();
        }
        return ac;
    }

    public Keyframe ReadKeyframe()
    {
        Keyframe kf = new Keyframe();
        kf.weightedMode = (WeightedMode)m_BinaryReader.ReadInt32();
        kf.time = m_BinaryReader.ReadSingle();
        kf.value = m_BinaryReader.ReadSingle();
        kf.inTangent = m_BinaryReader.ReadSingle();
        kf.inWeight = m_BinaryReader.ReadSingle();
        kf.outTangent = m_BinaryReader.ReadSingle();
        kf.outWeight = m_BinaryReader.ReadSingle();
        return kf;
    }

}
