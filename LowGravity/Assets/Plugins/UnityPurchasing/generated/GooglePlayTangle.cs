#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("wNRkaAOVWrQQMZSn1TR7/zPVJHGp8T/+UiQdIPVHqLDSKs475qqdM1jb1drqWNvQ2Fjb29pQ0xjF66NJ7CHRccwJm0gD1BVBbuXZV3yHOGcK/q1//wPn+OMi70gYZ7oXZfwU6uHYkRaNHe4VmmC75YaDQEbIbvhVSrSzls6mdMRMkGk4yhCPvAv7fOzqWNv46tfc0/Bcklwt19vb29/a2fUqnNbWkPtnwfF37GQvctIXVkJug5Syu2tvFY/gFiNPakIB89pXf2CA+rUIYNDG/TBvKynjsNThe17UnJJONNYl1vsvpjqzjbQDbFMXbEeOeR+FUrX1WCr9aC7JMlA1VSJ0P6s7pgylkJAxefb4vIAQr2IUz/n9u8nNl1YhQyiH79jZ29rb");
        private static int[] order = new int[] { 12,5,5,6,4,7,13,12,13,10,11,13,13,13,14 };
        private static int key = 218;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
