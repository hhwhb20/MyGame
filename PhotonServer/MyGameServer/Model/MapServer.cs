using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Map;
using System.Xml;
using Common;

namespace MyGameServer.Model
{
    public class MapServer : Map
    {
        public void Init(string xmlPath)
        {
            XmlDocument xmlDoc = new XmlDocument();

            //加载文件
            xmlDoc.Load(xmlPath);

            //得到根节点
            XmlNode rootNode = xmlDoc.FirstChild;//获取第一个节点

            //得到根节点下面的子节点的集合
            XmlNodeList list = rootNode.ChildNodes; //获取当前节点下面的所有子节点

            foreach (XmlNode temp in list)
            {
                switch (temp.Name)
                {
                    case "mapSize":
                        SaveMapSize(temp);
                        break;
                    case "tiledSize":
                        SaveTiledSize(temp);
                        break;
                    case "mapTiledNum":
                        SaveMapTiledNum(temp);
                        break;
                    case "arrayTiled":
                        SaveArrayTiled(temp);
                        break;
                    default:
                        break;
                }
            }
        }

        public void SaveMapSize(XmlNode node)
        {
            MyGameServer.Instance.PrintLog("SaveMapSize Start");
            if(node == null)
            {
                return;
            }

            XmlNodeList list = node.ChildNodes;

            if (list.Count != (int)enumMapSize.MS_Max)
            {
                MyGameServer.Instance.PrintLog("xml地图配置mapSize中子节点数量错误");
                return;
            }

            XmlNode width = list[(int)enumMapSize.MS_Width];
            XmlNode height = list[(int)enumMapSize.MS_Height];

            int w = Int32.Parse(width.InnerText);
            int h = Int32.Parse(height.InnerText);

            MapSize = new Size(w, h);

            MyGameServer.Instance.PrintLog("MapSize初始化成功");
        }

        public void SaveTiledSize(XmlNode node)
        {
            MyGameServer.Instance.PrintLog("SaveTiledSize Start");
            if (node == null)
            {
                return;
            }

            XmlNodeList list = node.ChildNodes;

            if (list.Count != (int)enumTiledSize.TS_Max)
            {
                MyGameServer.Instance.PrintLog("xml地图配置tiledSize中子节点数量错误");
                return;
            }

            XmlNode width = list[(int)enumTiledSize.TS_Width];
            XmlNode height = list[(int)enumTiledSize.TS_Height];

            TiledSize = new Size(Int32.Parse(width.InnerText), Int32.Parse(height.InnerText));

            MyGameServer.Instance.PrintLog("TiledSize初始化成功");
        }

        public void SaveMapTiledNum(XmlNode node)
        {
            MyGameServer.Instance.PrintLog("SaveMapTiledNum Start");
            if (node == null)
            {
                return;
            }

            XmlNodeList list = node.ChildNodes;

            if (list.Count != (int)enumMapTiledNum.MTN_Max)
            {
                MyGameServer.Instance.PrintLog("xml地图配置mapTiledNum中子节点数量错误");
                return;
            }

            XmlNode width = list[(int)enumMapTiledNum.MTN_Width];
            XmlNode height = list[(int)enumMapTiledNum.MTN_Height];

            MapTiledNum = new Size(Int32.Parse(width.InnerText), Int32.Parse(height.InnerText));

            MyGameServer.Instance.PrintLog("MapTiledNum初始化成功");
        }

        public void SaveArrayTiled(XmlNode node)
        {
            MyGameServer.Instance.PrintLog("SaveArrayTiled Start");
            if (node == null)
            {
                return;
            }

            XmlNodeList list = node.ChildNodes;

            if (list.Count != MapTiledNum.width * MapTiledNum.height)
            {
                MyGameServer.Instance.PrintLog("xml地图配置mapTiledNum中子节点数量错误");
                return;
            }

            foreach(XmlNode temp in list)
            {
                if(temp.Name == "tiled")
                {
                    XmlNodeList tempList = temp.ChildNodes;

                    if (tempList.Count != (int)enumTiled.eT_Max)
                    {
                        MyGameServer.Instance.PrintLog("xml地图配置mapTiledNum中子节点数量错误");
                        return;
                    }

                    XmlNode xIndex = list[(int)enumTiled.eT_xIndex];
                    XmlNode yIndex = list[(int)enumTiled.eT_yIndex];
                    XmlNode terrain = list[(int)enumTiled.eT_Terrain];

                    Tiled tiled = new Tiled();
                    tiled.Terrain = (TerrainCode)Int32.Parse(terrain.InnerText);
                    tiled.xIndex = Int32.Parse(xIndex.InnerText);
                    tiled.yIndex = Int32.Parse(yIndex.InnerText);

                    SetTiledByIndex(tiled.xIndex, tiled.yIndex, tiled);
                }
            }

            MyGameServer.Instance.PrintLog("Tiled初始化成功");
        }
    }
}
