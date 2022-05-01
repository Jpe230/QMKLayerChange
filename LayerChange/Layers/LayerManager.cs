using System;
using System.Collections.Generic;
using LayerChange.Entities;
using System.Text.Json;
using System.IO;
using System.Linq;
using Tulpep.NotificationWindow;
using System.Diagnostics;
using LayerChange.HID;

namespace LayerChange.Layers
{
    public class LayerManager
    {
        private static LayerManager instance;

        public static LayerManager GetManager
        {
            get
            {
                if(instance == null)
                    instance = new LayerManager();
                return instance;
            }
        }

        private int CurrentLayer;
        private List<Layer> UserLayers;

        public LayerManager()
        {
            using (StreamReader streamReader = new StreamReader("Layers.json"))
            {
                string jsonString = streamReader.ReadToEnd();
                UserLayers = JsonSerializer.Deserialize<LayerWrapper>(jsonString)?.Layers;
            }
        }

        public void SetCurrentLayer(WindowInfo windowInfo)
        {

            List<Layer> pLayers = UserLayers.Where(
                ul =>
                    (
                        ul.Apps.Any(app => windowInfo.FileDescription.Contains(app))
                    ) ||
                    (
                        ul.Apps.Any(app => windowInfo.ProcessName.Contains(app))
                    ) ||
                    (
                        ul.Apps.Any(app => windowInfo.WindowName.Contains(app))
                    )

            ).ToList();

            if(pLayers.Count > 0)
            {
                CurrentLayer = pLayers.First().LayerId;
            }
            else
            {
                CurrentLayer = 0;
            }

            QMKBoardManager.GetManager.ChangeLayer(CurrentLayer);
        }
    }
}
