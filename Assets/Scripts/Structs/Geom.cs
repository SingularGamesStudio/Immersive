using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Geom
{
    public static float eps = 1e-8f;
    public static float goodAtan2(float x, float y)
    {
        if(x<=0 && y > 0)
        {
            return Mathf.Atan(-x / y);
        } else if(x<0 && y <= 0)
        {
            return Mathf.Atan(-y / -x)+Mathf.PI/2f;
        } else if(x >= 0 && y < 0)
        {
            return Mathf.Atan(x / -y) + Mathf.PI;
        } else if(x>0 && y >= 0)
        {
            return Mathf.Atan(y / x) + Mathf.PI*1.5f;
        } else
        {
            Debug.LogError("vector for atan2 is 0");
            return -1000;
        }
    }
    static bool between(float e1, float e2, float x)
    {
        if (e1 < 0)
            e1 += Mathf.PI * 2;
        if (e2 >= 2 * Mathf.PI)
            e2 -= Mathf.PI * 2;
        if (x < 0)
            x += Mathf.PI * 2;
        if (x >= 2 * Mathf.PI)
            x -= Mathf.PI * 2;
        if (e1<=e2)
        {
            return (x >= e1 && x < e2);
        } else
        {
            if (x < e1 && x > e2)
                return false;
            else return true;
        }
    }
   public static bool intersectSegmentSector(Vector2 s1, Vector2 s2, Vector2 Center, float r, float AngleC, float AngleR)
    {
        s1 = new Vector2(s1.x - Center.x, s1.y - Center.y);
        s2 = new Vector2(s2.x - Center.x, s2.y - Center.y);
        float A = s1.y - s2.y;
        float B = s2.x - s1.x;
        float C = s1.x * s2.y - s1.y * s2.x;
        Vector2 perp = new Vector2(-A * C / (A * A + B * B), -B * C / (A * A + B * B));
        if (perp.magnitude+eps>r)
        {
            return false;
        } else
        {
            float d = r * r - C * C / (A * A + B * B);
            float temp = Mathf.Sqrt(d / (A * A + B * B));
            Vector2 dot1 = new Vector2(perp.x + B * temp, perp.y - A * temp);
            Vector2 dot2 = new Vector2(perp.x - B * temp, perp.y + A * temp);
            Vector2 r1, r2;
            if (Mathf.Abs(s1.x - s2.x) == 0)
            {
                if (s1.y >= s2.y)
                {
                    Vector2 temp1 = s2;
                    s2 = s1;
                    s1 = temp1;
                }
                if (dot1.y >= dot2.y)
                {
                    Vector2 temp1 = dot2;
                    dot2 = dot1;
                    dot1 = temp1;
                }
                if (dot1.y > s2.y || dot2.y < s1.y)
                    return false;
                else
                {
                    if (s1.y > dot1.y)
                    {
                        r1 = s1;
                    }
                    else r1 = dot1;
                    if (s2.y > dot2.y)
                    {
                        r2 = s2;
                    }
                    else r2 = dot2;
                }
            } else
            {
                if (s1.x >= s2.x)
                {
                    Vector2 temp1 = s2;
                    s2 = s1;
                    s1 = temp1;
                }
                if (dot1.x >= dot2.x)
                {
                    Vector2 temp1 = dot2;
                    dot2 = dot1;
                    dot1 = temp1;
                }
                if (dot1.x > s2.x || dot2.x < s1.x)
                    return false;
                else
                {
                    if (s1.x > dot1.x)
                    {
                        r1 = s1;
                    }
                    else r1 = dot1;
                    if (s2.x > dot2.x)
                    {
                        r2 = s2;
                    }
                    else r2 = dot2;
                }
            }
            if(between(AngleC-AngleR, AngleC+AngleR, goodAtan2(r1.x, r1.y)) || between(AngleC - AngleR, AngleC + AngleR, goodAtan2(r2.x, r2.y)))
            {
                return true;
            } else
            {
                float a1 = goodAtan2(r1.x, r1.y), a2 = goodAtan2(r2.x, r2.y);
                if (a2 >= a1 && a2 - a1 <= Mathf.PI)
                {
                    if (between(a1, a2, AngleC))
                    {
                        return true;
                    }
                    else return false;
                } else
                {
                    if (between(a2, a1, AngleC))
                    {
                        return true;
                    }
                    else return false;
                }
            }
        }
    }
}
