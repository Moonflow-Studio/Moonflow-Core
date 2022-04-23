using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MFBinaryWriter
{
    protected MemoryStream m_Stream = null;
    protected BinaryWriter m_BinaryWriter = null;
        
    public MFBinaryWriter()
    {
        m_Stream = new MemoryStream(8192);
        m_BinaryWriter = new BinaryWriter(m_Stream);
    }
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

    public MFBinaryWriter Write(short value)
    {
        m_BinaryWriter.Write(value);
        return this;
    }
    
    public MFBinaryWriter Write(uint value)
    {
        m_BinaryWriter.Write((uint)value);
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

    public MFBinaryWriter Write(Color32 value)
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

    public MFBinaryWriter Write(Gradient g)
    {
        Write((int)g.mode);
        Write(g.alphaKeys.Length);
        Write(g.colorKeys.Length);
        for (int i = 0; i < g.alphaKeys.Length; i++)
        {
            Write(g.alphaKeys[i]);
        }

        for (int i = 0; i < g.colorKeys.Length; i++)
        {
            Write(g.colorKeys[i]);
        }
        return this;
    }

    public MFBinaryWriter Write(GradientAlphaKey gak)
    {
        Write(gak.alpha);
        Write(gak.time);
        return this;
    }

    public MFBinaryWriter Write(GradientColorKey gck)
    {
        Write(gck.color);
        Write(gck.time);
        return this;
    }

    
}
