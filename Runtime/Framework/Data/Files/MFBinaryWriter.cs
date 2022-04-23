using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MFBinaryWriter
{
    MemoryStream m_Stream = null;
    BinaryWriter m_BinaryWriter = null;
        
    public MFBinaryWriter Write(bool value)
    {
        m_BinaryWriter.Write(value);
        return this;
    }
    public MFBinaryWriter Write(int value)
    {
        m_BinaryWriter.Write(value);
        return this;
    }
    public MFBinaryWriter Write(float value)
    {
        m_BinaryWriter.Write(value);
        return this;
    }
    public MFBinaryWriter Write(string value)
    {
        m_BinaryWriter.Write(value);
        return this;
    }
    public MFBinaryWriter Write(Vector2 value)
    {
        m_BinaryWriter.Write(value.x);
        m_BinaryWriter.Write(value.y);
        return this;
    } 
    public MFBinaryWriter Write(Vector2Int value)
    {
        m_BinaryWriter.Write(value.x);
        m_BinaryWriter.Write(value.y);
        return this;
    } 
    public MFBinaryWriter Write(Vector3 value)
    {
        m_BinaryWriter.Write(value.x);
        m_BinaryWriter.Write(value.y);
        m_BinaryWriter.Write(value.z);
        return this;
    } 
    public MFBinaryWriter Write(Vector3Int value)
    {
        m_BinaryWriter.Write(value.x);
        m_BinaryWriter.Write(value.y);
        m_BinaryWriter.Write(value.z);
        return this;
    } 
    public MFBinaryWriter Write(Vector4 value)
    {
        m_BinaryWriter.Write(value.x);
        m_BinaryWriter.Write(value.y);
        m_BinaryWriter.Write(value.z);
        m_BinaryWriter.Write(value.w);
        return this;
    }
    public MFBinaryWriter Write(Color value)
    {
        m_BinaryWriter.Write(value.r);
        m_BinaryWriter.Write(value.g);
        m_BinaryWriter.Write(value.b);
        m_BinaryWriter.Write(value.a);
        return this;
    }
    public MFBinaryWriter WriteRGB(Color value)
    {
        m_BinaryWriter.Write(value.r);
        m_BinaryWriter.Write(value.g);
        m_BinaryWriter.Write(value.b);
        return this;
    }
    public MFBinaryWriter Write(Quaternion value)
    {
        m_BinaryWriter.Write(value.x);
        m_BinaryWriter.Write(value.y);
        m_BinaryWriter.Write(value.z);
        m_BinaryWriter.Write(value.w);
        return this;
    }
    public MFBinaryWriter Write(Matrix4x4 value)
    {
        for (int i = 0; i < 16; i++)
        {
            m_BinaryWriter.Write(value[i]);
        }
        return this;
    }
    public MFBinaryWriter Write(Bounds value)
    {
        Write(value.min);
        Write(value.max);
        return this;
    }

    public MFBinaryWriter Write(AnimationCurve value)
    {
        Write((int)value.preWrapMode);
        Write((int)value.postWrapMode);
        Write(value.length);
        for (int i = 0; i < value.length; i++)
        {
            Write(value.keys[i]);
        }
        return this;
    }

    public MFBinaryWriter Write(Keyframe keyframe)
    {
        Write((int)keyframe.weightedMode);
        Write(keyframe.time);
        Write(keyframe.value);
        Write(keyframe.inTangent);
        Write(keyframe.inWeight);
        Write(keyframe.outTangent);
        Write(keyframe.outWeight);
        return this;
    }
    
}
