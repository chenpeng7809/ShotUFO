using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class XMLDoc
{
		public static XMLDoc LoadXML (string path, string name)
		{
				XMLDoc newData = new XMLDoc ();
				//				newData.Load (path + name);
				MemoryStream stream = DecryptXml (path + name);
				newData.Load (stream);
				stream.Close ();
				return newData;
		}
	
		public static XMLDoc LoadXML (Stream inStream)
		{
				XMLDoc newData = new XMLDoc ();
				//				newData.Load (inStream);
				MemoryStream stream = DecryptXml (inStream);
				newData.Load (stream);
				stream.Close ();
				return newData;
		}
	
		public void WriteToFile ()
		{
				Debug.Log ("SteinXMLDoc WriteToFile[" + _path + "]");
				xmlDoc.Save (_path);
		}
	
		string _path;
		XmlDocument xmlDoc = new XmlDocument ();
		XmlNode root;
	
		public void Load (string path)
		{
				try {
						xmlDoc.Load (path);
						root = xmlDoc.SelectSingleNode ("Root");
						_path = path;
				} catch (System.Exception e) {
						Debug.LogError (e.ToString ());
						root = null;
				}
		}
	
		public void Load (Stream inStream)
		{
				try {
						xmlDoc.Load (inStream);
						root = xmlDoc.SelectSingleNode ("Root");
				} catch (System.Exception e) {
						Debug.LogError (e.ToString ());
						root = null;
						throw e;
				}
		}
	
		public XmlNode Find (System.Predicate<XmlNode> match)
		{
				foreach (XmlNode node in root.ChildNodes) {
						if (match.Invoke (node))
								return node;
				}
		
				return null;
		}
	
		public List<XmlNode> GetAll ()
		{
				List<XmlNode> nodeList = new List<XmlNode> ();
				foreach (XmlNode node in root.ChildNodes) {
						nodeList.Add (node);
				}
				return nodeList;
		}
	
		public XmlNodeList GetNodeList ()
		{
				return root.ChildNodes;
		}
	
		public XmlNode CreateItem (string itemName = "item")
		{
				XmlNode newItem = xmlDoc.CreateNode (XmlNodeType.Element, itemName, "");
				root.AppendChild (newItem);
				return newItem;
		}
	
		public XmlNode CreateAtt (string attName)
		{
				XmlNode attr = xmlDoc.CreateNode (XmlNodeType.Attribute, attName, "");
				return attr;
		}
	
		public void Release ()
		{
				root = null;
				xmlDoc = null;
		}
		/// <summary>
		/// 解密xml
		/// </summary>
		/// <returns>The xml.</returns>
		/// <param name="inStream">In stream.</param>
		static MemoryStream DecryptXml (Stream inStream)
		{
				StreamReader sReader = new StreamReader (inStream);
				string xmlData = sReader.ReadToEnd ();
				sReader.Close ();
				string decryptData = Decrypt (xmlData);
				byte[] decryptArray = UTF8Encoding.UTF8.GetBytes (decryptData);
				MemoryStream memoryStream = new MemoryStream (decryptArray);
				return memoryStream;
		}
		/// <summary>
		/// 解密xml
		/// </summary>
		/// <returns>The xml.</returns>
		/// <param name="xmlPath">Xml path.</param>
		static MemoryStream DecryptXml (string xmlPath)
		{
				StreamReader sReader = File.OpenText (xmlPath);
				string xmlData = sReader.ReadToEnd ();
				sReader.Close ();
				string decryptData = Decrypt (xmlData);
				byte[] decryptArray = UTF8Encoding.UTF8.GetBytes (decryptData);
				MemoryStream memoryStream = new MemoryStream (decryptArray);
				return memoryStream;
		}
	
		static string Decrypt (string toD)
		{
				//加密和解密采用相同的key,具体值自己填，但是必须为32位//
				byte[] keyArray = UTF8Encoding.UTF8.GetBytes ("12348578902223367877723456789012");
		
				RijndaelManaged rDel = new RijndaelManaged ();
				rDel.Key = keyArray;
				rDel.Mode = CipherMode.ECB;
				rDel.Padding = PaddingMode.PKCS7;
				ICryptoTransform cTransform = rDel.CreateDecryptor ();
		
				byte[] toEncryptArray = System.Convert.FromBase64String (toD);
				byte[] resultArray = cTransform.TransformFinalBlock (toEncryptArray, 0, toEncryptArray.Length);
		
				return UTF8Encoding.UTF8.GetString (resultArray);
		}
}