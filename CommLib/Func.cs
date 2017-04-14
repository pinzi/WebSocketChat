using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace CommLib
{
    public static class Func
    {
        #region Enum枚举操作扩展方法

        /// <summary>  
        /// 根据枚举类型得到其所有的 值 与 枚举定义字符串 的集合  
        /// </summary>  
        /// <param name="enumType"></param>  
        /// <returns></returns>  
        public static NameValueCollection GetEnumStringFromEnumValue(Type enumType)
        {
            NameValueCollection nvc = new NameValueCollection();
            Type typeDescription = typeof(DescriptionAttribute);
            System.Reflection.FieldInfo[] fields = enumType.GetFields();
            string strText = string.Empty;
            string strValue = string.Empty;
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    strValue = ((int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null)).ToString();
                    nvc.Add(strValue, field.Name);
                }
            }
            return nvc;
        }

        /// <summary>  
        /// 扩展方法：根据枚举值得到属性Description中的描述, 如果没有定义此属性则返回空串  
        /// </summary>  
        /// <param name="value"></param>  
        /// <param name="enumType"></param>  
        /// <returns></returns>  
        public static String ToEnumDescriptionString(this int value, Type enumType)
        {
            NameValueCollection nvc = GetNVCFromEnumValue(enumType);
            return nvc[value.ToString()];
        }

        /// <summary>  
        /// 根据枚举类型得到其所有的 值 与 枚举定义Description属性 的集合  
        /// </summary>  
        /// <param name="enumType"></param>  
        /// <returns></returns>  
        public static NameValueCollection GetNVCFromEnumValue(Type enumType)
        {
            NameValueCollection nvc = new NameValueCollection();
            Type typeDescription = typeof(DescriptionAttribute);
            System.Reflection.FieldInfo[] fields = enumType.GetFields();
            string strText = string.Empty;
            string strValue = string.Empty;
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    strValue = ((int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null)).ToString();
                    object[] arr = field.GetCustomAttributes(typeDescription, true);
                    if (arr.Length > 0)
                    {
                        DescriptionAttribute aa = (DescriptionAttribute)arr[0];
                        strText = aa.Description;
                    }
                    else
                    {
                        strText = "";
                    }
                    nvc.Add(strValue, strText);
                }
            }
            return nvc;
        }

        /// <summary>
        /// 根据枚举值获取枚举描述
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum enumValue)
        {
            string str = enumValue.ToString();
            System.Reflection.FieldInfo field = enumValue.GetType().GetField(str);
            object[] objs = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (objs == null || objs.Length == 0) return str;
            System.ComponentModel.DescriptionAttribute da = (System.ComponentModel.DescriptionAttribute)objs[0];
            return da.Description;
        }

        /// <summary>
        /// 根据枚举值获取枚举名称
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetEnumName(Enum enumValue)
        {
            return enumValue.ToString();
        }

        private static ConcurrentDictionary<Enum, string> _ConcurrentDictionary = new ConcurrentDictionary<Enum, string>();

        /// <summary>
        /// 获取枚举的描述信息(Descripion)。
        /// 支持位域，如果是位域组合值，多个按分隔符组合。
        /// </summary>
        public static string GetDescription(this Enum @this)
        {
            return _ConcurrentDictionary.GetOrAdd(@this, (key) =>
            {
                var type = key.GetType();
                var field = type.GetField(key.ToString());
                //如果field为null则应该是组合位域值，
                return field == null ? key.GetDescriptions() : GetDescription(field);
            });
        }

        /// <summary>
        /// 获取位域枚举的描述，多个按分隔符组合
        /// </summary>
        public static string GetDescriptions(this Enum @this, string separator = ",")
        {
            var names = @this.ToString().Split(',');
            string[] res = new string[names.Length];
            var type = @this.GetType();
            for (int i = 0; i < names.Length; i++)
            {
                var field = type.GetField(names[i].Trim());
                if (field == null) continue;
                res[i] = GetDescription(field);
            }
            return string.Join(separator, res);
        }

        private static string GetDescription(FieldInfo field)
        {
            var att = System.Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute), false);
            return att == null ? field.Name : ((DescriptionAttribute)att).Description;
        }

        #endregion

        /// <summary>
        /// 字符串MD5加密
        /// </summary>
        /// <param name="StrText">明文字符串</param>
        /// <param name="EncryptLenth">密文字符长度：16或32，默认为32</param>
        /// <param name="IsCapitals">密文字符串是否全部大写，默认为True</param>
        /// <returns></returns>
        public static string MD5Encrypt(string StrText, [DefaultValue(32)]int EncryptLenth, [DefaultValue(true)]bool IsCapitals)
        {
            StringBuilder StrEncrypt = new StringBuilder(32);
            //16位密文
            if (EncryptLenth.Equals(16))
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                string Str = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(StrText)), 4, 8);
                StrEncrypt.Append(Str.Replace("-", ""));
            }
            else//32位密文
            {
                MD5 md5 = MD5.Create();//实例化一个md5对像
                //加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
                byte[] ArrByte = md5.ComputeHash(Encoding.UTF8.GetBytes(StrText));
                // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
                if (IsCapitals)
                {
                    //全部大写
                    foreach (byte b in ArrByte)
                    {
                        StrEncrypt.Append(b.ToString("X").PadLeft(2, '0'));
                    }
                }
                else
                {
                    //全部小写
                    foreach (byte b in ArrByte)
                    {
                        StrEncrypt.Append(b.ToString("x").PadLeft(2, '0'));
                    }
                }
            }
            return StrEncrypt.ToString();
        }

        /// <summary>
        /// 判断文件目录是否存在
        /// </summary>
        /// <param name="DirectoryPath"></param>
        /// <returns></returns>
        public static bool IsDirectoryExists(string DirectoryPath)
        {
            return Directory.Exists(DirectoryPath);
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static bool IsFileExists(string FilePath)
        {
            return File.Exists(FilePath);
        }

        /// <summary>
        /// 判断网络文件是否存在
        /// </summary>
        /// <param name="FilePathUrl"></param>
        /// <returns></returns>
        public static bool IsNetFileExists(string FilePathUrl)
        {
            //LogHelper.Info(FilePathUrl);
            bool result = false;
            WebResponse response = null;
            try
            {
                WebRequest req = WebRequest.Create(FilePathUrl);
                response = req.GetResponse();
                //LogHelper.Info(response.ToString());
                result = response == null ? false : true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 创建文件并写入信息
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="FileContent"></param>
        /// <returns></returns>
        public static bool CreateFile(string FilePath, string FileContent)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                fs = File.Create(FilePath);//创建文件
                sw = new StreamWriter(fs, Encoding.UTF8);//创建写入流
                sw.Write(FileContent);
                sw.Flush();
                sw.Close();
                return true;
            }
            catch (Exception ex)
            {
                fs.Close();
                fs.Dispose();
                sw.Close();
                sw.Dispose();
                LogHelper.Error("创建文件" + FilePath + "出错，错误信息：" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 根据当前日期创建 年/月/日 格式的文件夹
        /// </summary>
        /// <returns>新创建的文件夹完整路径</returns>
        public static string CreateDateDirectory(string ParentPath, out string message)
        {
            message = string.Empty;
            try
            {
                DateTime DtNow = DateTime.Now;
                string StrDate = DtNow.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
                string DirectoryYear = ParentPath + "/" + StrDate.Substring(0, 4);//年份
                string DirectoryMonth = ParentPath + "/" + StrDate.Substring(0, 4) + "/" + StrDate.Substring(4, 2);//月份
                string DirectoryDay = ParentPath + "/" + StrDate.Substring(0, 4) + "/" + StrDate.Substring(4, 2) + "/" + StrDate.Substring(6);//日

                //判断年份文件是否存在，不存在则创建该文件夹
                if (!Directory.Exists(DirectoryYear))
                {
                    Directory.CreateDirectory(DirectoryYear);
                }
                //判断月份文件夹是否存在，不存在则创建该文件夹
                if (!Directory.Exists(DirectoryMonth))
                {
                    Directory.CreateDirectory(DirectoryMonth);
                }
                //判断日文件夹是否存在，不存在则创建该文件夹
                if (!Directory.Exists(DirectoryDay))
                {
                    Directory.CreateDirectory(DirectoryDay);
                }
                string DirectoryDatePath = DtNow.ToString("yyyy/MM/dd", DateTimeFormatInfo.InvariantInfo);
                return DirectoryDatePath;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                LogHelper.Error("目录创建出错，出错信息：" + ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="FullPath">被创建文件的完整路径</param>
        public static void CreateDirectory(string FullPath)
        {
            if (!Directory.Exists(FullPath))
            {
                Directory.CreateDirectory(FullPath);
            }
        }

        /// <summary>
        /// 读取web.config配置节点信息
        /// </summary>
        /// <param name="Key">节点名称</param>
        /// <param name="Section">配置节点路径名称</param>
        /// <returns>节点值</returns>
        public static string ReadConfig(string Key, string Section = "appSettings")
        {
            return new ConfigHelper().GetValue(Key, Section);
        }

        /// <summary>
        /// 读取xml文件节点文本值
        /// </summary>
        /// <param name="XmlFullPath"></param>
        /// <param name="SelectNodes"></param>
        /// <param name="NodeIndex"></param>
        /// <returns></returns>
        public static string ReadXmlNodeText(string XmlFullPath, string SelectNodes, int NodeIndex)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XmlFullPath);
            XmlNodeList NodeList = xmlDoc.SelectNodes(SelectNodes);
            string NodeText = NodeList[NodeIndex].ChildNodes[0].InnerText;
            return NodeText;
        }

        /// <summary>
        /// 读取xml文件节点属性值
        /// </summary>
        /// <param name="XmlFullPath"></param>
        /// <param name="NodeIndex"></param>
        /// <param name="TagName"></param>
        /// <param name="Attributes"></param>
        /// <returns></returns>
        public static string ReadXmlNodeText(string XmlFullPath, int NodeIndex, string TagName, string Attributes)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XmlFullPath);
            XmlNodeList NodeList = xmlDoc.GetElementsByTagName(TagName);
            string AttValue = NodeList[NodeIndex].Attributes[Attributes].Value;
            return AttValue;
        }

        /// <summary>
        /// 读取xml文件节点属性值
        /// </summary>
        /// <param name="NodeList"></param>
        /// <param name="NodeIndex"></param>
        /// <param name="AttrName"></param>
        /// <returns></returns>
        public static string ReadXmlNodeText(XmlNodeList NodeList, int NodeIndex, string AttrName)
        {
            string AttValue = NodeList[NodeIndex].Attributes[AttrName].Value;
            return AttValue;
        }

        /// <summary>
        /// 根据xml节点获取节点属性值
        /// </summary>
        /// <param name="Xn"></param>
        /// <param name="AttrName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string ReadXmlNodeText(XmlNode Xn, string AttrName, out string message)
        {
            message = string.Empty;
            try
            {
                string AttValue = Xn.Attributes[AttrName].Value;
                return AttValue;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取xml根节点文件节点下的所有子节点
        /// </summary>
        /// <param name="XmlFullPath"></param>
        /// <param name="RootName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static XmlNodeList GetChildNodeList(string XmlFullPath, string RootName, out string message)
        {
            message = string.Empty;
            XmlNodeList xmlNodeList = null;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(XmlFullPath);
                XmlNode xnRoot = xmlDoc.SelectSingleNode(RootName);
                xmlNodeList = xnRoot.ChildNodes;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return xmlNodeList;
        }

        /// <summary>
        /// 文件流分段上传方式获取文件数据并保存文件
        /// </summary>
        /// <param name="saveFilePath"></param>
        /// <param name="file"></param>
        public static void SaveFromStream(string saveFilePath, HttpPostedFileBase file)
        {
            long lStartPos = 0;
            int startPosition = 0;
            int endPosition = 0;
            var contentRange = HttpContext.Current.Request.Headers["Content-Range"];
            //bytes 10000-19999/1157632
            if (!string.IsNullOrEmpty(contentRange))
            {
                contentRange = contentRange.Replace("bytes", "").Trim();
                contentRange = contentRange.Substring(0, contentRange.IndexOf("/"));
                string[] ranges = contentRange.Split('-');
                startPosition = int.Parse(ranges[0]);//上传部分文件流起始位置
                endPosition = int.Parse(ranges[1]);//上传部分文件流结束位置
            }
            FileStream fs;
            if (File.Exists(saveFilePath))
            {
                fs = File.OpenWrite(saveFilePath);//打开已经上传的文件
                lStartPos = fs.Length;//获取已上传文件的大小
            }
            else
            {
                fs = new FileStream(saveFilePath, FileMode.Create);//新的文件上传
                lStartPos = 0;
            }
            if (lStartPos > endPosition)
            {
                fs.Close();//已上传部分文件大小超出上传部分文件流结束位置，则说明上传完毕了
                return;
            }
            else if (lStartPos < startPosition)
            {
                lStartPos = startPosition;
            }
            else if (lStartPos > startPosition && lStartPos < endPosition)
            {
                lStartPos = startPosition;
            }
            fs.Seek(lStartPos, SeekOrigin.Current);
            byte[] nbytes = new byte[512];
            int nReadSize = 0;
            nReadSize = file.InputStream.Read(nbytes, 0, 512);
            while (nReadSize > 0)
            {
                fs.Write(nbytes, 0, nReadSize);
                nReadSize = file.InputStream.Read(nbytes, 0, 512);
            }
            fs.Close();
        }

        /// <summary>
        /// 将图片的Base64字符串保存为文件
        /// </summary>
        /// <param name="fileData"></param>
        /// <param name="saveFilePath"></param>
        /// <param name="ext"></param>
        public static void SaveFromBase64String(string fileData, string saveFilePath, string ext)
        {
            byte[] fileBytes = Convert.FromBase64String(fileData);
            string name = DateTime.Now.ToString("yyyyMMddHHmmss") + ext;
            string savePath = saveFilePath + "\\" + name;
            File.WriteAllBytes(savePath, fileBytes);
        }

        /// <summary>
        /// 异步GET请求WEBAPI
        /// </summary>
        /// <param name="GetUrl">接口地址</param>
        /// <param name="ParamData">提交参数</param>
        /// <param name="Ret">请求响应结果</param>
        /// <returns>是否请求成功</returns>
        public static bool HttpAsyncGet(string GetUrl, string ParamData, out string Ret)
        {
            bool Result = true;
            Ret = string.Empty;
            if (DataDic.IsDebug)
            {
                LogHelper.Info("请求的接口url：" + GetUrl);
                LogHelper.Info("提交给接口的参数ParamData：" + ParamData);
            }
            try
            {
                if (string.IsNullOrEmpty(GetUrl))
                {
                    throw new ArgumentNullException("GetUrl");
                }
                if (!string.IsNullOrEmpty(ParamData))
                {
                    GetUrl = GetUrl + "?" + ParamData;
                }
                if (DataDic.IsDebug)
                {
                    LogHelper.Info("GetUrl+ParamData=" + GetUrl);
                }
                HttpWebRequest request = WebRequest.Create(GetUrl) as HttpWebRequest;
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        Ret = sr.ReadToEnd();
                    }
                }
                else
                {
                    Ret = response.StatusDescription;
                    Result = false;
                }
                if (DataDic.IsDebug)
                {
                    LogHelper.Info("接口返回结果：" + Ret);
                }
            }
            catch (WebException ex)
            {
                Result = false;
                Ret = ex.Message;
                LogHelper.Error("HttpAsyncGet请求接口出错，出错信息：" + ex.Message + ex.StackTrace + ex.TargetSite + ex.Source + ex.InnerException);
            }
            return Result;
        }

        /// <summary>
        /// 异步POST请求WEBAPI
        /// </summary>
        /// <param name="PostUrl">接口地址</param>
        /// <param name="ParamData">提交数据（json或参数&连接字符串）</param>
        /// <param name="Ret">返回消息</param>
        /// <param name="ContentType">数据类型，默认为json</param>
        /// <returns>是否成功提交数据</returns>
        public static bool HttpAsyncPost(string PostUrl, string ParamData, out string Ret, string ContentType = "application/json")
        {
            Ret = string.Empty;
            bool Result = true;
            if (DataDic.IsDebug)
            {
                LogHelper.Info("请求的接口url：" + PostUrl);
                LogHelper.Info("提交给接口的参数：" + ParamData);
            }
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(PostUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
            StringContent content = new StringContent(ParamData, Encoding.UTF8, ContentType);
            try
            {
                var result = client.PostAsync(PostUrl, content).Result;
                HttpStatusCode code = result.StatusCode;
                if (DataDic.IsDebug)
                {
                    LogHelper.Info("HttpStatusCode：" + code.ToString());
                }
                if (code.Equals(HttpStatusCode.OK))
                {
                    Ret = result.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    Result = false;
                    Ret = result.ReasonPhrase;
                }
                if (DataDic.IsDebug)
                {
                    LogHelper.Info("接口返回内容：" + Ret);
                }
            }
            catch (Exception ex)
            {
                Result = false;
                LogHelper.Error("HttpAsyncPost请求接口出错，出错信息：" + ex.Message + ex.StackTrace + ex.TargetSite + ex.Source + ex.InnerException);
            }
            finally
            {
                if (client != null)
                {
                    client.Dispose();
                }
            }
            return Result;
        }

        /// <summary>
        /// 验证https证书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region 计算地理位置距离
        /// <summary>
        /// 地球赤道半径，单位：km
        /// </summary>
        private const double EARTH_RADIUS = 6378.137;//地球半径：6371.393
        /// <summary>
        /// 计算角度
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static double rad(double d)
        {
            return d * Math.PI / 180.0;
        }

        /// <summary>
        /// 计算地理位置距离，单位：米
        /// </summary>
        /// <param name="lat1">原点经度</param>
        /// <param name="lng1">原点维度</param>
        /// <param name="lat2">动点位置经度</param>
        /// <param name="lng2">动点位置维度</param>
        /// <returns></returns>
        public static int GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            try
            {
                double radLat1 = rad(lat1);
                double radLat2 = rad(lat2);
                double a = radLat1 - radLat2;
                double b = rad(lng1) - rad(lng2);

                double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
                 Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
                s = s * EARTH_RADIUS * 1000;
                s = Math.Round(s * 10000) / 10000;
                return Convert.ToInt32(s);
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        /// <summary>
        /// 验证手机号码是否合法
        /// </summary>
        /// <param name="mobilecode">手机号码</param>
        /// <returns></returns>
        public static bool VerifyMobile(string mobilecode)
        {
            Regex reg = new Regex("0?(13|14|15|17|18)[0-9]{9}", RegexOptions.IgnoreCase);
            return reg.IsMatch(mobilecode);
        }

        /// <summary>
        /// Json字符串反序列化
        /// </summary>
        /// <typeparam name="T">序列化对象类型</typeparam>
        /// <param name="JsonStr">json字符串</param>
        /// <returns></returns>
        public static T JsonStringToObj<T>(this string JsonStr)
        {
            //JavaScriptSerializer Serializer = new JavaScriptSerializer();
            //JsonStr = Regex.Replace(JsonStr, @"\\/Date\((\d+)\)\\/", match =>
            //{
            //    DateTime dt = new DateTime(1970, 1, 1);
            //    dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
            //    dt = dt.ToLocalTime();
            //    return dt.ToString("yyyyMMddHHmmss");
            //});
            //T objs = Serializer.Deserialize<T>(JsonStr);
            T objs = JsonConvert.DeserializeObject<T>(JsonStr);
            return objs;
        }

        /// <summary>
        /// 对象序列化Json字符串
        /// </summary>
        /// <typeparam name="T">序列化对象类型</typeparam>
        /// <param name="Obj">序列化对象</param>
        /// <returns></returns>
        public static string ObjToJsonString<T>(object Obj)
        {
            JsonSerializerSettings jss = new JsonSerializerSettings();
            jss.NullValueHandling = NullValueHandling.Ignore;
            jss.DateFormatString = "yyyyMMddHHmmss";
            string JsonString = JsonConvert.SerializeObject(Obj, Newtonsoft.Json.Formatting.Indented, jss);
            return JsonString;
        }

        /// <summary>
        /// 读取txt文件内容
        /// </summary>
        /// <param name="FileFullPath">文件绝对路径</param>
        /// <returns></returns>
        public static string ReadText(string FileFullPath)
        {
            if (File.Exists(FileFullPath))
            {
                StreamReader Sr = null;
                try
                {
                    Sr = File.OpenText(FileFullPath);
                    StringBuilder Sb = new StringBuilder();
                    while (Sr.Peek() > -1)
                    {
                        Sb.Append(Sr.ReadLine());
                    }
                    Sr.Close();
                    Sr.Dispose();
                    return Sb.ToString();
                }
                catch (Exception)
                {
                    Sr.Close();
                    Sr.Dispose();
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// calss对象转List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<string> CreateModelProperty<T>(T obj) where T : class
        {
            List<string> listColumns = new List<string>();
            BindingFlags bf = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;
            Type objType = typeof(T);
            PropertyInfo[] propInfoArr = objType.GetProperties(bf);
            foreach (PropertyInfo item in propInfoArr)
            {
                object[] objAttrs = item.GetCustomAttributes(typeof(T), true);
                listColumns.Add(item.Name);
            }
            return listColumns;
        }

        /// <summary>
        /// 向集合中添加元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="value"></param>
        public static void IEnumerableAdd<T>(this IEnumerable<T> collection, T value)
        {
            (collection as List<T>).Add(value);
        }

        /// <summary>
        /// 从集合中删除元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="value"></param>
        public static void IEnumerableRemove<T>(this IEnumerable<T> collection, T value)
        {
            (collection as List<T>).Remove(value);
        }

        /// <summary>
        /// 检索集合中是否包含某个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IEnumerableContains<T>(this IEnumerable<T> collection, T value)
        {
            return (collection as List<T>).Contains(value);
        }

        /// <summary>
        /// 合并两个路径字符串(适用于磁盘文件路径和url路径拼接的Path.Combine)
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static string Combine(params string[] paths)
        {
            if (paths.Length == 0)
            {
                throw new ArgumentException("please input path");
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                string spliter = "\\";
                string firstPath = paths[0];
                if (firstPath.StartsWith("HTTP", StringComparison.OrdinalIgnoreCase))
                {
                    spliter = "/";
                }
                if (!firstPath.EndsWith(spliter))
                {
                    firstPath = firstPath + spliter;
                }
                builder.Append(firstPath);
                for (int i = 1; i < paths.Length; i++)
                {
                    string nextPath = paths[i];
                    if (nextPath.StartsWith("/") || nextPath.StartsWith("\\"))
                    {
                        nextPath = nextPath.Substring(1);
                    }
                    if (i != paths.Length - 1)//not the last one
                    {
                        if (nextPath.EndsWith("/") || nextPath.EndsWith("\\"))
                        {
                            nextPath = nextPath.Substring(0, nextPath.Length - 1) + spliter;
                        }
                        else
                        {
                            nextPath = nextPath + spliter;
                        }
                    }
                    builder.Append(nextPath);
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// 添加cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="val"></param>
        public static void addCookie(string name, string val)
        {
            HttpCookie hc = new HttpCookie(name, HttpUtility.UrlEncode(val, Encoding.UTF8));
            if (HttpContext.Current.Request.Cookies[name] != null)
            {
                hc.Value = val;
                HttpContext.Current.Response.Cookies.Set(hc);
            }
            else
            {
                hc.Value = val;
                HttpContext.Current.Response.Cookies.Add(hc);
            }
        }

        /// <summary>
        /// 获取cookie
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string getCookie(string name)
        {
            HttpCookie v = HttpContext.Current.Request.Cookies[name];
            if (v != null)
            {
                return HttpUtility.UrlDecode(v.Value, Encoding.UTF8);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Object扩展方法-类型转换
        /// </summary>
        /// <param name="Obj"></param>
        /// <returns></returns>
        public static int ToInt(this object Obj)
        {
            if (Obj == null)
            {
                return 0;
            }
            string s = Obj.ToString();
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            int Ret = 0;
            int.TryParse(s, out Ret);
            return Ret;
        }
    }
}
