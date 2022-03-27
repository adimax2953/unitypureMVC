using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class RandomManager
{
    public static string createRandomCode(int count)
    {

        string allChar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
        string[] allCharArray = allChar.Split(',');
        string randomCode = "";
        int temp = -1;

        Random rand = new Random();
        for (int i = 0; i < count; i++)
        {
            if (temp != -1)
            {
                rand = new Random(Guid.NewGuid().GetHashCode());
            }
            int t = rand.Next(62);
            if (temp != -1 && temp == t)
            {
                return createRandomCode(count);
            }
            temp = t;
            randomCode += allCharArray[t];
        }
        return randomCode; 
    }
}