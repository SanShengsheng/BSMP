using System;
using System.Collections;

namespace MQKJ.BSMP.Utils.Tools
{
    /// <summary>
    /// 随机打乱顺序
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Item<T>
    {
        T[] item;
        //构造函数
        public Item(T[] obj)
        {
            item = new T[obj.Length];
            for (int i = 0; i < obj.Length; i++)
            {
                item[i] = obj[i];
            }
        }
        public Type ShowType() { return typeof(T); } //返回类型
        public T[] GetItems() { return item; } //返回原数组
                                               //返回打乱顺序后的数组
        public T[] GetDisruptedItems()
        {
            //生成一个新数组：用于在之上计算和返回
            T[] temp;
            temp = new T[item.Length];
            for (int i = 0; i < temp.Length; i++) { temp[i] = item[i]; }
            //打乱数组中元素顺序
            Random rand = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < temp.Length; i++)
            {
                int x, y; T t;
                x = rand.Next(0, temp.Length);
                do
                {
                    y = rand.Next(0, temp.Length);
                } while (y == x);
                t = temp[x];
                temp[x] = temp[y];
                temp[y] = t;
            }
            return temp;
        }
    }
}
