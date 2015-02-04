using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class XMLDataHolder
{
		protected XMLDoc _data;
		protected XmlNode curNode;
		protected const string exceptionMessageFormat = "DataForm {0} DO NOT have data {1}";
		protected const int NONE_DATA_KEY = -1;
		public string ItemKeyName;
	
		public virtual bool ReadBool (string name)
		{
				byte _boolVale = ReadByte (name);
				return _boolVale == 1;
		}
		/// <summary>
		///读取毫秒（ms）时间
		/// </summary>
		/// <returns>The MS time.</returns>
		/// <param name="name">Name.</param>
		public virtual float ReadMSTime (string name)
		{
				return (float)ReadLong (name) / 1000.0f;
		}
	
		public virtual float ReadPercentValue (string name)
		{
				return (float)ReadLong (name) / 100.0f;
		}
	
		public virtual int[] ReadIdList (string name, char splitChar = ',')
		{
				string str = ReadUTF8 (name);
		
				if (str == null || str.Equals (""))
						return null;
				string[] splitList = str.Split (new char[]{splitChar});
				int[] idArr = new int[splitList.Length];
				for (int i=0; i<splitList.Length; ++i) {
						idArr [i] = int.Parse (splitList [i]);
				}
				return idArr;
		}
	
		public virtual float[] ReadFloatArray (string name, char splitChar = ',')
		{
				string str = ReadUTF8 (name);
		
				if (str == null || str.Equals (""))
						return null;
				string[] splitList = str.Split (new char[]{splitChar});
				float[] idArr = new float[splitList.Length];
				for (int i=0; i<splitList.Length; ++i) {
						idArr [i] = float.Parse (splitList [i]);
				}
				return idArr;
		}
	
		public XMLDoc XMLData {
				get {
						return _data as XMLDoc;
				}
		}
	
		public void SetXMLData (XMLDoc data)
		{
				_data = data;
		}
	
		public void Release ()
		{
				curNode = null;
				_data.Release ();
				_data = null;
		}
	
		public bool SetCurData (string[] keyNames, string[] keyValues)
		{
				for (int i =0; i<keyNames.Length; ++i) {
						if (keyValues [i] == NONE_DATA_KEY.ToString ())
								return false;
			
				}
		
				curNode = _data.Find (
			delegate (XmlNode node) {
						bool _matchResult = true;
						for (int i =0; i<keyNames.Length; ++i) {
								_matchResult &= node.Attributes.GetNamedItem (keyNames [i]).Value == keyValues [i];
				
						}
						return _matchResult;
			
				});
				if (curNode == null) {
						Debug.LogError (string.Format (exceptionMessageFormat, this.ToString (), keyValues [0]));
						return false;
				}
				return true;
		}
	
		XmlNodeList list = null;
		int nodePtr = 0;
	
		public bool MoveNext ()
		{
				if (list == null)
						list = _data.GetNodeList ();
				if (nodePtr >= list.Count)
						return false;
				else {
						curNode = list [nodePtr];
						nodePtr++;
						return true;		
				}
		}
	
		public byte ReadByte (string name)
		{
				string value = curNode.Attributes.GetNamedItem (name).Value;
		
				if (string.IsNullOrEmpty (value))
						return 0;
				else
						return byte.Parse (value);
		}
	
		public sbyte ReadSByte (string name)
		{
				string value = curNode.Attributes.GetNamedItem (name).Value;
		
				if (string.IsNullOrEmpty (value))
						return 0;
				else
						return sbyte.Parse (value);
		}
	
		public int ReadInt (string name)
		{
				try {
						string value = curNode.Attributes.GetNamedItem (name).Value;
						//				return int.Parse (curNode.Attributes.GetNamedItem (name).Value);
						if (string.IsNullOrEmpty (value))
								return 0;
						else
								return int.Parse (value);
				} catch (System.Exception e) {
						Debug.Log ("Read int [" + name + "] Failed " + e.ToString ());
						return 0;
				} 
		}
	
		public long ReadLong (string name)
		{
				string value = curNode.Attributes.GetNamedItem (name).Value;
				if (string.IsNullOrEmpty (value))
						return 0;
				else
						return long.Parse (value);
		}
	
		public string ReadUTF8 (string name)
		{
				return curNode.Attributes.GetNamedItem (name).Value;
		}
	
		public float ReadFloat (string name)
		{
				try {
						string value = curNode.Attributes.GetNamedItem (name).Value;
			
						if (string.IsNullOrEmpty (value))
								return 0;
						else {
								return float.Parse (value);
						}
				} catch (System.Exception e) {
						Debug.Log ("Read float [" + name + "] Failed " + e.ToString ());
						return 0;
				} 
		}
	
		public int[] GetAllKeys ()
		{
				List<int> idList = new List<int> ();
				List<XmlNode> nodeList = _data.GetAll ();
				foreach (XmlNode item in nodeList) {
						idList.Add (int.Parse (item.Attributes.GetNamedItem (ItemKeyName).Value));
				}
				return idList.ToArray ();
		}
	
		public byte[] ReadBlob (string name)
		{
				throw new System.NotImplementedException ();
		}
	
		protected XmlNode EnsureAtt (string name)
		{
				XmlNode att = curNode.Attributes.GetNamedItem (name);
				if (curNode == null) {
						att = _data.CreateAtt (name);
						curNode.Attributes.SetNamedItem (att);		
				}
				return att;
		}
	
		protected void WriteInt (string name, int value)
		{
				EnsureAtt (name).Value = value.ToString ();
		}
	
		protected void WriteUTF8 (string name, string value)
		{
				EnsureAtt (name).Value = value;
		}
	
		protected void WriteBool (string name, bool value)
		{
				EnsureAtt (name).Value = value ? "1" : "0";
		}
	
	
}
