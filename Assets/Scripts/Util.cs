using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;


public static class Util {
    public static int findInArrayArray(this string[][] uncooked, string[] slot) {
        int temp2 = -1;
        int count = 0;
        foreach(string[] order in uncooked) {
            bool check = false;
            if(order.Length != slot.Length) { check = true; }
            foreach(string food in slot) {
                if(Array.IndexOf(order, food) == -1) { check = true; }
            }
            if(!check) {
                temp2 = count;
            }
            count++;
        }
        return temp2;
    }

    public static string arrayToString(this string[] f) {
        string temp = "";
        int count = 0;
        foreach(string i in f) {
            if(count != 0) {
                temp += " ";
            }
            count++;
            temp += i;
        }
        return temp;
    }

    public static string arrayArrayToString(this string[][] f) {
        string temp = "";
        int count = 0;
        foreach(string[] i in f) {
            if(count != 0) {
                temp += ", ";
            }
            count++;
            temp += arrayToString(i);
        }
        return temp;
    }

    public static string CapitalizeFirstLetter(this string word)
        => word[0].ToString().ToUpper() + word.Substring(1);
}
