#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class AppleTangle
    {
        private static byte[] data = System.Convert.FromBase64String("raV3lEFVU8epKUe1/v3ldsvgpUoqJmVjdHJvYG9lZ3JjJnZpam9lfyA2IgAFUwINFRtHdnZqYyZFY3RyiXWHZsAdXQ8plLT+Qk72Zj6YE/N2amMmVGlpciZFRzYYEQs2MDYyNI0fj9j/TWrzAa0kNgTuHjj+Vg/VZGpjJnVyZ2hiZ3RiJnJjdGt1JmdvYG9lZ3JvaWgmR3Nybml0b3J/N4QHBgAPLIBOgPFlYgMHNof0NiwAk5h8CqJBjV3SEDE1zcIJS8gSb9fGZTVx8TwBKlDt3AknCNy8dR9Js4YSLdZvQZJwD/jybYsoRqDxQUt5dmpjJkVjdHJvYG9lZ3JvaWgmR3NiMyUTTRNfG7WS8fCamMlWvMdeVq7aeCQzzCPT3wnQbdKkIiUX8aeqAep7P4WNVSbVPsK3uZxJDG35Lfp8NoQHcDYIAAVTGwkHB/kCAgUEBw4tAAcDAwEEBxAYbnJydnU8KSlxtzZe6lwCNIputYkb2GN1+WFYY7oCABUEU1U3FTYXAAVTAgwVDEd2diZFRzaEByQ2CwAPLIBOgPELBwcHNoQCvTaEBaWmBQQHBAQHBDYLAA8pNofFAA4tAAcDAwEEBDaHsByHtTM0NzI2NTBcEQs1MzY0Nj80NzI2CwAPLIBOgPELBwcDAwYFhAcHBlpDeBlKbVaQR4/CcmQNFoVHgTWMh98weceBU9+hn780RP3e03eYeKdUYYkOsibxzaoqJml2sDkHNoqxRcmzPKvyCQgGlA23JxAoctM6C91kELjydZ3o1GIJzX9JMt6kOP9++W3OAAVTGwgCEAISLdZvQZJwD/jybYs1MFw2ZDcNNg8ABVMCABUEU1U3FQA2CQAFUxsVBwf5AgM2BQcH+TYbcm5pdG9yfzcQNhIABVMCBRULR3ZfoQMPehFGUBcYctWxjSU9QaXTaSyAToDxCwcHAwMGNmQ3DTYPAAVTIuTt17F22QlD5yHM92t+6+GzERFUY2pvZ2hlYyZpaCZybm91JmVjdGhiJmVpaGJvcm9paHUmaWAmc3VjJmdoYiZlY3Ryb2BvZWdyb2loJnYoRqDxQUt5Dlg2GQAFUxslAh42EBmX3RhBVu0D61h/givtMKRRSlPqfyZndXVza2N1JmdlZWN2cmdoZWN0Z2Vyb2VjJnVyZ3Jja2NocnUoNg5YNoQHFwAFUxsmAoQHDjaEBwI2CZs79S1PLhzO+MizvwjfWBrQzTtP3nCZNRJjp3GSzysEBQcGB6WEB3FxKGd2dmpjKGVpaylndnZqY2VneUeunv/XzGCaIm0X1qW94h0sxRkwn0orfrHrip3a9XGd9HDUcTZJx3JvYG9lZ3JjJmR/JmdofyZ2Z3RyGYOFgx2fO0Ex9K+dRogq0reWFN4maWAmcm5jJnJuY2gmZ3Z2am9lZ7Edu5VEIhQswQkbsEuaWGXOTYYREDYSAAVTAgUVC0d2dmpjJlRpaXI7IGEmjDVs8QuEydjtpSn/VWxdYmpjJk9oZSg3IDYiAAVTAg0VG0d2NhcABVMCDBUMR3Z2amMmT2hlKDfPH3TzWwjTeVmd9CMFvFOJS1sL9wMGBYQHCQY2hAcMBIQHBwbil68PVqyM09zi+tYPATG2c3Mn");
        private static int[] order = new int[] { 24,44,10,55,6,16,42,11,34,18,57,32,42,20,54,51,28,35,26,57,20,24,37,32,47,25,58,47,32,50,30,34,50,50,52,44,53,38,43,45,52,51,52,46,50,46,47,53,50,50,53,53,53,59,59,56,56,58,58,59,60 };
        private static int key = 6;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
